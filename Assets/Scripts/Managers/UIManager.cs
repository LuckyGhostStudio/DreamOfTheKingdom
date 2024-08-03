using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI 面板")]
    public GameObject gameplayPanel;    // Gameplay UI 面板
    public GameObject gameWinPanel;     // 游戏获胜 UI 面板
    public GameObject gameOverPanel;    // 游戏结束 UI 面板
    public GameObject pickCardPanel;    // 选择卡牌 UI 面板

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