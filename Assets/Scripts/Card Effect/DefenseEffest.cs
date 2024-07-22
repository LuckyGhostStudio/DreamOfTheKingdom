using UnityEngine;

[CreateAssetMenu(fileName = "Defense Effect", menuName = "Card Effect/Defense Effect")]
public class DefenseEffest : Effect
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
                from.IncreaseDefense(value);  // 增加防御值
                Debug.Log("添加了 " + value + " 点防御值");
                break;
            case EffectTargetType.Target:
                from.IncreaseDefense(value);  // 增加防御值
                Debug.Log("添加了 " + value + " 点防御值");
                break;
            default:
                break;
        }
    }
}
