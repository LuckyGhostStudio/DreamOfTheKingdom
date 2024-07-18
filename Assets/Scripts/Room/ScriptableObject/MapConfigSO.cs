using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 地图配置 SO
/// </summary>
[CreateAssetMenu(fileName = "MapConfigSO", menuName = "Map/MapConfigSO")]
public class MapConfigSO : ScriptableObject
{
    public List<RoomBlueprint> roomBlueprints;  // 房间模板列表
}

/// <summary>
/// 每列房间模板：用于生成每列房间
/// </summary>
[System.Serializable]
public class RoomBlueprint
{
    public int min, max;        // 最小房间数 最大房间数
    public RoomType roomType;   // 房间类型（多种类型）
}