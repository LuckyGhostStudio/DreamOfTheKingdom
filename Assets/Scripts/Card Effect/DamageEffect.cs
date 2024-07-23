using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(fileName = "Damage Effect", menuName = "Card Effect/Damage Effect")]
public class DamageEffect : Effect
{
    /// <summary>
    /// ִ��Ч��
    /// </summary>
    /// <param name="from">������</param>
    /// <param name="target">Ŀ��</param>
    public override void Execute(CharacterBase from, CharacterBase target)
    {
        if (!target) return;

        switch (targetType)
        {
            case EffectTargetType.Target:
                int damage = (int)math.round(value * from.baseStrength);    // �����������ֵ����˺�
                target.TakeDamage(damage);  // �����˺�
                Debug.Log("����� " + damage + " ���˺�");
                break;
            case EffectTargetType.All:
                // �ҵ����е���
                foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    enemy.GetComponent<CharacterBase>().TakeDamage(value); // �����˺�
                }
                break;
            default:
                break;
        }
    }
}
