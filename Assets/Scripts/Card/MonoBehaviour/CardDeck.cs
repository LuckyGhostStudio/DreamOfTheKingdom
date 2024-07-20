using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 牌堆
/// </summary>
public class CardDeck : MonoBehaviour
{
    public CardManager cardManager;             // 卡牌管理器
    public CardLayoutManager cardLayoutManager; // 卡牌布局管理器

    private List<CardDataSO> drawDeck = new List<CardDataSO>();      // 待抽牌堆
    private List<CardDataSO> discardDeck = new List<CardDataSO>();   // 弃牌堆
    private List<Card> handCardObjectList = new List<Card>();        // 当前手牌（每回合）

    // TODO 测试
    private void Start()
    {
        InitalizeDeck();
        DrawCard(3);
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

        // TODO 洗牌 更新抽牌或弃牌列表
    }

    [ContextMenu("抽牌测试")]
    public void DrawCardTest()
    {
        DrawCard(1);
    }

    /// <summary>
    /// 抽牌
    /// </summary>
    /// <param name="amount">数量</param>
    private void DrawCard(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            if (drawDeck.Count <= 0)
            {
                // TODO 洗牌 更新抽牌或弃牌列表
            }
            CardDataSO currentCardData = drawDeck[0];   // 抽出第 0 张卡牌
            drawDeck.RemoveAt(0);                       // 移除该卡牌

            Card card = cardManager.GetCardObject().GetComponent<Card>();   // 从对象池获取一个 Card 对象
            card.Init(currentCardData);     // 使用抽出的卡牌数据初始化卡牌
            handCardObjectList.Add(card);   // 添加到手牌列表
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
            currentCard.transform.SetPositionAndRotation(cardTransform.position, cardTransform.rotation);   // 设置卡牌位置和旋转
        }
    }
}
