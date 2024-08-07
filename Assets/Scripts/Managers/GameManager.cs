using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("��ͼ����")]
    public MapLayoutSO mapLayoutData;

    public List<Enemy> aliveEnemyList;

    [Header("��Ϸ��ʤ�¼��㲥")]
    public ObjectEventSO gameWinEvent;

    [Header("��Ϸʧ���¼��㲥")]
    public ObjectEventSO gameOverEvent;

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

        if (mapLayoutData.mapRoomDataList.Count <= 0)
        {
            return;
        }
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

        aliveEnemyList.Clear(); // ��յ����б�
    }

    /// <summary>
    /// �����������¼�
    /// </summary>
    /// <param name="obj"></param>
    public void OnRoomLoadedEvent(object obj)
    {
        // ���ҳ��������е���
        Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (var enemy in enemies)
        {
            aliveEnemyList.Add(enemy);  // ��ӵ������б�
        }
    }

    /// <summary>
    /// ��ɫ����ʱ����
    /// </summary>
    /// <param name="character">��ɫ</param>
    public void OnCharacterDeadEvent(object character)
    {
        if (character is Player)
        {
            Debug.Log("Player Dead.");
            StartCoroutine(GameFinishedEventDelayAction(gameOverEvent));    // ������Ϸ�����¼�
        }

        if (character is Enemy)
        {
            Debug.Log("Enemy Dead.");
            Enemy enemy = character as Enemy;

            aliveEnemyList.Remove(enemy);   // �Ƴ�����
            // ����ȫ������
            if (aliveEnemyList.Count <= 0)
            {
                StartCoroutine(GameFinishedEventDelayAction(gameWinEvent)); // ������Ϸ��ʤ�¼�
            }
        }
        else if (character is Boos)
        {
            Debug.Log("ͨ��.");
            StartCoroutine(GameFinishedEventDelayAction(gameOverEvent));    // ������Ϸ�����¼�
        }
    }

    /// <summary>
    /// �ӳٴ�����Ϸ�����¼�
    /// </summary>
    /// <param name="gameFinishedEvent">�¼�</param>
    /// <returns></returns>
    IEnumerator GameFinishedEventDelayAction(ObjectEventSO gameFinishedEvent)
    {
        yield return new WaitForSeconds(1.5f);
        gameFinishedEvent.RaiseEvent(null, this);
    }

    /// <summary>
    /// ����Ϸ��ʼʱ����
    /// </summary>
    public void OnNewGameEvent()
    {
        mapLayoutData.mapRoomDataList.Clear();  // ��յ�ͼ��������
        mapLayoutData.linePositionList.Clear(); // �����������
    }
}
