using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    private AssetReference currentScene;    // ��ǰҪ���صĳ���
    public AssetReference mapScene;         // Map ����
    public AssetReference menuScecne;       // Menu ����

    private Vector2Int currentRoomVector;   // ��ǰ���������
    private Room currentRoom;

    [Header("�����������¼��㲥")]
    public ObjectEventSO afterRoomLoadedEvent;  // �����������¼�
    [Header("���·���״̬�¼��㲥")]
    public ObjectEventSO updateRoomStateEvent;  // ���·���״̬�¼�

    private void Start()
    {
        currentRoomVector = -Vector2Int.one;
        LoadMenu();     // �������˵�����
    }

    /// <summary>
    /// ���ط��䳡������������¼�����
    /// </summary>
    /// <param name="data">����</param>
    public async void OnLoadRoomEvent(object data)
    {
        if (data is Room)
        {
            currentRoom = data as Room;
            RoomDataSO currentRoomData = currentRoom.roomData;
            currentRoomVector = new Vector2Int(currentRoom.column, currentRoom.line);

            currentScene = currentRoomData.sceneToLoad;     // ��ǰ����ĳ���
        }

        await UnloadSceneTask();    // ж���Ѽ���ĳ���
        await LoadSceneTask();      // ���س���

        afterRoomLoadedEvent.RaiseEvent(currentRoom, this);   // ���������������¼�
    }

    /// <summary>
    /// �첽���س�������
    /// </summary>
    /// <returns></returns>
    private async Awaitable LoadSceneTask()
    {
        var s = currentScene.LoadSceneAsync(LoadSceneMode.Additive);    // �첽���س���
        await s.Task;   // �ȴ��������

        // �������سɹ�
        if (s.Status == AsyncOperationStatus.Succeeded)
        {
            SceneManager.SetActiveScene(s.Result.Scene);    // �����
        }
    }

    /// <summary>
    /// �첽ж���Ѽ���ĳ�������
    /// </summary>
    /// <returns></returns>
    private async Awaitable UnloadSceneTask()
    {
        await SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());     // �첽ж���Ѽ���ĳ���
    }

    /// <summary>
    /// ���ص�ͼ������Map �����¼�����
    /// </summary>
    public async void LoadMap()
    {
        await UnloadSceneTask();    // ж���Ѽ���ĳ���

        if (currentRoomVector != -Vector2Int.one)
        {
            updateRoomStateEvent.RaiseEvent(currentRoomVector, this);   // �������·���״̬�¼�
        }

        currentScene = mapScene;
        await LoadSceneTask();      // ���ص�ͼ����
    }

    /// <summary>
    /// ���� Menu ����
    /// </summary>
    public async void LoadMenu()
    {
        if (currentScene != null)
        {
            await UnloadSceneTask();    // ж���Ѽ���ĳ���
        }
        currentScene = menuScecne;
        await LoadSceneTask();          // ���� Menu ����
    }
}
