using UnityEngine;

/// <summary>
/// ����Ч��
/// </summary>
[CreateAssetMenu(fileName = "Strength Effect", menuName = "Card Effect/Strength Effect")]
public class StrengthEffect : Effect
{
    /// <summary>
    /// ִ��Ч��
    /// </summary>
    /// <param name="from">������</param>
    /// <param name="target">Ŀ��</param>
    public override void Execute(CharacterBase from, CharacterBase target)
    {
        switch (targetType)
        {
            case EffectTargetType.Self:
                from.SetupStrength(value, true); // ��� value �غϵ����� buff Ч��
                Debug.Log("����� " + value + " �غϵ����� buff Ч��");
                break;
            case EffectTargetType.Target:
                from.SetupStrength(value, false); // ��� value �غϵ����� debuff Ч��
                Debug.Log("����� " + value + " �غϵ����� debuff Ч��");
                break;
            default:
                break;
        }
    }
}
