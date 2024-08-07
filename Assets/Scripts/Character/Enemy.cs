using System.Collections;
using UnityEngine;

public class Enemy : CharacterBase
{
    public EnemyActionDataSO actionData;    // 行为数据
    public EnemyAction currentAction;       // 当前行为

    protected Player player;

    /// <summary>
    /// 玩家回合开始时调用
    /// </summary>
    public virtual void OnPlayerTurnBegin()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        currentAction = actionData.actions[Random.Range(0, actionData.actions.Count)];  // 随机获取行为
        Debug.Log("当前行为 " + currentAction.effect.GetType().ToString());
    }

    /// <summary>
    /// 敌人回合开始时调用
    /// </summary>
    public virtual void OnEnemyTurnBegin()
    {
        ResetDefense();
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
        //animator.SetTrigger("skill");               // 设置技能动画
        //currentAction.effect.Execute(this, this);   // 给自己执行技能效果
        StartCoroutine(ProcessDelayAction("skill"));
    }

    public void Attack()
    {
        //animator.SetTrigger("attack");              // 设置攻击动画
        //currentAction.effect.Execute(this, player); // 攻击玩家
        StartCoroutine(ProcessDelayAction("attack"));
    }

    /// <summary>
    /// 延迟执行行为
    /// </summary>
    /// <param name="actionName">行为名称</param>
    /// <returns></returns>
    IEnumerator ProcessDelayAction(string actionName)
    {
        animator.SetTrigger(actionName);            // 设置行为动画
        // 等待直到 动画播放完成 60% 且不在过渡过程中
        yield return new WaitUntil(() =>
            animator.GetCurrentAnimatorStateInfo(0).IsName(actionName)
            && animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1.0f > 0.55f
            && !animator.IsInTransition(0)
        );
        
        if (actionName == "attack")
        {
            currentAction.effect.Execute(this, player); // 攻击玩家
        }
        else if (actionName == "skill")
        {
            currentAction.effect.Execute(this, this);   // 给自己执行技能效果
        }
    }
}
