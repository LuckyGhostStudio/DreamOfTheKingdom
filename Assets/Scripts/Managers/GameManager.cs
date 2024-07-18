using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("��ͼ����")]
    public MapLayoutSO mapLayoutData;

    private void Awake()
    {
        mapLayoutData.mapRoomDataList.Clear();
        mapLayoutData.linePositionList.Clear();
    }

    /// <summary>
    /// ���µ�ͼ�������ݣ������������¼�����
    /// </summary>
    /// <param name="roomVector">������������</param>
    public void UpdateMapLayoutData(object data)
    {
        Vector2Int roomVector = (Vector2Int)data;

        // ������ roomVector ƥ��ķ���
        MapRoomData currentRoom = mapLayoutData.mapRoomDataList.Find(r => r.column == roomVector.x && r.line == roomVector.y);
        // �ѷ���
        currentRoom.roomState = RoomState.Visited;
        // �뵱ǰ����ͬһ�е����з���
        List<MapRoomData> sameColumnRooms = mapLayoutData.mapRoomDataList.FindAll(r => r.column == currentRoom.column);

        // ����һ�з��䶼����
        foreach (var room in sameColumnRooms)
        {
            // ���ǵ�ǰ����
            if (room.line != currentRoom.line)
            {
                room.roomState = RoomState.Locked;
            }
        }

        // ����ǰ�������ӵ�����һ�����䶼����
        foreach (var next in currentRoom.nexts)
        {
            var nextRoom = mapLayoutData.mapRoomDataList.Find(r => r.column == next.x && r.line == next.y);
            nextRoom.roomState = RoomState.Attainable;
        }
    }
}
