using UnityEngine;
using UnityEngine.UIElements;

public class RestRoomPanel : MonoBehaviour
{
    private VisualElement rootElement;

    private Button restButton;      // 休息按钮
    private Button backToMapButton; // 返回地图按钮

    public Effect restHealEffect;   // 休息回血效果

    private CharacterBase player;

    [Header("加载地图事件广播")]
    public ObjectEventSO loadMapEvent;

    private void OnEnable()
    {
        rootElement = GetComponent<UIDocument>().rootVisualElement;

        restButton = rootElement.Q<Button>("RestButton");
        backToMapButton = rootElement.Q<Button>("BackToMapButton");

        player = FindAnyObjectByType<Player>(FindObjectsInactive.Include);  // 查找 Player 即使未激活

        restButton.clicked += OnRestButtonClicked;
        backToMapButton.clicked += OnBackToMapButtonClicked;
    }

    /// <summary>
    /// 休息按钮按下时调用
    /// </summary>
    private void OnRestButtonClicked()
    {
        restHealEffect.Execute(player, null);   // 执行回血效果
        restButton.SetEnabled(false);
    }

    /// <summary>
    /// 返回地图按钮按下时调用
    /// </summary>
    private void OnBackToMapButtonClicked()
    {
        loadMapEvent.RaiseEvent(null, this);    // 触发加载地图事件
    }
}
