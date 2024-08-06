using UnityEngine;
using UnityEngine.UIElements;

public class GameOverPanel : MonoBehaviour
{
    private Button backToStartButton;   // 返回开始界面按钮

    [Header("加载 Menu 事件广播")]
    public ObjectEventSO loadMenuEvent;

    private void OnEnable()
    {
        GetComponent<UIDocument>().rootVisualElement.Q<Button>("BackToStartButton").clicked += OnGameOverPanelClicked;
    }

    private void OnGameOverPanelClicked()
    {
        loadMenuEvent.RaiseEvent(null, this);
    }
}
