using UnityEngine;
using UnityEngine.EventSystems;

public class TreasureButton : MonoBehaviour, IPointerDownHandler
{
    [Header("游戏获胜事件广播")]
    public ObjectEventSO gameWinEvent;

    public void OnPointerDown(PointerEventData eventData)
    {
        gameWinEvent.RaiseEvent(null, this);    // 触发游戏获胜事件
    }
}
