using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    protected Animator animator;

    [Header("血量")]
    public int maxHp;

    public IntVariable hp;      // hp 值类型
    public int CurrentHP { get => hp.currentValue; set => hp.SetValue(value); } // 当前 HP
    public int MaxHP { get => hp.maxValue; }                                    // 最大 HP

    public bool isDead;

    [Header("防御值")]
    public IntVariable defense; // 防御值值类型
    public int CurrentDefense { get => defense.currentValue; set => defense.SetValue(value); } // 当前 防御值

    public VFXController vFXController;

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    protected virtual void Start()
    {
        hp.maxValue = maxHp;
        CurrentHP = MaxHP;

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

        if (CurrentHP > MaxHP)
        {
            CurrentHP = MaxHP;
        }

        vFXController.buff.SetActive(true);    // 启用 buff 效果

        Debug.Log("Current HP " + CurrentHP);
    }
}
