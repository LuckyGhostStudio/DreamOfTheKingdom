using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("地图布局")]
    public MapLayoutSO mapLayoutData;

    public List<Enemy> aliveEnemyList;

    [Header("游戏获胜事件广播")]
    public ObjectEventSO gameWinEvent;

    [Header("游戏失败事件广播")]
    public ObjectEventSO gameOverEvent;

    private void Awake()
    {
        mapLayoutData.mapRoomDataList.Clear();
        mapLayoutData.linePositionList.Clear();
    }

    /// <summary>
    /// 更新地图布局数据：房间加载完成事件监听
    /// </summary>
    /// <param name="roomVector">房间坐标数据</param>
    public void UpdateMapLayoutData(object data)
    {
        Vector2Int roomVector = (Vector2Int)data;

        if (mapLayoutData.mapRoomDataList.Count <= 0)
        {
            return;
        }
        // 查找与 roomVector 匹配的房间
        MapRoomData currentRoom = mapLayoutData.mapRoomDataList.Find(r => r.column == roomVector.x && r.line == roomVector.y);
        // 已访问
        currentRoom.roomState = RoomState.Visited;
        // 与当前房间同一列的所有房间
        List<MapRoomData> sameColumnRooms = mapLayoutData.mapRoomDataList.FindAll(r => r.column == currentRoom.column);

        // 将这一列房间都锁定
        foreach (var room in sameColumnRooms)
        {
            // 不是当前房间
            if (room.line != currentRoom.line)
            {
                room.roomState = RoomState.Locked;
            }
        }

        // 将当前房间连接到的下一个房间都激活
        foreach (var next in currentRoom.nexts)
        {
            var nextRoom = mapLayoutData.mapRoomDataList.Find(r => r.column == next.x && r.line == next.y);
            nextRoom.roomState = RoomState.Attainable;
        }

        aliveEnemyList.Clear(); // 清空敌人列表
    }

    /// <summary>
    /// 房间加载完成事件
    /// </summary>
    /// <param name="obj"></param>
    public void OnRoomLoadedEvent(object obj)
    {
        // 查找场景中所有敌人
        Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (var enemy in enemies)
        {
            aliveEnemyList.Add(enemy);  // 添加到敌人列表
        }
    }

    /// <summary>
    /// 角色死亡时调用
    /// </summary>
    /// <param name="character">角色</param>
    public void OnCharacterDeadEvent(object character)
    {
        if (character is Player)
        {
            Debug.Log("Player Dead.");
            StartCoroutine(GameFinishedEventDelayAction(gameOverEvent));    // 触发游戏结束事件
        }

        if (character is Enemy)
        {
            Debug.Log("Enemy Dead.");
            Enemy enemy = character as Enemy;

            aliveEnemyList.Remove(enemy);   // 移除敌人
            // 敌人全部死亡
            if (aliveEnemyList.Count <= 0)
            {
                StartCoroutine(GameFinishedEventDelayAction(gameWinEvent)); // 触发游戏获胜事件
            }
        }
        else if (character is Boos)
        {
            Debug.Log("通关.");
            StartCoroutine(GameFinishedEventDelayAction(gameOverEvent));    // 触发游戏结束事件
        }
    }

    /// <summary>
    /// 延迟触发游戏结束事件
    /// </summary>
    /// <param name="gameFinishedEvent">事件</param>
    /// <returns></returns>
    IEnumerator GameFinishedEventDelayAction(ObjectEventSO gameFinishedEvent)
    {
        yield return new WaitForSeconds(1.5f);
        gameFinishedEvent.RaiseEvent(null, this);
    }

    /// <summary>
    /// 新游戏开始时调用
    /// </summary>
    public void OnNewGameEvent()
    {
        mapLayoutData.mapRoomDataList.Clear();  // 清空地图房间数据
        mapLayoutData.linePositionList.Clear(); // 清空连线数据
    }
}
