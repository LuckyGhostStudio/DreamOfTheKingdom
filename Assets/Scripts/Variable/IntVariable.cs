using UnityEngine;

[CreateAssetMenu(fileName = "IntVariable", menuName = "Variable/IntVariable")]
public class IntVariable : ScriptableObject
{
    public int maxValue;        // 最大值
    public int currentValue;    // 当前值

    public IntEventSO ValueChangedEvent;     // 值改变事件

    [TextArea]
    [SerializeField] private string description;

    /// <summary>
    /// 设置 Value
    /// </summary>
    /// <param name="value"></param>
    public void SetValue(int value)
    {
        currentValue = value;
        ValueChangedEvent?.RaiseEvent(value, this);  // 触发 Value 改变事件
    }
}
