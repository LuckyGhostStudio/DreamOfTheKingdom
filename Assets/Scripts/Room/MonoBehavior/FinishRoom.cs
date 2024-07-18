using UnityEngine;

public class FinishRoom : MonoBehaviour
{
    public ObjectEventSO loadMapEvent;  // 加载 Map 事件

    private void OnMouseDown()
    {
        loadMapEvent.RaiseEvent(null, this);    // 触发加载 Map 场景事件
    }
}
