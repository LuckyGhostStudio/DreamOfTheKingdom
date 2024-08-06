using UnityEngine;

/// <summary>
/// 回合管理器
/// </summary>
public class TurnBaseManager : MonoBehaviour
{
    public GameObject player;

    private bool isPlayerTurn = false;  // 玩家回合
    private bool isEnemyTurn = false;   // 敌人回合
    public bool battleEnd = true;       // 战斗结束

    private float playerWaitTimeCounter;// 玩家等待计时器
    public float playerWaitTime;        // 玩家等待时间（等待动画等等）
    
    private float enemyTurnTimeCounter; // 敌人回合计时器
    public float enemyTurnDuration;     // 敌人回合持续时间

    [Header("玩家回合开始事件广播")]
    public ObjectEventSO playerTurnBeginEvent;  // 玩家回合开始事件
    [Header("敌人回合开始事件广播")]
    public ObjectEventSO enemyTurnBeginEvent;   // 敌人回合开始事件
    [Header("敌人回合结束事件广播")]
    public ObjectEventSO enemyTurnEndEvent;     // 敌人回合结束事件

    private void Update()
    {
        if (battleEnd)
        {
            return;
        }

        // 敌人回合
        if (isEnemyTurn)
        {
            enemyTurnTimeCounter += Time.deltaTime;
            if (enemyTurnTimeCounter >= enemyTurnDuration)   // 计时结束
            {
                enemyTurnTimeCounter = 0;

                EnemyTurnEnd();         // 敌人回合结束

                isPlayerTurn = true;    // 玩家回合开始
            }
        }

        // 玩家回合
        if (isPlayerTurn)
        {
            playerWaitTimeCounter += Time.deltaTime;
            if (playerWaitTimeCounter >= playerWaitTime)  // 玩家等待结束
            {
                playerWaitTimeCounter = 0;

                PlayerTurnBegin();      // 玩家回合开始

                isPlayerTurn = false;
            }
        }
    }

    /// <summary>
    /// 游戏开始 初始化回合数据
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
    /// 玩家回合开始
    /// </summary>
    public void PlayerTurnBegin()
    {
        playerTurnBeginEvent.RaiseEvent(null, this);    // 触发玩家回合开始事件
    }

    /// <summary>
    /// 敌人回合开始
    /// </summary>
    public void EnemyTurnBegin()
    {
        isEnemyTurn = true;
        enemyTurnBeginEvent.RaiseEvent(null, this);     // 触发敌人回合开始事件
    }

    /// <summary>
    /// 敌人回合结束
    /// </summary>
    public void EnemyTurnEnd()
    {
        isEnemyTurn = false;
        enemyTurnEndEvent.RaiseEvent(null, this);       // 触发敌人回合结束事件
    }

    /// <summary>
    /// 房间加载完成时调用
    /// </summary>
    /// <param name="obj"></param>
    public void OnRoomLoadedEvent(object obj)
    {
        Room room = obj as Room;
        switch (room.roomData.roomType)
        {
            case RoomType.MinorEnemy:
            case RoomType.EliteEnemy:
            case RoomType.Boss:
                player.SetActive(true);     // 启用 Player
                GameStart();                // 游戏开始 初始化回合
                break;
            case RoomType.Treasure:
            case RoomType.Shop:
                player.SetActive(false);    // 关闭 Player
                break;
            case RoomType.Rest:
                player.SetActive(true);     // 启用 Player
                player.GetComponent<PlayerAnimation>().SetSleepAnimation(); // 播放睡觉动画
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 停止回合系统
    /// </summary>
    public void StopTurnBaseSystem(object obj)
    {
        battleEnd = true;           // 对局结束
        player.SetActive(false);    // 关闭 player
    }

    public void NewGame()
    {
        player.GetComponent<Player>().Init();   // 初始化 Player
    }
}
