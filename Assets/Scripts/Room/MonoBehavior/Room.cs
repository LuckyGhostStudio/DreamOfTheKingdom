using UnityEngine;

/// <summary>
/// ����
/// </summary>
public class Room : MonoBehaviour
{
    public int column;  // ����������
    public int line;    // ����������

    private SpriteRenderer spriteRenderer;  // ͼ�� Sprite

    public RoomDataSO roomData;     // ��������
    public RoomState roomState;     // ����״̬

    [Header("�㲥")]
    public ObjectEventSO loadRoomEvent;   // ���ط��䳡���¼� SO

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        SetupRoom(0, 0, roomData);
    }

    private void OnMouseDown()
    {
        Debug.Log("������䣺" + roomData.roomType);

        loadRoomEvent.RaiseEvent(roomData, this);   // �������ط����¼�
    }

    /// <summary>
    /// ��������ʱ�������÷�������
    /// </summary>
    /// <param name="column">����������</param>
    /// <param name="line">����������</param>
    /// <param name="roomData">��������</param>
    public void SetupRoom(int column, int line, RoomDataSO roomData)
    {
        this.line = line;
        this.column = column;
        this.roomData = roomData;

        spriteRenderer.sprite = roomData.roomIcon;
    }
}
