using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    private AssetReference currentScene;    // ��ǰҪ���صĳ���
    public AssetReference mapScene;         // Map ����

    /// <summary>
    /// ���ط��䳡������������¼�����
    /// </summary>
    /// <param name="data">����</param>
    public async void OnLoadRoomEvent(object data)
    {
        if (data is RoomDataSO)
        {
            RoomDataSO currentData = (RoomDataSO)data;
            Debug.Log(currentData.roomType);

            currentScene = currentData.sceneToLoad; // ��ǰ����ĳ���
        }

        await UnloadSceneTask();    // ж���Ѽ���ĳ���
        await LoadSceneTask();      // ���س���
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
