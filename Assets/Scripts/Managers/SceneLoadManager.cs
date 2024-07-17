using UnityEngine;

public class SceneLoadManager : MonoBehaviour
{
    /// <summary>
    /// 加载场景
    /// </summary>
    /// <param name="data">数据</param>
    public void OnLoadRoomEvent(object data)
    {
        if (data is RoomDataSO)
        {
            RoomDataSO currentData = (RoomDataSO)data;
            Debug.Log(currentData.roomType);
        }
    }
}
