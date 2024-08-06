using UnityEngine;
using UnityEngine.EventSystems;

public class TreasureButton : MonoBehaviour, IPointerDownHandler
{
    [Header("��Ϸ��ʤ�¼��㲥")]
    public ObjectEventSO gameWinEvent;

    public void OnPointerDown(PointerEventData eventData)
    {
        gameWinEvent.RaiseEvent(null, this);    // ������Ϸ��ʤ�¼�
    }
}
