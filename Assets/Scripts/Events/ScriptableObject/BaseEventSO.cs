using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// �¼� SO ����
/// </summary>
/// <typeparam name="T">�¼�����</typeparam>
public class BaseEventSO<T> : ScriptableObject
{
    [TextArea]
    public string description;              // �¼�����

    public UnityAction<T> OnEventRaised;    // �¼� Action

    public string lastSender;               // ���һ�������¼��Ķ���

    /// <summary>
    /// �����¼�
    /// </summary>
    /// <param name="value">�¼�����</param>
    /// <param name="sender">�����¼��Ķ���</param>
    public void RaiseEvent(T value, object sender)
    {
        OnEventRaised?.Invoke(value);   // �����¼�
        lastSender = sender?.ToString();
    }
}
