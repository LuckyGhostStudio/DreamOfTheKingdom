using System.Collections.Generic;
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

    public List<Vector2Int> nexts;  // ��ǰ����ɵ���ķ����λ���б�

    [Header("�㲥")]
    public ObjectEventSO loadRoomEvent;   // ���ط��䳡���¼� SO

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnMouseEnter()
    {
        transform.localScale *= 1.2f;
    }

    private void OnMouseExit()
    {
        transform.localScale /= 1.2f;
    }

    private void OnMouseDown()
    {
        Debug.Log("������䣺" + roomData.roomType);
        // ����ɽ���
        if (roomState == RoomState.Attainable)
        {
            loadRoomEvent.RaiseEvent(this, this);   // �������ط����¼�
        }
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
        // ���ò�ͬ״̬ʱ����ɫ
        spriteRenderer.color = roomState switch
        {
            RoomState.Locked => new Color(0.5f, 0.5f, 0.5f, 1.0f),
            RoomState.Visited => new Color(0.5f, 0.8f, 0.8f, 0.9f),
            RoomState.Attainable => Color.white,
            _ => throw new System.NotImplementedException(),
        };
    }
}
