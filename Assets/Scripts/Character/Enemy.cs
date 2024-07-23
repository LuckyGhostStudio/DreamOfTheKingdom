using UnityEngine;

public class Enemy : CharacterBase
{
    public EnemyActionDataSO actionData;    // 行为数据
    public EnemyAction currentAction;       // 当前行为

    protected Player player;

    protected override void Awake()
    {
        base.Awake();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    /// <summary>
    /// 玩家回合开始时调用
    /// </summary>
    public virtual void OnPlayerTurnBegin()
    {
        currentAction = actionData.actions[Random.Range(0, actionData.actions.Count)];  // 随机获取行为
        Debug.Log("当前行为 " + currentAction.effect.GetType().ToString());
    }

    /// <summary>
    /// 敌人回合开始时调用
    /// </summary>
    public virtual void OnEnemyTurnBegin()
    {
        // 根据效果类型 执行对应行为
        switch (currentAction.effect.targetType)
        {
            case EffectTargetType.Self:
                Skill();
                break;
            case EffectTargetType.Target:
                Attack();
                break;
            case EffectTargetType.All:
                break;
            default:
                break;
        }
    }

    public void Skill()
    {
        currentAction.effect.Execute(this, this);   // 给自己执行技能效果
    }

    public void Attack()
    {
        currentAction.effect.Execute(this, player); // 攻击玩家
    }
}
