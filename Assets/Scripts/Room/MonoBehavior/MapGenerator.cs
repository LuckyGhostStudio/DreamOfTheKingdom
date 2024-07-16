using UnityEngine;

/// <summary>
/// 地图生成器
/// </summary>
public class MapGenerator : MonoBehaviour
{
    public MapConfigSO mapConfigData;   // 地图配置数据
    public Room roomPrefab;             // 房间 Prefab

    private float screenHeight;     // 屏幕高度
    private float screenWeight;     // 屏幕宽度

    private float columnWeight;     // 每列房间占的宽度
    private Vector3 generatePoint;  // 生成点位置


    private void Awake()
    {
        screenHeight = Camera.main.orthographicSize * 2;
        screenWeight = screenHeight * Camera.main.aspect;

        columnWeight = screenWeight / (mapConfigData.roomBlueprints.Count + 1);
    }

    public void CreateMap()
    {
        // 对每列生成房间
        for (int column = 0; column < mapConfigData.roomBlueprints.Count; column++)
        {
            RoomBlueprint blueprint = mapConfigData.roomBlueprints[column];
            int amount = Random.Range(blueprint.min, blueprint.max);    // 每列房间数

            for (int i = 0; i < amount; i++)
            {
                GameObject room = Instantiate(roomPrefab.gameObject, transform);
                
            }
        }
    }
}
