using UnityEngine;

/// <summary>
/// �غϹ�����
/// </summary>
public class TurnBaseManager : MonoBehaviour
{
    private bool isPlayerTurn = false;  // ��һغ�
    private bool isEnemyTurn = false;   // ���˻غ�
    public bool battleEnd = true;       // ս������

    private float playerWaitTimeCounter;// ��ҵȴ���ʱ��
    public float playerWaitTime;        // ��ҵȴ�ʱ�䣨�ȴ������ȵȣ�
    
    private float enemyTurnTimeCounter; // ���˻غϼ�ʱ��
    public float enemyTurnDuration;     // ���˻غϳ���ʱ��

    [Header("��һغϿ�ʼ�¼��㲥")]
    public ObjectEventSO playerTurnBeginEvent;  // ��һغϿ�ʼ�¼�
    [Header("���˻غϿ�ʼ�¼��㲥")]
    public ObjectEventSO enemyTurnBeginEvent;   // ���˻غϿ�ʼ�¼�
    [Header("���˻غϽ����¼��㲥")]
    public ObjectEventSO enemyTurnEndEvent;     // ���˻غϽ����¼�

    private void Update()
    {
        if (battleEnd)
        {
            return;
        }

        // ���˻غ�
        if (isEnemyTurn)
        {
            enemyTurnTimeCounter += Time.deltaTime;
            if (enemyTurnTimeCounter >= enemyTurnDuration)   // ��ʱ����
            {
                enemyTurnTimeCounter = 0;

                EnemyTurnEnd();         // ���˻غϽ���

                isPlayerTurn = true;    // ��һغϿ�ʼ
            }
        }

        // ��һغ�
        if (isPlayerTurn)
        {
            playerWaitTimeCounter += Time.deltaTime;
            if (playerWaitTimeCounter >= playerWaitTime)  // ��ҵȴ�����
            {
                playerWaitTimeCounter = 0;

                PlayerTurnBegin();      // ��һغϿ�ʼ

                isPlayerTurn = false;
            }
        }
    }

    /// <summary>
    /// ��Ϸ��ʼ ��ʼ���غ�����
    /// </summary>
    [ContextMenu("Game Start")]
    public void GameStart()
    {
        isPlayerTurn = true;
        isEnemyTurn = false;

        battleEnd = false;
        
        playerWaitTimeCounter = 0.0f;
        enemyTurnTimeCounter = 0.0f;
    }

    /// <summary>
    /// ��һغϿ�ʼ
    /// </summary>
    public void PlayerTurnBegin()
    {
        playerTurnBeginEvent.RaiseEvent(null, this);    // ������һغϿ�ʼ�¼�
    }

    /// <summary>
    /// ���˻غϿ�ʼ
    /// </summary>
    public void EnemyTurnBegin()
    {
        isEnemyTurn = true;
        enemyTurnBeginEvent.RaiseEvent(null, this);     // �������˻غϿ�ʼ�¼�
    }

    /// <summary>
    /// ���˻غϽ���
    /// </summary>
    public void EnemyTurnEnd()
    {
        isEnemyTurn = false;
        enemyTurnEndEvent.RaiseEvent(null, this);       // �������˻غϽ����¼�
    }
}
