using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ͼ������
/// </summary>
public class MapGenerator : MonoBehaviour
{
    [Header("��ͼ���ñ�")]
    public MapConfigSO mapConfigData;   // ��ͼ��������

    [Header("Ԥ����")]
    public Room roomPrefab;             // ���� Prefab
    public LineRenderer linePrefab;     // Line Prefab

    private float screenHeight;     // ��Ļ�߶�
    private float screenWidth;      // ��Ļ���

    private float columnWidth;      // ÿ�з���ռ�Ŀ��
    private Vector3 generatePoint;  // ���ɵ�λ��
    public float border;            // �߽�Ԥ������

    private List<Room> rooms = new List<Room>();                    // �����б�
    private List<LineRenderer> lines = new List<LineRenderer>();    // �����б�

    private void Awake()
    {
        screenHeight = Camera.main.orthographicSize * 2;
        screenWidth = screenHeight * Camera.main.aspect;

        columnWidth = screenWidth / mapConfigData.roomBlueprints.Count;
    }

    private void Start()
    {
        CreateMap();
    }

    /// <summary>
    /// ������ͼ
    /// </summary>
    public void CreateMap()
    {
        List<Room> previousColumnRooms = new List<Room>();  // ǰһ�з���

        // ��ÿ�����ɷ���
        for (int column = 0; column < mapConfigData.roomBlueprints.Count; column++)
        {
            RoomBlueprint blueprint = mapConfigData.roomBlueprints[column];
            int amount = Random.Range(blueprint.min, blueprint.max);            // ÿ�з�����
            float startHeight = screenHeight / 2 - screenHeight / (amount + 1); // ��һ�еĸ߶� = �ϰ�߶� - �и߶�
            // ÿ�е�һ�����ɵ�λ��
            generatePoint = new Vector3(-screenWidth / 2 + border + columnWidth * column, startHeight, 0);
            
            Vector3 newPosition = generatePoint;

            List<Room> currentColumnRooms = new List<Room>();   // ��ǰ�з���

            float lineHeight = screenHeight / (amount + 1);
            // ������ǰ�е�ÿ��
            for (int i = 0; i < amount; i++)
            {
                if (column == mapConfigData.roomBlueprints.Count - 1)
                {
                    newPosition.x = screenWidth / 2 - border * 2;   // ���һ��λ�ù̶�
                }
                else if (column != 0)
                {
                    newPosition.x = generatePoint.x + Random.Range(-border / 2, border / 2);    // �����������ƫ��
                }

                newPosition.y = startHeight - lineHeight * i;                                       // ÿ�е�������
                Room room = Instantiate(roomPrefab, newPosition, Quaternion.identity, transform);   // ���ɷ���

                rooms.Add(room);
                currentColumnRooms.Add(room);
            }

            // ���ǵ�һ��
            if (previousColumnRooms.Count > 0)
            {
                CreateConnections(previousColumnRooms, currentColumnRooms); // ��������
            }

            previousColumnRooms = currentColumnRooms;   // �ǵ�һ�� ǰһ��Ϊ��ǰ��
        }
    }

    /// <summary>
    /// �������з���֮�������
    /// </summary>
    /// <param name="column1">��һ�еķ���</param>
    /// <param name="column2">�ڶ��еķ���</param>
    private void CreateConnections(List<Room> column1Rooms, List<Room> column2Rooms)
    {
        HashSet<Room> connectedColumn2Rooms = new HashSet<Room>();  // �ڶ��������ӵķ���

        // ��һ��������ӵ��ڶ���
        foreach (var room in column1Rooms)
        {
            Room targetRoom = ConnectToRandomRoom(room, column2Rooms);  // ��ǰ����������ӵ��ڶ����е�ĳ��
            connectedColumn2Rooms.Add(targetRoom);
        }

        // �ڶ���δ���ӵ��������ӵ���һ��
        foreach (var room in column2Rooms)
        {
            // �÷���δ����
            if (!connectedColumn2Rooms.Contains(room))
            {
                ConnectToRandomRoom(room, column1Rooms);    // ��ǰ����������ӵ���һ��
            }
        }
    }

    /// <summary>
    /// ��ǰ����������ӵ��ڶ��з����е�ĳ��
    /// </summary>
    /// <param name="room">��ǰ����</param>
    /// <param name="column2Rooms">�ڶ��з���</param>
    /// <param name="forward">�Ƿ�Ϊ��������</param>
    /// <returns>���ӵ��ĵڶ����еķ���</returns>
    private Room ConnectToRandomRoom(Room room, List<Room> column2Rooms)
    {
        Room targetRoom;

        targetRoom = column2Rooms[Random.Range(0, column2Rooms.Count)];     // �ڶ��������ѡ��һ������

        LineRenderer line = Instantiate(linePrefab, transform);     // ��������
        line.SetPosition(0, room.transform.position);           // ������� room
        line.SetPosition(1, targetRoom.transform.position);     // �����յ� targetRoom

        lines.Add(line);

        return targetRoom;
    }

    /// <summary>
    /// �������ɷ���
    /// </summary>
    [ContextMenu("ReGenerateRooms")]
    public void ReGenerateRooms()
    {
        // ���ٷ���
        foreach (var room in rooms)
        {
            Destroy(room.gameObject);
        }
        // ��������
        foreach (var line in lines)
        {
            Destroy(line.gameObject);
        }

        rooms.Clear();
        lines.Clear();

        // ������ͼ
        CreateMap();
    }
}
