using UnityEngine;
using UnityEngine.UIElements;

public class GameplayPanel : MonoBehaviour
{
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

        energyAmountLabel.text = "0";
        drawDeckAmountLabel.text = "0";
        DiscardDeckAmountLabel.text = "0";
        turnLabel.text = "游戏开始";
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
}
