using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    private AssetReference currentScene;    // 当前要加载的场景
    public AssetReference mapScene;         // Map 场景
    public AssetReference menuScecne;       // Menu 场景

    private Vector2Int currentRoomVector;   // 当前房间的坐标
    private Room currentRoom;

    [Header("房间加载完成事件广播")]
    public ObjectEventSO afterRoomLoadedEvent;  // 房间加载完成事件
    [Header("更新房间状态事件广播")]
    public ObjectEventSO updateRoomStateEvent;  // 更新房间状态事件

    private void Start()
    {
        currentRoomVector = -Vector2Int.one;
        LoadMenu();     // 加载主菜单场景
    }

    /// <summary>
    /// 加载房间场景：房间加载事件监听
    /// </summary>
    /// <param name="data">数据</param>
    public async void OnLoadRoomEvent(object data)
    {
        if (data is Room)
        {
            currentRoom = data as Room;
            RoomDataSO currentRoomData = currentRoom.roomData;
            currentRoomVector = new Vector2Int(currentRoom.column, currentRoom.line);

            currentScene = currentRoomData.sceneToLoad;     // 当前房间的场景
        }

        await UnloadSceneTask();    // 卸载已激活的场景
        await LoadSceneTask();      // 加载场景

        afterRoomLoadedEvent.RaiseEvent(currentRoom, this);   // 触发房间加载完成事件
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

        if (currentRoomVector != -Vector2Int.one)
        {
            updateRoomStateEvent.RaiseEvent(currentRoomVector, this);   // 触发更新房间状态事件
        }

        currentScene = mapScene;
        await LoadSceneTask();      // 加载地图场景
    }

    /// <summary>
    /// 加载 Menu 场景
    /// </summary>
    public async void LoadMenu()
    {
        if (currentScene != null)
        {
            await UnloadSceneTask();    // 卸载已激活的场景
        }
        currentScene = menuScecne;
        await LoadSceneTask();          // 加载 Menu 场景
    }
}
