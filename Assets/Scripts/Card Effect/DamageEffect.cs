using UnityEngine;

[CreateAssetMenu(fileName = "Damage Effect", menuName = "Card Effect/Damage Effect")]
public class DamageEffect : Effect
{
    /// <summary>
    /// 执行效果
    /// </summary>
    /// <param name="from">发出者</param>
    /// <param name="target">目标</param>
    public override void Execute(CharacterBase from, CharacterBase target)
    {
        if (!target) return;

        switch (targetType)
        {
            case EffectTargetType.Target:
                target.TakeDamage(value);   // 计算伤害
                Debug.Log("造成了 " + value + " 点伤害");
                break;
            case EffectTargetType.All:
                // 找到所有敌人
                foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    enemy.GetComponent<CharacterBase>().TakeDamage(value); // 计算伤害
                }
                break;
            default:
                break;
        }
    }
}
