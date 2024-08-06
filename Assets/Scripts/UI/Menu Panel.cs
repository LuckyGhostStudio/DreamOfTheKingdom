using UnityEngine;
using UnityEngine.UIElements;

public class MenuPanel : MonoBehaviour
{
    private VisualElement rootElement;

    private Button newGameButton;   // 新游戏按钮
    private Button quitGameButton;  // 退出游戏按钮

    [Header("新游戏开始事件广播")]
    public ObjectEventSO newGameEvent;

    private void OnEnable()
    {
        rootElement = GetComponent<UIDocument>().rootVisualElement;

        newGameButton = rootElement.Q<Button>("NewGameButton");
        quitGameButton = rootElement.Q<Button>("QuitGameButton");

        newGameButton.clicked += OnNewGameButtonClicked;
        quitGameButton.clicked += OnQuitGameButtonClicked;
    }

    /// <summary>
    /// 退出游戏按钮按下时调用
    /// </summary>
    private void OnQuitGameButtonClicked()
    {
        Application.Quit();
    }

    /// <summary>
    /// 开始新游戏按钮按下时调用
    /// </summary>
    private void OnNewGameButtonClicked()
    {
        newGameEvent.RaiseEvent(null, this);    // 触发新游戏开始事件
    }
}
