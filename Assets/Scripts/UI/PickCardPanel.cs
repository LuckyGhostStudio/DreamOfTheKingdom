using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PickCardPanel : MonoBehaviour
{
    public CardManager cardManager;

    private VisualElement rootElement;      // 根节点元素

    public VisualTreeAsset cardTemplate;    // 卡牌 UI Document
    private VisualElement cardContainer;    // 放置卡牌 UI 的容器

    private CardDataSO currentCardData;     // 当前卡牌数据

    private Button confirmButton;           // 确认按钮

    private List<Button> cardButtons = new List<Button>();  // 卡牌 Button 列表

    [Header("结束选择卡牌页面事件广播")]
    public ObjectEventSO finishPickCardEvent;

    private void OnEnable()
    {
        rootElement = GetComponent<UIDocument>().rootVisualElement;
        cardContainer = rootElement.Q<VisualElement>("Container");
        confirmButton = rootElement.Q<Button>("ConfirmButton");

        confirmButton.clicked += OnConfirmButtonClicked;

        for (int i = 0; i < 3; i++)
        {
            var card = cardTemplate.Instantiate();          // 生成卡片 UI 实例
            CardDataSO data = cardManager.GetNewCardData(); // 抽取卡牌数据

            InitCard(card, data);       // 初始化卡牌 UI

            Button cardButton = card.Q<Button>("Card");     // 当前卡牌 UI 按钮 

            cardContainer.Add(card);    // 添加为容器的子物体
            cardButtons.Add(cardButton);

            cardButton.clicked += () => OnCardButtonClicked(cardButton, data);  // 添加点击方法
        }
    }

    /// <summary>
    /// 确认按钮按下时调用
    /// </summary>
    private void OnConfirmButtonClicked()
    {
        cardManager.UnlockCard(currentCardData);    // 添加当前卡牌到卡牌库
        finishPickCardEvent.RaiseEvent(null, this); // 触发卡牌选择页面结束事件
    }

    /// <summary>
    /// 卡牌被点击时调用
    /// </summary>
    /// <param name="cardButton">卡牌按钮</param>
    private void OnCardButtonClicked(Button cardButton, CardDataSO data)
    {
        currentCardData = data;  // 当前点击的卡牌数据

        Debug.Log("Current Card " + currentCardData.cardName);

        // 设置当前已点击的卡牌按钮为不启用状态
        for (int i = 0; i < cardButtons.Count; i++)
        {
            cardButtons[i].SetEnabled(cardButtons[i] != cardButton);
        }
    }

    /// <summary>
    /// 初始化卡牌 UI 数据
    /// </summary>
    /// <param name="cardElement">卡牌 UI 元素根节点</param>
    /// <param name="cardData">卡牌数据</param>
    public void InitCard(VisualElement cardElement, CardDataSO cardData)
    {
        VisualElement cardSpriteElement = cardElement.Q<VisualElement>("CardSprite");   // 获取卡牌图片 UI
        Label cardCost = cardElement.Q<Label>("EnergyCost");                            // 获取卡牌消耗 UI
        Label cardDescription = cardElement.Q<Label>("CardDescription");                // 获取卡牌描述 UI
        Label cardType = cardElement.Q<Label>("CardType");                              // 获取卡牌类型 UI
        Label cardName = cardElement.Q<Label>("CardName");                              // 获取卡牌名字 UI

        cardSpriteElement.style.backgroundImage = new StyleBackground(cardData.cardImage);
        cardCost.text = cardData.cost.ToString();
        cardDescription.text = cardData.description;
        cardName.text = cardData.cardName;

        cardType.text = cardData.cardType switch
        {
            CardType.Attack => "攻击",
            CardType.Defense => "防御",
            CardType.Abilities => "能力",
            _ => throw new System.NotImplementedException(),
        };
    }
}
