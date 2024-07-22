using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    protected Animator animator;

    public VFXController vFXController;

    [Header("Ѫ��")]
    public int maxHp;

    public IntVariable hp;      // hp ֵ����
    public int CurrentHP { get => hp.currentValue; set => hp.SetValue(value); } // ��ǰ HP
    public int MaxHP { get => hp.maxValue; }                                    // ��� HP

    public bool isDead;

    [Header("����ֵ")]
    public IntVariable defense; // ����ֵֵ����
    public int CurrentDefense { get => defense.currentValue; set => defense.SetValue(value); } // ��ǰ ����ֵ

    [Header("����Ч�������غ���")]
    public IntVariable strengthRound;   // ����Ч�������غ�����buff or debuff��

    [Header("��������ֵ")]
    public float baseStrength = 1.0f;           // ��������ֵ
    private float strengthEffectValue = 0.5f;   // ������Чֵ

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
        CurrentHP = Mathf.Min(CurrentHP, MaxHP);

        vFXController.buff.SetActive(true);    // ���� buff Ч��

        Debug.Log("Current HP " + CurrentHP);
    }

    /// <summary>
    /// �������� buff �� debuff ֵ
    /// </summary>
    /// <param name="round">���ӵĿɳ����غ���</param>
    /// <param name="isPositive">�Ƿ�������Ч��</param>
    public virtual void SetupStrength(int round, bool isBuff)
    {
        if (isBuff)
        {
            baseStrength = Mathf.Min(baseStrength + strengthEffectValue, 1.5f); // ���ӻ�������
            vFXController.buff.SetActive(true);                                 // ���� buff Ч��
        }
        else
        {
            baseStrength = 1.0f - strengthEffectValue;  // ��С��������
            vFXController.debuff.SetActive(true);       // ���� debuff Ч��
        }

        if (baseStrength == 1.0f)       // ��������ֵû�б仯
        {
            strengthRound.SetValue(0);  // ����غ���
        }
        else
        {
            strengthRound.SetValue(strengthRound.currentValue + round);     // ���ӳ����غϸ���
        }
    }

    /// <summary>
    /// ��������Ч���غ�����ÿ�غϵ��ã�
    /// </summary>
    public void UpdateStrengthRound()
    {
        strengthRound.SetValue(strengthRound.currentValue - 1);     // �������ӵĻغ���
        if (strengthRound.currentValue <= 0)
        {
            strengthRound.SetValue(0);
            baseStrength = 1.0f;            // ��������Ч��
        }
    }
}
