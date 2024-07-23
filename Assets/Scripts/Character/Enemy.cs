using UnityEngine;

public class Enemy : CharacterBase
{
    public EnemyActionDataSO actionData;    // ��Ϊ����
    public EnemyAction currentAction;       // ��ǰ��Ϊ

    protected Player player;

    protected override void Awake()
    {
        base.Awake();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    /// <summary>
    /// ��һغϿ�ʼʱ����
    /// </summary>
    public virtual void OnPlayerTurnBegin()
    {
        currentAction = actionData.actions[Random.Range(0, actionData.actions.Count)];  // �����ȡ��Ϊ
        Debug.Log("��ǰ��Ϊ " + currentAction.effect.GetType().ToString());
    }

    /// <summary>
    /// ���˻غϿ�ʼʱ����
    /// </summary>
    public virtual void OnEnemyTurnBegin()
    {
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
        currentAction.effect.Execute(this, this);   // ���Լ�ִ�м���Ч��
    }

    public void Attack()
    {
        currentAction.effect.Execute(this, player); // �������
    }
}
