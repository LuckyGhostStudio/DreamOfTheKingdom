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
}
