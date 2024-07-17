using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 事件监听基类
/// </summary>
/// <typeparam name="T"></typeparam>
public class BaseEventListener<T> : MonoBehaviour
{
    public BaseEventSO<T> eventSO;  // 监听的事件 SO
    public UnityEvent<T> response;  // 响应事件

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
    /// 触发响应事件
    /// </summary>
    /// <param name="value">事件数据</param>
    private void OnEventRaised(T value)
    {
        response?.Invoke(value);    // 触发响应事件
    }
}
