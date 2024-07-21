using UnityEngine;

/// <summary>
/// Ч������
/// </summary>
public abstract class Effect : ScriptableObject
{
    public int value;                   // Ч����ֵ
    public EffectTargetType targetType; // Ŀ������

    /// <summary>
    /// ִ��Ч��
    /// </summary>
    /// <param name="from">������</param>
    /// <param name="target">Ŀ��</param>
    public abstract void Execute(CharacterBase from, CharacterBase target);
}
