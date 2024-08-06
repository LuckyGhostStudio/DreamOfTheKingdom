using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class CardManager : MonoBehaviour
{
    public PoolTool poolTool;
    public List<CardDataSO> cardDataList;   // 卡牌数据列表

    [Header("卡牌库")]
    public CardLibrarySO newGameCardLibraryData;    // 新游戏初始卡牌库
    public CardLibrarySO currentCardLibraryData;    // 当前持有的卡牌库

    int preCardIndex = 0;   // 上一张卡片序号（在抽卡面板中）

    private void Awake()
    {
        InitaliezCardDataList();

        // 初始化当前持有的卡牌库
        foreach (var card in newGameCardLibraryData.cardLibraryList)
        {
            currentCardLibraryData.cardLibraryList.Add(card);
        }
    }

    private void OnDisable()
    {
        currentCardLibraryData.cardLibraryList.Clear();     // 清空当前持有的卡牌库
    }

    #region 加载卡牌数据
    /// <summary>
    /// 初始化卡牌数据列表
    /// </summary>
    private void InitaliezCardDataList()
    {
        // 异步加载 Addressable 资源化的 卡牌数据 Addressables Group 的 Label 为 CardData
        Addressables.LoadAssetsAsync<CardDataSO>("CardData", null).Completed += OnCardDataLoaded;
    }

    /// <summary>
    /// 卡牌加载完成时调用
    /// </summary>
    /// <param name="handle">操作句柄</param>
    private void OnCardDataLoaded(AsyncOperationHandle<IList<CardDataSO>> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)    // 加载成功
        {
            cardDataList = new List<CardDataSO>(handle.Result); // 添加到卡牌数据列表
        }
        else
        {
            Debug.LogError("No Card Data Found.");
        }
    }
    #endregion

    /// <summary>
    /// 获取卡牌
    /// </summary>
    /// <returns></returns>
    public GameObject GetCardObject()
    {
        GameObject cardObj = poolTool.GetObjectFromPool();
        cardObj.transform.localScale = Vector3.zero;    // 初始化大小为 0
        return cardObj;
    }

    /// <summary>
    /// 释放卡牌
    /// </summary>
    /// <param name="card"></param>
    public void DiscardCardObject(GameObject card)
    {
        poolTool.ReleaseObjectToPool(card);
    }

    /// <summary>
    /// 抽取一张卡牌
    /// </summary>
    /// <returns>卡牌数据</returns>
    public CardDataSO GetNewCardData()
    {
        int randomIndex;
        do
        {
            randomIndex = UnityEngine.Random.Range(0, cardDataList.Count);  // 随机抽取卡牌
        } while (preCardIndex == randomIndex);  // 与前一张不同

        preCardIndex = randomIndex;
        return cardDataList[randomIndex];
    }

    /// <summary>
    /// 解锁一张新卡牌 添加到当前卡牌库
    /// </summary>
    /// <param name="cardData">卡牌数据</param>
    public void UnlockCard(CardDataSO cardData)
    {
        // 查找当前卡牌
        CardLibraryEntry target = currentCardLibraryData.cardLibraryList.Find(t => cardData == t.cardData);
        // 已存在
        if (target != null)
        {
            // 数量 +1
            target.amount++;
        }
        else
        {
            CardLibraryEntry newCardEntry = new CardLibraryEntry();     // 新卡牌数据项
            newCardEntry.cardData = cardData;
            newCardEntry.amount = 1;

            currentCardLibraryData.cardLibraryList.Add(newCardEntry);   // 添加新卡牌项
        }
    }
}
