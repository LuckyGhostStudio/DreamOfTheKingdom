using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    private AssetReference currentScene;    // ��ǰҪ���صĳ���
    public AssetReference mapScene;         // Map ����

    private Vector2Int currentRoomVector;    // ��ǰ���������

    [Header("�㲥")]
    public ObjectEventSO afterRoomLoadedEvent;  // �����������¼�

    /// <summary>
    /// ���ط��䳡������������¼�����
    /// </summary>
    /// <param name="data">����</param>
    public async void OnLoadRoomEvent(object data)
    {
        if (data is Room)
        {
            Room currentRoom = data as Room;
            RoomDataSO currentRoomData = currentRoom.roomData;
            currentRoomVector = new Vector2Int(currentRoom.column, currentRoom.line);

            currentScene = currentRoomData.sceneToLoad;     // ��ǰ����ĳ���
        }

        await UnloadSceneTask();    // ж���Ѽ���ĳ���
        await LoadSceneTask();      // ���س���

        afterRoomLoadedEvent.RaiseEvent(currentRoomVector, this);   // ���������������¼�
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
        currentScene = mapScene;
        await LoadSceneTask();      // ���ص�ͼ����
    }
}
