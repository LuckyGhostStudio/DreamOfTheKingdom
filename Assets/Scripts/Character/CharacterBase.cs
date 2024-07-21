using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    protected Animator animator;

    public int maxHp;

    public IntVariable hp;      // hp ֵ����
    public int CurrentHP { get => hp.currentValue; set => hp.SetValue(value); } // ��ǰ HP
    public int MaxHP { get => hp.maxValue; }                                    // ��� HP

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
    /// �����˺�
    /// </summary>
    /// <param name="damage">�˺�ֵ</param>
    public virtual void TakeDamage(int damage)
    {
        if (CurrentHP > damage)
        {
            CurrentHP -= damage;
            Debug.Log("Current HP " + CurrentHP);
        }
        else
        {
            CurrentHP = 0;  // ����
            isDead = true;
        }
    }
}
