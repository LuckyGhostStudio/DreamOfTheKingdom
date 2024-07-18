using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

/// <summary>
/// ��ͼ������
/// </summary>
public class MapGenerator : MonoBehaviour
{
    [Header("��ͼ���ñ�")]
    public MapConfigSO mapConfigData;   // ��ͼ��������

    [Header("��ͼ����")]
    public MapLayoutSO mapLayoutData;   // ��ͼ��������

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

    public List<RoomDataSO> roomDataList = new List<RoomDataSO>();  // ���������б�
    private Dictionary<RoomType, RoomDataSO> roomDataDict = new Dictionary<RoomType, RoomDataSO>();     // ��������-�������� �ֵ�

    private void Awake()
    {
        screenHeight = Camera.main.orthographicSize * 2;
        screenWidth = screenHeight * Camera.main.aspect;

        columnWidth = screenWidth / mapConfigData.roomBlueprints.Count;

        // ��ʼ�����������ֵ�
        foreach (RoomDataSO roomData in roomDataList)
        {
            roomDataDict.Add(roomData.roomType, roomData);
        }
    }

    //private void Start()
    //{
    //    CreateMap();
    //}

    private void OnEnable()
    {
        if (mapLayoutData.mapRoomDataList.Count > 0)
        {
            LoadMap();      // ���ص�ͼ
        }
        else
        {
            CreateMap();    // ������ͼ
        }
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
            int amount = UnityEngine.Random.Range(blueprint.min, blueprint.max);            // ÿ�з�����
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
                    newPosition.x = generatePoint.x + UnityEngine.Random.Range(-border / 2, border / 2);    // �����������ƫ��
                }

                newPosition.y = startHeight - lineHeight * i;                                       // ÿ�е�������
                Room room = Instantiate(roomPrefab, newPosition, Quaternion.identity, transform);   // ���ɷ���
                RoomType newType = GetRandomRoomType(mapConfigData.roomBlueprints[column].roomType);// ���ѡ��ǰ�еķ�������
                room.name = newType.ToString() + "Room";
                room.roomState = column == 0 ? RoomState.Attainable : RoomState.Locked;             // ���÷���״̬����һ�пɴ

                room.SetupRoom(column, i, GetRoomData(newType));                                    // ���÷�������

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

        SaveMap();  // �����ͼ
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
            Room targetRoom = ConnectToRandomRoom(room, column2Rooms, false);  // ��ǰ����������ӵ��ڶ����е�ĳ��
            connectedColumn2Rooms.Add(targetRoom);
        }

        // �ڶ���δ���ӵ��������ӵ���һ��
        foreach (var room in column2Rooms)
        {
            // �÷���δ����
            if (!connectedColumn2Rooms.Contains(room))
            {
                ConnectToRandomRoom(room, column1Rooms, true);    // ��ǰ����������ӵ���һ��
            }
        }
    }

    /// <summary>
    /// ��ǰ����������ӵ��ڶ��з����е�ĳ��
    /// </summary>
    /// <param name="room">��ǰ����</param>
    /// <param name="column2Rooms">�ڶ��з���</param>
    /// <param name="check">�Ƿ�Ϊ��������</param>
    /// <returns>���ӵ��ĵڶ����еķ���</returns>
    private Room ConnectToRandomRoom(Room room, List<Room> column2Rooms, bool check)
    {
        Room targetRoom;

        targetRoom = column2Rooms[UnityEngine.Random.Range(0, column2Rooms.Count)];     // �ڶ��������ѡ��һ������

        if (check)
        {
            targetRoom.nexts.Add(new Vector2Int(room.column, room.line));           // ��ӵ��ɵ���ķ����б�
        }
        else
        {
            room.nexts.Add(new Vector2Int(targetRoom.column, targetRoom.line));     // ��ӵ��ɵ���ķ����б�
        }

        LineRenderer line = Instantiate(linePrefab, transform);     // ��������
        line.SetPosition(0, room.transform.position);               // ������� room
        line.SetPosition(1, targetRoom.transform.position);         // �����յ� targetRoom

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

    /// <summary>
    /// ���ݷ������ͷ��ط�������
    /// </summary>
    /// <param name="roomType">��������</param>
    /// <returns>��������</returns>
    private RoomDataSO GetRoomData(RoomType roomType)
    {
        return roomDataDict[roomType];
    }

    /// <summary>
    /// ���������������
    /// </summary>
    /// <param name="flags">�������� Flags</param>
    /// <returns>��������</returns>
    private RoomType GetRandomRoomType(RoomType flags)
    {
        string[] options = flags.ToString().Split(',');                                 // ��������б�Ϊ string
        string randomTypeOption = options[UnityEngine.Random.Range(0, options.Length)]; // ���ѡ������
        return (RoomType)Enum.Parse(typeof(RoomType), randomTypeOption);                // ���ط�������
    }

    /// <summary>
    /// �����ͼ
    /// </summary>
    private void SaveMap()
    {
        mapLayoutData.mapRoomDataList = new List<MapRoomData>();    // ��ͼ�ķ��������б�

        // �����ͼ�����������ɵķ��������
        for (int i = 0; i < rooms.Count; i++)
        {
            MapRoomData mapRoomData = new MapRoomData();

            mapRoomData.posX = rooms[i].transform.position.x;
            mapRoomData.posY = rooms[i].transform.position.y;
            mapRoomData.column = rooms[i].column;
            mapRoomData.line = rooms[i].line;
            mapRoomData.roomData = rooms[i].roomData;
            mapRoomData.roomState = rooms[i].roomState;
            mapRoomData.nexts = rooms[i].nexts;

            mapLayoutData.mapRoomDataList.Add(mapRoomData);     // ��ӷ�������
        }

        mapLayoutData.linePositionList = new List<LinePosition>();  // ���������б�

        // ���淿��֮�����������
        for (int i = 0; i < lines.Count; i++)
        {
            LinePosition line = new LinePosition();

            line.startPos = new SerializeVector3(lines[i].GetPosition(0));
            line.endPos = new SerializeVector3(lines[i].GetPosition(1));

            mapLayoutData.linePositionList.Add(line);   // �����������
        }
    }

    /// <summary>
    /// ���ص�ͼ
    /// </summary>
    private void LoadMap()
    {
        // ���ɷ���
        for (int i = 0; i < mapLayoutData.mapRoomDataList.Count; i++)
        {
            MapRoomData mapRoomData = mapLayoutData.mapRoomDataList[i];

            Vector3 newPosition = new Vector3(mapRoomData.posX, mapRoomData.posY, 0);
            Room room = Instantiate(roomPrefab, newPosition, Quaternion.identity);  // ���ɷ���
            room.name = mapRoomData.roomData.roomType.ToString() + "Room";
            room.roomState = mapRoomData.roomState;
            room.SetupRoom(mapRoomData.column, mapRoomData.line, mapRoomData.roomData);
            room.nexts = mapRoomData.nexts;

            rooms.Add(room);    // ��ӵ��б�
        }

        // ��������
        for (int i = 0; i < mapLayoutData.linePositionList.Count; i++)
        {
            LinePosition linePos = mapLayoutData.linePositionList[i];

            LineRenderer line = Instantiate(linePrefab, transform);     // ��������
            line.SetPosition(0, linePos.startPos.ToVector3());          // �������
            line.SetPosition(1, linePos.endPos.ToVector3());          // �����յ�

            lines.Add(line);    // ��ӵ��б�
        }
    }
}
