using UnityEngine;

public class Room : MonoBehaviour
{
    public int column;  // ����������
    public int line;    // ����������

    private SpriteRenderer spriteRenderer;  // ͼ�� Sprite

    public RoomDataSO roomData;     // ��������
    public RoomState roomState;     // ����״̬

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
    }

    /// <summary>
    /// ��������ʱ�������÷�������
    /// </summary>
    /// <param name="column">����������</param>
    /// <param name="line">����������</param>
    /// <param name="roomData">��������</param>
    private void SetupRoom(int column, int line, RoomDataSO roomData)
    {
        this.line = line;
        this.column = column;
        this.roomData = roomData;

        spriteRenderer.sprite = roomData.roomIcon;
    }
}
