using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    protected Animator animator;

    public int maxHp;

    public IntVariable hp;      // hp 值类型
    public int CurrentHP { get => hp.currentValue; set => hp.SetValue(value); } // 当前 HP
    public int MaxHP { get => hp.maxValue; }                                    // 最大 HP

    public bool isDead;

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    protected virtual void Start()
    {
        hp.maxValue = maxHp;
        CurrentHP = MaxHP;
    }

    /// <summary>
    /// 计算伤害
    /// </summary>
    /// <param name="damage">伤害值</param>
    public virtual void TakeDamage(int damage)
    {
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
}
