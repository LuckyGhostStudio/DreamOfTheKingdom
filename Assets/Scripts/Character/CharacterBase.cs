using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    protected Animator animator;

    [Header("Ѫ��")]
    public int maxHp;

    public IntVariable hp;      // hp ֵ����
    public int CurrentHP { get => hp.currentValue; set => hp.SetValue(value); } // ��ǰ HP
    public int MaxHP { get => hp.maxValue; }                                    // ��� HP

    public bool isDead;

    [Header("����ֵ")]
    public IntVariable defense; // ����ֵֵ����
    public int CurrentDefense { get => defense.currentValue; set => defense.SetValue(value); } // ��ǰ ����ֵ

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
    /// �����˺�
    /// </summary>
    /// <param name="damage">�˺�ֵ</param>
    public virtual void TakeDamage(int damage)
    {
        // �������
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

        // ����Ѫ��
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

    /// <summary>
    /// ���ӷ���ֵ
    /// </summary>
    /// <param name="amount">����ֵ</param>
    public virtual void IncreaseDefense(int amount)
    {
        CurrentDefense += amount;   // �ڵ�ǰ����ֵ�Ļ���������
        Debug.Log("Current Defense " + CurrentDefense);
    }

    /// <summary>
    /// ���÷���ֵ
    /// </summary>
    public virtual void ResetDefense()
    {
        CurrentDefense = 0;
    }

    /// <summary>
    /// �ָ�Ѫ��
    /// </summary>
    /// <param name="amount">Ѫ��ֵ</param>
    public virtual void HealHealth(int amount)
    {
        CurrentHP += amount;

        if (CurrentHP > MaxHP)
        {
            CurrentHP = MaxHP;
        }

        vFXController.buff.SetActive(true);    // ���� buff Ч��

        Debug.Log("Current HP " + CurrentHP);
    }
}
