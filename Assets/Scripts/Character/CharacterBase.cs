using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    protected Animator animator;

    public VFXController vFXController;

    [Header("血量")]
    public int maxHp;

    public IntVariable hp;      // hp 值类型
    public int CurrentHP { get => hp.currentValue; set => hp.SetValue(value); } // 当前 HP
    public int MaxHP { get => hp.maxValue; }                                    // 最大 HP

    public bool isDead;

    [Header("防御值")]
    public IntVariable defense; // 防御值值类型
    public int CurrentDefense { get => defense.currentValue; set => defense.SetValue(value); } // 当前 防御值

    [Header("力量效果持续回合数")]
    public IntVariable strengthRound;   // 力量效果持续回合数（buff or debuff）

    [Header("基础力量值")]
    public float baseStrength = 1.0f;           // 基础力量值
    private float strengthEffectValue = 0.5f;   // 力量增效值

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    protected virtual void Start()
    {
        hp.maxValue = maxHp;
        CurrentHP = MaxHP;
        strengthRound.SetValue(0);
        ResetDefense();
    }

    /// <summary>
    /// 计算伤害
    /// </summary>
    /// <param name="damage">伤害值</param>
    public virtual void TakeDamage(int damage)
    {
        // 计算防御
        if (CurrentDefense > damage)
        {
            CurrentDefense -= damage;
            Debug.Log("Current Defense " + CurrentHP);
        }
        else
        {
            damage -= CurrentDefense;
            CurrentDefense = 0;
        }

        // 计算血量
        if (CurrentHP > damage)
        {
            CurrentHP -= damage;
            Debug.Log("Current HP " + CurrentHP);
        }
        else
        {
            CurrentHP = 0;  // 死亡
            isDead = true;
        }
    }

    /// <summary>
    /// 增加防御值
    /// </summary>
    /// <param name="amount">防御值</param>
    public virtual void IncreaseDefense(int amount)
    {
        CurrentDefense += amount;   // 在当前防御值的基础上增加
        Debug.Log("Current Defense " + CurrentDefense);
    }

    /// <summary>
    /// 重置防御值
    /// </summary>
    public virtual void ResetDefense()
    {
        CurrentDefense = 0;
    }

    /// <summary>
    /// 恢复血量
    /// </summary>
    /// <param name="amount">血量值</param>
    public virtual void HealHealth(int amount)
    {
        CurrentHP += amount;
        CurrentHP = Mathf.Min(CurrentHP, MaxHP);

        vFXController.buff.SetActive(true);    // 启用 buff 效果

        Debug.Log("Current HP " + CurrentHP);
    }

    /// <summary>
    /// 设置力量 buff 或 debuff 值
    /// </summary>
    /// <param name="round">增加的可持续回合数</param>
    /// <param name="isPositive">是否是正面效果</param>
    public virtual void SetupStrength(int round, bool isBuff)
    {
        if (isBuff)
        {
            baseStrength = Mathf.Min(baseStrength + strengthEffectValue, 1.5f); // 增加基础力量
            vFXController.buff.SetActive(true);                                 // 启用 buff 效果
        }
        else
        {
            baseStrength = 1.0f - strengthEffectValue;  // 减小基础力量
            vFXController.debuff.SetActive(true);       // 启用 debuff 效果
        }

        if (baseStrength == 1.0f)       // 力量增益值没有变化
        {
            strengthRound.SetValue(0);  // 清除回合数
        }
        else
        {
            strengthRound.SetValue(strengthRound.currentValue + round);     // 增加持续回合个数
        }
    }

    /// <summary>
    /// 更新力量效果回合数（每回合调用）
    /// </summary>
    public void UpdateStrengthRound()
    {
        strengthRound.SetValue(strengthRound.currentValue - 1);     // 减少增加的回合数
        if (strengthRound.currentValue <= 0)
        {
            strengthRound.SetValue(0);
            baseStrength = 1.0f;            // 重置力量效果
        }
    }
}
