using System.Collections;
using UnityEngine;

public class Enemy : CharacterBase
{
    public EnemyActionDataSO actionData;    // ��Ϊ����
    public EnemyAction currentAction;       // ��ǰ��Ϊ

    protected Player player;

    /// <summary>
    /// ��һغϿ�ʼʱ����
    /// </summary>
    public virtual void OnPlayerTurnBegin()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        currentAction = actionData.actions[Random.Range(0, actionData.actions.Count)];  // �����ȡ��Ϊ
        Debug.Log("��ǰ��Ϊ " + currentAction.effect.GetType().ToString());
    }

    /// <summary>
    /// ���˻غϿ�ʼʱ����
    /// </summary>
    public virtual void OnEnemyTurnBegin()
    {
        ResetDefense();
        // ����Ч������ ִ�ж�Ӧ��Ϊ
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
        //animator.SetTrigger("skill");               // ���ü��ܶ���
        //currentAction.effect.Execute(this, this);   // ���Լ�ִ�м���Ч��
        StartCoroutine(ProcessDelayAction("skill"));
    }

    public void Attack()
    {
        //animator.SetTrigger("attack");              // ���ù�������
        //currentAction.effect.Execute(this, player); // �������
        StartCoroutine(ProcessDelayAction("attack"));
    }

    /// <summary>
    /// �ӳ�ִ����Ϊ
    /// </summary>
    /// <param name="actionName">��Ϊ����</param>
    /// <returns></returns>
    IEnumerator ProcessDelayAction(string actionName)
    {
        animator.SetTrigger(actionName);            // ������Ϊ����
        // �ȴ�ֱ�� ����������� 60% �Ҳ��ڹ��ɹ�����
        yield return new WaitUntil(() =>
            animator.GetCurrentAnimatorStateInfo(0).IsName(actionName)
            && animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1.0f > 0.55f
            && !animator.IsInTransition(0)
        );
        
        if (actionName == "attack")
        {
            currentAction.effect.Execute(this, player); // �������
        }
        else if (actionName == "skill")
        {
            currentAction.effect.Execute(this, this);   // ���Լ�ִ�м���Ч��
        }
    }
}
