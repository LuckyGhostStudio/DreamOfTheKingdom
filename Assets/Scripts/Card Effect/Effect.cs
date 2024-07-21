using UnityEngine;

/// <summary>
/// 效果基类
/// </summary>
public abstract class Effect : ScriptableObject
{
    public int value;                   // 效果数值
    public EffectTargetType targetType; // 目标类型

    /// <summary>
    /// 执行效果
    /// </summary>
    /// <param name="from">发出者</param>
    /// <param name="target">目标</param>
    public abstract void Execute(CharacterBase from, CharacterBase target);
}
