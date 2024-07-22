using UnityEngine;

public class Player : CharacterBase
{
    public int maxMana;

    public IntVariable mana;      // mana 值类型
    public int CurrentMana { get => mana.currentValue; set => mana.SetValue(value); }   // 当前 Mana

    private void OnEnable()
    {
        mana.maxValue = maxMana;
        CurrentMana = mana.maxValue;
    }

    /// <summary>
    /// 新回合
    /// </summary>
    public void NewTurn()
    {
        CurrentMana = maxMana;  // 重置 Mana
    }

    /// <summary>
    /// 更新 Mana
    /// </summary>
    /// <param name="cost">花费值</param>
    public void UpdateMana(int cost)
    {
        CurrentMana -= cost;
        if (CurrentMana <= 0)
        {
            CurrentMana = 0;
        }
    }
}
