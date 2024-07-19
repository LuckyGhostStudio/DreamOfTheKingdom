using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ͼ���� SO
/// </summary>
[CreateAssetMenu(fileName = "MapLayoutSO", menuName = "Map/MapLayoutSO")]
public class MapLayoutSO : ScriptableObject
{
    public List<MapRoomData> mapRoomDataList = new List<MapRoomData>();     // ��ͼ�����б�
    public List<LinePosition> linePositionList = new List<LinePosition>();  // ���������б�
}

/// <summary>
/// ��ͼ�Ϸ�������ݣ�Ϊ�����ͼ����
/// </summary>
[System.Serializable]
public class MapRoomData
{
    public float posX, posY;        // ��������
    public int column, line;        // ����
    public RoomDataSO roomData;     // ��������
    public RoomState roomState;     // ����״̬
    public List<Vector2Int> nexts;  // ��ǰ����ɵ���ķ��������б�
}

/// <summary>
/// ����֮�������λ��
/// </summary>
[System.Serializable]
public class LinePosition
{
    public SerializeVector3 startPos;   // ���
    public SerializeVector3 endPos;     // �յ�
}