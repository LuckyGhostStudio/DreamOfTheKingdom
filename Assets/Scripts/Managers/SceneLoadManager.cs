using UnityEngine;

public class SceneLoadManager : MonoBehaviour
{
    /// <summary>
    /// ���س���
    /// </summary>
    /// <param name="data">����</param>
    public void OnLoadRoomEvent(object data)
    {
        if (data is RoomDataSO)
        {
            RoomDataSO currentData = (RoomDataSO)data;
            Debug.Log(currentData.roomType);
        }
    }
}
