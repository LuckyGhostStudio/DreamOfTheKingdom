using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI 面板")]
    public GameObject gameplayPanel;
    public GameObject gameWinPanel;
    public GameObject gameOverPanel;

    /// <summary>
    /// 房间加载时调用
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
    /// 隐藏所有 Panel
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
        gameWinPanel.SetActive(false);
    }
    
    public void OnGameOverEvent()
    {
        gameplayPanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }
}