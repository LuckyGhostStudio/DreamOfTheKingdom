using UnityEngine;

/// <summary>
/// ��ͼ������
/// </summary>
public class MapGenerator : MonoBehaviour
{
    public MapConfigSO mapConfigData;   // ��ͼ��������
    public Room roomPrefab;             // ���� Prefab

    private float screenHeight;     // ��Ļ�߶�
    private float screenWeight;     // ��Ļ���

    private float columnWeight;     // ÿ�з���ռ�Ŀ��
    private Vector3 generatePoint;  // ���ɵ�λ��


    private void Awake()
    {
        screenHeight = Camera.main.orthographicSize * 2;
        screenWeight = screenHeight * Camera.main.aspect;

        columnWeight = screenWeight / (mapConfigData.roomBlueprints.Count + 1);
    }

    public void CreateMap()
    {
        // ��ÿ�����ɷ���
        for (int column = 0; column < mapConfigData.roomBlueprints.Count; column++)
        {
            RoomBlueprint blueprint = mapConfigData.roomBlueprints[column];
            int amount = Random.Range(blueprint.min, blueprint.max);    // ÿ�з�����

            for (int i = 0; i < amount; i++)
            {
                GameObject room = Instantiate(roomPrefab.gameObject, transform);
                
            }
        }
    }
}
