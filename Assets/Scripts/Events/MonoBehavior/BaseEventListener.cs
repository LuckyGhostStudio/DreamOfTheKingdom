using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// �¼���������
/// </summary>
/// <typeparam name="T"></typeparam>
public class BaseEventListener<T> : MonoBehaviour
{
    public BaseEventSO<T> eventSO;  // �������¼� SO
    public UnityEvent<T> response;  // ��Ӧ�¼�

    private void OnEnable()
    {
        if (eventSO)
        {
            eventSO.OnEventRaised += OnEventRaised;
        }
    }

    private void OnDisable()
    {
        if (eventSO)
        {
            eventSO.OnEventRaised -= OnEventRaised;
        }
    }

    /// <summary>
    /// ������Ӧ�¼�
    /// </summary>
    /// <param name="value">�¼�����</param>
    private void OnEventRaised(T value)
    {
        response?.Invoke(value);    // ������Ӧ�¼�
    }
}
