using System;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    private void Awake()
    {
        InitaliezCardDataList();

        // 初始化当前持有的卡牌库
        foreach (var card in newGameCardLibraryData.cardLibraryList)
        {
            currentCardLibraryData.cardLibraryList.Add(card);
        }
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
        return poolTool.GetObjectFromPool();
    }

    /// <summary>
    /// 释放卡牌
    /// </summary>
    /// <param name="card"></param>
    public void DiscardCardObject(GameObject card)
    {
        poolTool.ReleaseObjectToPool(card);
    }
}
