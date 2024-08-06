using UnityEngine;
using UnityEngine.UIElements;

public class GameWinPanel : MonoBehaviour
{
    private VisualElement rootElement;  // 根节点元素

    private Button pickCardButton;      // 选择卡牌按钮
    private Button backToMapButton;     // 返回地图按钮

    [Header("加载地图事件广播")]
    public ObjectEventSO loadMapEvent;

    [Header("选择卡牌事件广播")]
    public ObjectEventSO pickCardEvent;

    private void Awake()
    {
        rootElement = GetComponent<UIDocument>().rootVisualElement;

        pickCardButton = rootElement.Q<Button>("PickCardButton");
        backToMapButton = rootElement.Q<Button>("BackToMapButton");

        pickCardButton.clicked += OnPickCardButtonClicked;
        backToMapButton.clicked += OnBackToMapButtonClicked;
    }

    /// <summary>
    /// 选择卡牌按钮按下时调用
    /// </summary>
    private void OnPickCardButtonClicked()
    {
        pickCardEvent.RaiseEvent(null, this);   // 触发选择卡牌事件
    }

    /// <summary>
    /// 返回地图按钮按下时调用
    /// </summary>
    private void OnBackToMapButtonClicked()
    {
        loadMapEvent.RaiseEvent(null, this);    // 触发加载地图事件
    }

    /// <summary>
    /// 结束选择卡牌页面时调用
    /// </summary>
    public void OnFinishPickCardEvent()
    {
        pickCardButton.style.display = DisplayStyle.None;   // 隐藏选择卡牌按钮
    }
}
