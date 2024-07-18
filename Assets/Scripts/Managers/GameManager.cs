using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("地图布局")]
    public MapLayoutSO mapLayoutData;

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
    }
}
