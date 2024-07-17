using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ͼ������
/// </summary>
public class MapGenerator : MonoBehaviour
{
    public MapConfigSO mapConfigData;   // ��ͼ��������
    public Room roomPrefab;             // ���� Prefab

    private float screenHeight;     // ��Ļ�߶�
    private float screenWidth;      // ��Ļ���

    private float columnWidth;      // ÿ�з���ռ�Ŀ��
    private Vector3 generatePoint;  // ���ɵ�λ��
    public float border;            // �߽�Ԥ������

    private List<Room> rooms = new List<Room>();    // �����б�

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
        // ��ÿ�����ɷ���
        for (int column = 0; column < mapConfigData.roomBlueprints.Count; column++)
        {
            RoomBlueprint blueprint = mapConfigData.roomBlueprints[column];
            int amount = Random.Range(blueprint.min, blueprint.max);            // ÿ�з�����
            float startHeight = screenHeight / 2 - screenHeight / (amount + 1); // ��һ�еĸ߶� = �ϰ�߶� - �и߶�
            // ÿ�е�һ�����ɵ�λ��
            generatePoint = new Vector3(-screenWidth / 2 + border + columnWidth * column, startHeight, 0);
            
            Vector3 newPosition = generatePoint;

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
            }
        }
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
        rooms.Clear();
        // ������ͼ
        CreateMap();
    }
}
