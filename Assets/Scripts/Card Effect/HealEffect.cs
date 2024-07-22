using UnityEngine;

/// <summary>
/// ����Ч��
/// </summary>
[CreateAssetMenu(fileName = "Heal Effect", menuName = "Card Effect/Heal Effect")]
public class HealEffect : Effect
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
                from.HealHealth(value); // �ָ�Ѫ��
                Debug.Log("�ָ��� " + value + " ��Ѫ��");
                break;
            case EffectTargetType.Target:
                from.HealHealth(value); // �ָ�Ѫ��
                Debug.Log("�ָ��� " + value + " ��Ѫ��");
                break;
            default:
                break;
        }
    }
}
