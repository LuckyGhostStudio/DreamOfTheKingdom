using UnityEngine;

public class Room : MonoBehaviour
{
    public int column;  // 房间所在列
    public int line;    // 房间所在行

    private SpriteRenderer spriteRenderer;  // 图标 Sprite

    public RoomDataSO roomData;     // 房间数据
    public RoomState roomState;     // 房间状态

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
        Debug.Log("点击房间：" + roomData.roomType);
    }

    /// <summary>
    /// 创建房间时用来设置房间属性
    /// </summary>
    /// <param name="column">房间所在列</param>
    /// <param name="line">房间所在行</param>
    /// <param name="roomData">房间数据</param>
    private void SetupRoom(int column, int line, RoomDataSO roomData)
    {
        this.line = line;
        this.column = column;
        this.roomData = roomData;

        spriteRenderer.sprite = roomData.roomIcon;
    }
}
