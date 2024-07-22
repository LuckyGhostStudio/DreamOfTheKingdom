using UnityEngine;

public class Player : CharacterBase
{
    public int maxMana;

    public IntVariable mana;      // mana ֵ����
    public int CurrentMana { get => mana.currentValue; set => mana.SetValue(value); }   // ��ǰ Mana

    private void OnEnable()
    {
        mana.maxValue = maxMana;
        CurrentMana = mana.maxValue;
    }

    /// <summary>
    /// �»غ�
    /// </summary>
    public void NewTurn()
    {
        CurrentMana = maxMana;  // ���� Mana
    }

    /// <summary>
    /// ���� Mana
    /// </summary>
    /// <param name="cost">����ֵ</param>
    public void UpdateMana(int cost)
    {
        CurrentMana -= cost;
        if (CurrentMana <= 0)
        {
            CurrentMana = 0;
        }
    }
}
