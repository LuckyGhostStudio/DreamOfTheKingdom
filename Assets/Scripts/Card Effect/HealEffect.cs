using UnityEngine;

/// <summary>
/// 治愈效果
/// </summary>
[CreateAssetMenu(fileName = "Heal Effect", menuName = "Card Effect/Heal Effect")]
public class HealEffect : Effect
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
                from.HealHealth(value); // 恢复血量
                Debug.Log("恢复了 " + value + " 点血量");
                break;
            case EffectTargetType.Target:
                from.HealHealth(value); // 恢复血量
                Debug.Log("恢复了 " + value + " 点血量");
                break;
            default:
                break;
        }
    }
}
