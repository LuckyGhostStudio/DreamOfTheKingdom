using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ͼ���� SO
/// </summary>
[CreateAssetMenu(fileName = "MapConfigSO", menuName = "Map/MapConfigSO")]
public class MapConfigSO : ScriptableObject
{
    public List<RoomBlueprint> roomBlueprints;  // ����ģ���б�
}

/// <summary>
/// ÿ�з���ģ�壺��������ÿ�з���
/// </summary>
[System.Serializable]
public class RoomBlueprint
{
    public int min, max;        // ��С������ ��󷿼���
    public RoomType roomType;   // �������ͣ��������ͣ�
}