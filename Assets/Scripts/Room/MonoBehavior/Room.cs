using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 房间
/// </summary>
public class Room : MonoBehaviour
{
    public int column;  // 房间所在列
    public int line;    // 房间所在行

    private SpriteRenderer spriteRenderer;  // 图标 Sprite

    public RoomDataSO roomData;     // 房间数据
    public RoomState roomState;     // 房间状态

    public List<Vector2Int> nexts;  // 当前房间可到达的房间的位置列表

    [Header("广播")]
    public ObjectEventSO loadRoomEvent;   // 加载房间场景事件 SO

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
        Debug.Log("点击房间：" + roomData.roomType);
        // 房间可进入
        if (roomState == RoomState.Attainable)
        {
            loadRoomEvent.RaiseEvent(this, this);   // 触发加载房间事件
        }
    }

    /// <summary>
    /// 创建房间时用来设置房间属性
    /// </summary>
    /// <param name="column">房间所在列</param>
    /// <param name="line">房间所在行</param>
    /// <param name="roomData">房间数据</param>
    public void SetupRoom(int column, int line, RoomDataSO roomData)
    {
        this.line = line;
        this.column = column;
        this.roomData = roomData;

        spriteRenderer.sprite = roomData.roomIcon;
        // 设置不同状态时的颜色
        spriteRenderer.color = roomState switch
        {
            RoomState.Locked => new Color(0.5f, 0.5f, 0.5f, 1.0f),
            RoomState.Visited => new Color(0.5f, 0.8f, 0.8f, 0.9f),
            RoomState.Attainable => Color.white,
            _ => throw new System.NotImplementedException(),
        };
    }
}
