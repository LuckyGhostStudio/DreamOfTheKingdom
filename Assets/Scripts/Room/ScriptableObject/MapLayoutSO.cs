using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 地图布局 SO
/// </summary>
[CreateAssetMenu(fileName = "MapLayoutSO", menuName = "Map/MapLayoutSO")]
public class MapLayoutSO : ScriptableObject
{
    public List<MapRoomData> mapRoomDataList = new List<MapRoomData>();     // 地图房间列表
    public List<LinePosition> linePositionList = new List<LinePosition>();  // 房间连线列表
}

/// <summary>
/// 地图上房间的数据：为保存地图数据
/// </summary>
[System.Serializable]
public class MapRoomData
{
    public float posX, posY;        // 房间坐标
    public int column, line;        // 列行
    public RoomDataSO roomData;     // 房间数据
    public RoomState roomState;     // 房间状态
}

/// <summary>
/// 房间之间的连线位置
/// </summary>
[System.Serializable]
public class LinePosition
{
    public SerializeVector3 startPos;
    public SerializeVector3 endPos;
}