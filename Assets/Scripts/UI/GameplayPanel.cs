using UnityEngine;
using UnityEngine.UIElements;

public class GameplayPanel : MonoBehaviour
{
    [Header("玩家回合结束事件广播")]
    public ObjectEventSO playerTurnEndEvent;    // 玩家回合结束事件

    private VisualElement rootElement;      // 根节点元素

    private Label energyAmountLabel;        // 能量 Label
    private Label drawDeckAmountLabel;      // 抽牌数量 Label
    private Label DiscardDeckAmountLabel;   // 弃牌数量 Label
    private Label turnLabel;                // 回合 Label

    private Button endTurnButton;           // 结束回合 Button

    private void OnEnable()
    {
        rootElement = GetComponent<UIDocument>().rootVisualElement;

        energyAmountLabel = rootElement.Q<Label>("EnergyAmount");
        drawDeckAmountLabel = rootElement.Q<Label>("DrawDeckAmount");
        DiscardDeckAmountLabel = rootElement.Q<Label>("DiscardAmount");
        turnLabel = rootElement.Q<Label>("TurnLabel");
        
        endTurnButton = rootElement.Q<Button>("EndTurn");
        endTurnButton.clicked += OnEndTurnButtonClick;

        energyAmountLabel.text = "0";
        drawDeckAmountLabel.text = "0";
        DiscardDeckAmountLabel.text = "0";
        turnLabel.text = "游戏开始";
    }

    /// <summary>
    /// 结束按钮按下时调用
    /// </summary>
    private void OnEndTurnButtonClick()
    {
        playerTurnEndEvent.RaiseEvent(null, this);  // 触发玩家回合结束事件
    }

    /// <summary>
    /// 更新抽牌堆卡牌数量
    /// </summary>
    /// <param name="amount">数量</param>
    public void UpdateDrawDeckAmount(int amount)
    {
        drawDeckAmountLabel.text = amount.ToString();
    }

    /// <summary>
    /// 更新弃牌堆卡牌数量
    /// </summary>
    /// <param name="amount">数量</param>
    public void UpdateDiscardDeckAmount(int amount)
    {
        DiscardDeckAmountLabel.text = amount.ToString();
    }

    /// <summary>
    /// 更新能量值
    /// </summary>
    /// <param name="amount">能量值</param>
    public void UpdateEnergyAmount(int amount)
    {
        energyAmountLabel.text = amount.ToString();
    }

    /// <summary>
    /// 敌人回合开始
    /// </summary>
    public void OnEnemyTurnBegin()
    {
        endTurnButton.SetEnabled(false);    // 回合按钮禁用

        turnLabel.text = "敌人回合";
        turnLabel.style.color = new StyleColor(Color.red);
    }

    /// <summary>
    /// 玩家回合开始
    /// </summary>
    public void OnPlayerTurnBegin()
    {
        endTurnButton.SetEnabled(true);    // 回合按钮禁用

        turnLabel.text = "玩家回合";
        turnLabel.style.color = new StyleColor(Color.white);
    }
}
