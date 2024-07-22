using UnityEngine;

[CreateAssetMenu(fileName = "Defense Effect", menuName = "Card Effect/Defense Effect")]
public class DefenseEffest : Effect
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
                from.IncreaseDefense(value);  // ���ӷ���ֵ
                Debug.Log("����� " + value + " �����ֵ");
                break;
            case EffectTargetType.Target:
                from.IncreaseDefense(value);  // ���ӷ���ֵ
                Debug.Log("����� " + value + " �����ֵ");
                break;
            default:
                break;
        }
    }
}
