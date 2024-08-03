using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI ���")]
    public GameObject gameplayPanel;    // Gameplay UI ���
    public GameObject gameWinPanel;     // ��Ϸ��ʤ UI ���
    public GameObject gameOverPanel;    // ��Ϸ���� UI ���
    public GameObject pickCardPanel;    // ѡ���� UI ���

    /// <summary>
    /// �������ʱ����
    /// </summary>
    /// <param name="data"></param>
    public void OnLoadRoomEvent(object data)
    {
        Room currentRoom = data as Room;

        switch (currentRoom.roomData.roomType)
        {
            case RoomType.MinorEnemy:
            case RoomType.EliteEnemy:
            case RoomType.Boss:
                gameplayPanel.SetActive(true);
                break;
            case RoomType.Treasure:
                break;
            case RoomType.Shop:
                break;
            case RoomType.Rest:
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// �������� Panel
    /// </summary>
    public void HideAllPanels()
    {
        gameplayPanel.SetActive(false);
        gameWinPanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    public void OnGameWinEvent()
    {
        gameplayPanel.SetActive(false);
        gameWinPanel.SetActive(true);
    }
    
    public void OnGameOverEvent()
    {
        gameplayPanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    public void OnPickCardEvent()
    {
        pickCardPanel.SetActive(true);
    }
}