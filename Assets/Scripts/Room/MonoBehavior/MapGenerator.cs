using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 地图生成器
/// </summary>
public class MapGenerator : MonoBehaviour
{
    public MapConfigSO mapConfigData;   // 地图配置数据
    public Room roomPrefab;             // 房间 Prefab

    private float screenHeight;     // 屏幕高度
    private float screenWidth;      // 屏幕宽度

    private float columnWidth;      // 每列房间占的宽度
    private Vector3 generatePoint;  // 生成点位置
    public float border;            // 边界预留距离

    private List<Room> rooms = new List<Room>();    // 房间列表

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
    /// 创建地图
    /// </summary>
    public void CreateMap()
    {
        // 对每列生成房间
        for (int column = 0; column < mapConfigData.roomBlueprints.Count; column++)
        {
            RoomBlueprint blueprint = mapConfigData.roomBlueprints[column];
            int amount = Random.Range(blueprint.min, blueprint.max);            // 每列房间数
            float startHeight = screenHeight / 2 - screenHeight / (amount + 1); // 第一行的高度 = 上半高度 - 行高度
            // 每列第一行生成点位置
            generatePoint = new Vector3(-screenWidth / 2 + border + columnWidth * column, startHeight, 0);
            
            Vector3 newPosition = generatePoint;

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
                    newPosition.x = generatePoint.x + Random.Range(-border / 2, border / 2);    // 随机设置左右偏移
                }

                newPosition.y = startHeight - lineHeight * i;                                       // 每行的纵坐标
                Room room = Instantiate(roomPrefab, newPosition, Quaternion.identity, transform);   // 生成房间
                rooms.Add(room);
            }
        }
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
        rooms.Clear();
        // 创建地图
        CreateMap();
    }
}
