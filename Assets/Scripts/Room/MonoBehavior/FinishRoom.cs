using UnityEngine;

public class FinishRoom : MonoBehaviour
{
    public ObjectEventSO loadMapEvent;  // ���� Map �¼�

    private void OnMouseDown()
    {
        loadMapEvent.RaiseEvent(null, this);    // �������� Map �����¼�
    }
}
