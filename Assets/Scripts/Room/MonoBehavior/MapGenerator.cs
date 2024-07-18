using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

/// <summary>
/// 地图生成器
/// </summary>
public class MapGenerator : MonoBehaviour
{
    [Header("地图配置表")]
    public MapConfigSO mapConfigData;   // 地图配置数据

    [Header("地图布局")]
    public MapLayoutSO mapLayoutData;   // 地图布局数据

    [Header("预制体")]
    public Room roomPrefab;             // 房间 Prefab
    public LineRenderer linePrefab;     // Line Prefab

    private float screenHeight;     // 屏幕高度
    private float screenWidth;      // 屏幕宽度

    private float columnWidth;      // 每列房间占的宽度
    private Vector3 generatePoint;  // 生成点位置
    public float border;            // 边界预留距离

    private List<Room> rooms = new List<Room>();                    // 房间列表
    private List<LineRenderer> lines = new List<LineRenderer>();    // 连线列表

    public List<RoomDataSO> roomDataList = new List<RoomDataSO>();  // 房间数据列表
    private Dictionary<RoomType, RoomDataSO> roomDataDict = new Dictionary<RoomType, RoomDataSO>();     // 房间类型-房间数据 字典

    private void Awake()
    {
        screenHeight = Camera.main.orthographicSize * 2;
        screenWidth = screenHeight * Camera.main.aspect;

        columnWidth = screenWidth / mapConfigData.roomBlueprints.Count;

        // 初始化房间数据字典
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
            LoadMap();      // 加载地图
        }
        else
        {
            CreateMap();    // 创建地图
        }
    }

    /// <summary>
    /// 创建地图
    /// </summary>
    public void CreateMap()
    {
        List<Room> previousColumnRooms = new List<Room>();  // 前一列房间

        // 对每列生成房间
        for (int column = 0; column < mapConfigData.roomBlueprints.Count; column++)
        {
            RoomBlueprint blueprint = mapConfigData.roomBlueprints[column];
            int amount = UnityEngine.Random.Range(blueprint.min, blueprint.max);            // 每列房间数
            float startHeight = screenHeight / 2 - screenHeight / (amount + 1); // 第一行的高度 = 上半高度 - 行高度
            // 每列第一行生成点位置
            generatePoint = new Vector3(-screenWidth / 2 + border + columnWidth * column, startHeight, 0);
            
            Vector3 newPosition = generatePoint;

            List<Room> currentColumnRooms = new List<Room>();   // 当前列房间

            float lineHeight = screenHeight / (amount + 1);
            // 遍历当前列的每行
            for (int i = 0; i < amount; i++)
            {
                if (column == mapConfigData.roomBlueprints.Count - 1)
                {
                    newPosition.x = screenWidth / 2 - border * 2;   // 最后一列位置固定
                }
                else if (column != 0)
                {
                    newPosition.x = generatePoint.x + UnityEngine.Random.Range(-border / 2, border / 2);    // 随机设置左右偏移
                }

                newPosition.y = startHeight - lineHeight * i;                                       // 每行的纵坐标
                Room room = Instantiate(roomPrefab, newPosition, Quaternion.identity, transform);   // 生成房间
                RoomType newType = GetRandomRoomType(mapConfigData.roomBlueprints[column].roomType);// 随机选择当前列的房间类型
                room.name = newType.ToString() + "Room";
                room.roomState = column == 0 ? RoomState.Attainable : RoomState.Locked;             // 设置房间状态（第一列可达）

                room.SetupRoom(column, i, GetRoomData(newType));                                    // 设置房间数据

                rooms.Add(room);
                currentColumnRooms.Add(room);
            }

            // 不是第一列
            if (previousColumnRooms.Count > 0)
            {
                CreateConnections(previousColumnRooms, currentColumnRooms); // 创建连线
            }

            previousColumnRooms = currentColumnRooms;   // 是第一列 前一列为当前列
        }

        SaveMap();  // 保存地图
    }

    /// <summary>
    /// 创建两列房间之间的连接
    /// </summary>
    /// <param name="column1">第一列的房间</param>
    /// <param name="column2">第二列的房间</param>
    private void CreateConnections(List<Room> column1Rooms, List<Room> column2Rooms)
    {
        HashSet<Room> connectedColumn2Rooms = new HashSet<Room>();  // 第二列已连接的房间

        // 第一列随机连接到第二列
        foreach (var room in column1Rooms)
        {
            Room targetRoom = ConnectToRandomRoom(room, column2Rooms, false);  // 当前房间随机连接到第二列中的某个
            connectedColumn2Rooms.Add(targetRoom);
        }

        // 第二列未连接到房间连接到第一列
        foreach (var room in column2Rooms)
        {
            // 该房间未连接
            if (!connectedColumn2Rooms.Contains(room))
            {
                ConnectToRandomRoom(room, column1Rooms, true);    // 当前房间随机连接到第一列
            }
        }
    }

    /// <summary>
    /// 当前房间随机连接到第二列房间中的某个
    /// </summary>
    /// <param name="room">当前房间</param>
    /// <param name="column2Rooms">第二列房间</param>
    /// <param name="check">是否为反向连接</param>
    /// <returns>连接到的第二列中的房间</returns>
    private Room ConnectToRandomRoom(Room room, List<Room> column2Rooms, bool check)
    {
        Room targetRoom;

        targetRoom = column2Rooms[UnityEngine.Random.Range(0, column2Rooms.Count)];     // 第二列中随机选择一个房间

        if (check)
        {
            targetRoom.nexts.Add(new Vector2Int(room.column, room.line));           // 添加到可到达的房间列表
        }
        else
        {
            room.nexts.Add(new Vector2Int(targetRoom.column, targetRoom.line));     // 添加到可到达的房间列表
        }

        LineRenderer line = Instantiate(linePrefab, transform);     // 生成连线
        line.SetPosition(0, room.transform.position);               // 设置起点 room
        line.SetPosition(1, targetRoom.transform.position);         // 设置终点 targetRoom

        lines.Add(line);

        return targetRoom;
    }

    /// <summary>
    /// 重新生成房间
    /// </summary>
    [ContextMenu("ReGenerateRooms")]
    public void ReGenerateRooms()
    {
        // 销毁房间
        foreach (var room in rooms)
        {
            Destroy(room.gameObject);
        }
        // 销毁连线
        foreach (var line in lines)
        {
            Destroy(line.gameObject);
        }

        rooms.Clear();
        lines.Clear();

        // 创建地图
        CreateMap();
    }

    /// <summary>
    /// 根据房间类型返回房间数据
    /// </summary>
    /// <param name="roomType">房间类型</param>
    /// <returns>房间数据</returns>
    private RoomDataSO GetRoomData(RoomType roomType)
    {
        return roomDataDict[roomType];
    }

    /// <summary>
    /// 返回随机房间类型
    /// </summary>
    /// <param name="flags">房间类型 Flags</param>
    /// <returns>房间类型</returns>
    private RoomType GetRandomRoomType(RoomType flags)
    {
        string[] options = flags.ToString().Split(',');                                 // 拆分类型列表为 string
        string randomTypeOption = options[UnityEngine.Random.Range(0, options.Length)]; // 随机选择类型
        return (RoomType)Enum.Parse(typeof(RoomType), randomTypeOption);                // 返回房间类型
    }

    /// <summary>
    /// 保存地图
    /// </summary>
    private void SaveMap()
    {
        mapLayoutData.mapRoomDataList = new List<MapRoomData>();    // 地图的房间数据列表

        // 保存地图上所有已生成的房间的数据
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

            mapLayoutData.mapRoomDataList.Add(mapRoomData);     // 添加房间数据
        }

        mapLayoutData.linePositionList = new List<LinePosition>();  // 房间连线列表

        // 保存房间之间的连线数据
        for (int i = 0; i < lines.Count; i++)
        {
            LinePosition line = new LinePosition();

            line.startPos = new SerializeVector3(lines[i].GetPosition(0));
            line.endPos = new SerializeVector3(lines[i].GetPosition(1));

            mapLayoutData.linePositionList.Add(line);   // 添加连线数据
        }
    }

    /// <summary>
    /// 加载地图
    /// </summary>
    private void LoadMap()
    {
        // 生成房间
        for (int i = 0; i < mapLayoutData.mapRoomDataList.Count; i++)
        {
            MapRoomData mapRoomData = mapLayoutData.mapRoomDataList[i];

            Vector3 newPosition = new Vector3(mapRoomData.posX, mapRoomData.posY, 0);
            Room room = Instantiate(roomPrefab, newPosition, Quaternion.identity);  // 生成房间
            room.name = mapRoomData.roomData.roomType.ToString() + "Room";
            room.roomState = mapRoomData.roomState;
            room.SetupRoom(mapRoomData.column, mapRoomData.line, mapRoomData.roomData);
            room.nexts = mapRoomData.nexts;

            rooms.Add(room);    // 添加到列表
        }

        // 生成连线
        for (int i = 0; i < mapLayoutData.linePositionList.Count; i++)
        {
            LinePosition linePos = mapLayoutData.linePositionList[i];

            LineRenderer line = Instantiate(linePrefab, transform);     // 生成连线
            line.SetPosition(0, linePos.startPos.ToVector3());          // 设置起点
            line.SetPosition(1, linePos.endPos.ToVector3());          // 设置终点

            lines.Add(line);    // 添加到列表
        }
    }
}
