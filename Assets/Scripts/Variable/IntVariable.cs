using UnityEngine;

[CreateAssetMenu(fileName = "IntVariable", menuName = "Variable/IntVariable")]
public class IntVariable : ScriptableObject
{
    public int maxValue;        // ���ֵ
    public int currentValue;    // ��ǰֵ

    public IntEventSO ValueChangedEvent;     // ֵ�ı��¼�

    [TextArea]
    [SerializeField] private string description;

    /// <summary>
    /// ���� Value
    /// </summary>
    /// <param name="value"></param>
    public void SetValue(int value)
    {
        currentValue = value;
        ValueChangedEvent?.RaiseEvent(value, this);  // ���� Value �ı��¼�
    }
}
