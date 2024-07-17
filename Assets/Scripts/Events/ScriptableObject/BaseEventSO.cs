using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 事件 SO 基类
/// </summary>
/// <typeparam name="T">事件类型</typeparam>
public class BaseEventSO<T> : ScriptableObject
{
    [TextArea]
    public string description;              // 事件描述

    public UnityAction<T> OnEventRaised;    // 事件 Action

    public string lastSender;               // 最后一个触发事件的对象

    /// <summary>
    /// 触发事件
    /// </summary>
    /// <param name="value">事件数据</param>
    /// <param name="sender">触发事件的对象</param>
    public void RaiseEvent(T value, object sender)
    {
        OnEventRaised?.Invoke(value);   // 触发事件
        lastSender = sender?.ToString();
    }
}
