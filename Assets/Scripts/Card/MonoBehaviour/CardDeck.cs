using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// 牌堆
/// </summary>
public class CardDeck : MonoBehaviour
{
    public CardManager cardManager;             // 卡牌管理器
    public CardLayoutManager cardLayoutManager; // 卡牌布局管理器

    public Vector3 deckPosition;    // 牌堆发牌位置

    private List<CardDataSO> drawDeck = new List<CardDataSO>();      // 待抽牌堆
    private List<CardDataSO> discardDeck = new List<CardDataSO>();   // 弃牌堆
    private List<Card> handCardObjectList = new List<Card>();        // 当前手牌（每回合）

    [Header("抽牌堆卡牌数量改变事件广播")]
    public IntEventSO drawDeckAmountChangedEvent;

    [Header("抽牌堆卡牌数量改变事件广播")]
    public IntEventSO discardDeckAmountChangedEvent;

    // TODO 测试
    private void Start()
    {
        InitalizeDeck();
    }

    /// <summary>
    /// 初始化牌堆
    /// </summary>
    public void InitalizeDeck()
    {
        drawDeck.Clear();
        // 将当前持有的卡牌库的卡牌添加到待抽牌列表
        foreach (var cardEntry in cardManager.currentCardLibraryData.cardLibraryList)
        {
            for (int i = 0; i < cardEntry.amount; i++)
            {
                drawDeck.Add(cardEntry.cardData);
            }
        }

        ShuffleDeck();  // 洗牌
    }

    [ContextMenu("抽牌测试")]
    public void DrawCardTest()
    {
        DrawCard(1);
    }

    /// <summary>
    /// 新回合抽卡
    /// </summary>
    public void NewTurnDrawCards()
    {
        DrawCard(4);
    }

    /// <summary>
    /// 抽牌
    /// </summary>
    /// <param name="amount">数量</param>
    public void DrawCard(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            if (drawDeck.Count <= 0)
            {
                // 将弃牌堆的卡牌添加到待抽列表
                foreach (var cardDara in discardDeck)
                {
                    drawDeck.Add(cardDara);
                }
                ShuffleDeck();  // 洗牌
            }

            CardDataSO currentCardData = drawDeck[0];   // 抽出第 0 张卡牌
            drawDeck.RemoveAt(0);                       // 移除该卡牌

            drawDeckAmountChangedEvent.RaiseEvent(drawDeck.Count, this);        // 触发抽牌堆数量改变事件

            Card card = cardManager.GetCardObject().GetComponent<Card>();   // 从对象池获取一个 Card 对象
            card.Init(currentCardData);             // 使用抽出的卡牌数据初始化卡牌
            card.transform.position = deckPosition; // 初始位置
            handCardObjectList.Add(card);           // 添加到手牌列表
        }

        SetCardLayout();    // 设置手牌布局
    }

    /// <summary>
    /// 设置卡牌布局
    /// </summary>
    private void SetCardLayout()
    {
        for (int i = 0; i < handCardObjectList.Count; i++)
        {
            Card currentCard = handCardObjectList[i];
            CardTransform cardTransform = cardLayoutManager.GetCardTransform(i, handCardObjectList.Count);  // 计算卡牌位置和旋转

            currentCard.UpdateStatus();         // 更新卡牌状态

            currentCard.isAnimatiing = true;    // 正在动画

            float delay = Mathf.Log10(1.2f + i);     // 每张牌移动延迟
            // 卡牌从 0 缩放到 1 动画
            currentCard.transform.DOScale(Vector3.one, 0.2f).SetDelay(delay).onComplete = () => // 缩放结束时执行
            {
                // 卡牌从初始位置移动到目标位置
                currentCard.transform.DOMove(cardTransform.position, 0.3f).onComplete = () =>   // 移动结束时执行
                {
                    currentCard.isAnimatiing = false;   // 动画结束
                };
                currentCard.transform.DORotateQuaternion(cardTransform.rotation, 0.5f); // 旋转到目标值
            };

            currentCard.GetComponent<SortingGroup>().sortingOrder = i;  // 设置卡牌 sorting layer
            currentCard.UpdatePositionAndRotation(cardTransform.position, cardTransform.rotation);  // 更新原角度和旋转
        }
    }

    /// <summary>
    /// 洗牌
    /// </summary>
    private void ShuffleDeck()
    {
        discardDeck.Clear();    // 清空弃牌堆

        drawDeckAmountChangedEvent.RaiseEvent(drawDeck.Count, this);        // 触发抽牌堆数量改变事件
        discardDeckAmountChangedEvent.RaiseEvent(discardDeck.Count, this);  // 触发弃牌堆数量改变事件

        for (int i = 0; i < drawDeck.Count; i++)
        {
            int randomIndex = Random.Range(i, drawDeck.Count);  // 获取随机下标
            // 交换
            CardDataSO temp = drawDeck[i];
            drawDeck[i] = drawDeck[randomIndex];
            drawDeck[randomIndex] = temp;
        }
    }

    /// <summary>
    /// 弃牌事件监听
    /// </summary>
    /// <param name="cardObj">卡牌对象</param>
    public void DiscardCard(object cardObj)
    {
        Card card = cardObj as Card;

        discardDeck.Add(card.cardData);     // 添加到弃牌堆
        handCardObjectList.Remove(card);    // 从手牌中移除

        cardManager.DiscardCardObject(card.gameObject);                     // 将卡牌释放回对象池
        discardDeckAmountChangedEvent.RaiseEvent(discardDeck.Count, this);  // 触发弃牌堆数量改变事件

        SetCardLayout();    // 重新设置手牌布局
    }

    /// <summary>
    /// 弃掉所有手牌
    /// </summary>
    public void DisacardHandCards()
    {
        foreach (Card card in handCardObjectList)
        {
            discardDeck.Add(card.cardData);                     // 添加到弃牌堆
            cardManager.DiscardCardObject(card.gameObject);     // 将卡牌释放回对象池
        }
        handCardObjectList.Clear();                                         // 清空手牌
        discardDeckAmountChangedEvent.RaiseEvent(discardDeck.Count, this);  // 触发弃牌堆数量改变事件
    }
}
