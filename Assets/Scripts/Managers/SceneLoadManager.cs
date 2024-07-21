using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    private AssetReference currentScene;    // 当前要加载的场景
    public AssetReference mapScene;         // Map 场景

    private Vector2Int currentRoomVector;    // 当前房间的坐标

    [Header("房间加载完成事件广播")]
    public ObjectEventSO afterRoomLoadedEvent;  // 房间加载完成事件

    /// <summary>
    /// 加载房间场景：房间加载事件监听
    /// </summary>
    /// <param name="data">数据</param>
    public async void OnLoadRoomEvent(object data)
    {
        if (data is Room)
        {
            Room currentRoom = data as Room;
            RoomDataSO currentRoomData = currentRoom.roomData;
            currentRoomVector = new Vector2Int(currentRoom.column, currentRoom.line);

            currentScene = currentRoomData.sceneToLoad;     // 当前房间的场景
        }

        await UnloadSceneTask();    // 卸载已激活的场景
        await LoadSceneTask();      // 加载场景

        afterRoomLoadedEvent.RaiseEvent(currentRoomVector, this);   // 触发房间加载完成事件
    }

    /// <summary>
    /// 异步加载场景任务
    /// </summary>
    /// <returns></returns>
    private async Awaitable LoadSceneTask()
    {
        var s = currentScene.LoadSceneAsync(LoadSceneMode.Additive);    // 异步加载场景
        await s.Task;   // 等待任务完成

        // 场景加载成功
        if (s.Status == AsyncOperationStatus.Succeeded)
        {
            SceneManager.SetActiveScene(s.Result.Scene);    // 激活场景
        }
    }

    /// <summary>
    /// 异步卸载已激活的场景任务
    /// </summary>
    /// <returns></returns>
    private async Awaitable UnloadSceneTask()
    {
        await SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());     // 异步卸载已激活的场景
    }

    /// <summary>
    /// 加载地图场景：Map 加载事件监听
    /// </summary>
    public async void LoadMap()
    {
        await UnloadSceneTask();    // 卸载已激活的场景
        currentScene = mapScene;
        await LoadSceneTask();      // 加载地图场景
    }
}
