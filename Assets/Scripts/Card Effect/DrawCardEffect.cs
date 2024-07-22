using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

/// <summary>
/// ����鿨�ƵĿ���Ч��
/// </summary>
[CreateAssetMenu(fileName = "DrawCard Effect", menuName = "Card Effect/DrawCard Effect")]
public class DrawCardEffect : Effect
{
    [Header("�鿨�¼��㲥")]
    public IntEventSO drawCardEvent;

    /// <summary>
    /// ִ��Ч��
    /// </summary>
    /// <param name="from">������</param>
    /// <param name="target">Ŀ��</param>
    public override void Execute(CharacterBase from, CharacterBase target)
    {
        drawCardEvent?.RaiseEvent(value, this);     // �����鿨�¼�
    }
}
