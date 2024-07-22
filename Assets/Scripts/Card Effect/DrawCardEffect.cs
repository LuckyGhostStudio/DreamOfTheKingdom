using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

/// <summary>
/// 随机抽卡牌的卡牌效果
/// </summary>
[CreateAssetMenu(fileName = "DrawCard Effect", menuName = "Card Effect/DrawCard Effect")]
public class DrawCardEffect : Effect
{
    [Header("抽卡事件广播")]
    public IntEventSO drawCardEvent;

    /// <summary>
    /// 执行效果
    /// </summary>
    /// <param name="from">发出者</param>
    /// <param name="target">目标</param>
    public override void Execute(CharacterBase from, CharacterBase target)
    {
        drawCardEvent?.RaiseEvent(value, this);     // 触发抽卡事件
    }
}
