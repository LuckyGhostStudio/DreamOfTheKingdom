using UnityEngine;

/// <summary>
/// 力量卡牌效果
/// </summary>
[CreateAssetMenu(fileName = "Strength Effect", menuName = "Card Effect/Strength Effect")]
public class StrengthEffect : Effect
{
    /// <summary>
    /// 执行效果
    /// </summary>
    /// <param name="from">发出者</param>
    /// <param name="target">目标</param>
    public override void Execute(CharacterBase from, CharacterBase target)
    {
        switch (targetType)
        {
            case EffectTargetType.Self:
                from.SetupStrength(value, true);    // 给自己添加 value 回合的力量 buff 效果
                Debug.Log("添加了 " + value + " 回合的力量 buff 效果");
                break;
            case EffectTargetType.Target:
                target.SetupStrength(value, false); // 给目标添加 value 回合的力量 debuff 效果
                Debug.Log("添加了 " + value + " 回合的力量 debuff 效果");
                break;
            default:
                break;
        }
    }
}
