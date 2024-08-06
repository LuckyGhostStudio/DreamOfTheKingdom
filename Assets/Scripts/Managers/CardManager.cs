using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class CardManager : MonoBehaviour
{
    public PoolTool poolTool;
    public List<CardDataSO> cardDataList;   // ���������б�

    [Header("���ƿ�")]
    public CardLibrarySO newGameCardLibraryData;    // ����Ϸ��ʼ���ƿ�
    public CardLibrarySO currentCardLibraryData;    // ��ǰ���еĿ��ƿ�

    int preCardIndex = 0;   // ��һ�ſ�Ƭ��ţ��ڳ鿨����У�

    private void Awake()
    {
        InitaliezCardDataList();

        // ��ʼ����ǰ���еĿ��ƿ�
        foreach (var card in newGameCardLibraryData.cardLibraryList)
        {
            currentCardLibraryData.cardLibraryList.Add(card);
        }
    }

    private void OnDisable()
    {
        currentCardLibraryData.cardLibraryList.Clear();     // ��յ�ǰ���еĿ��ƿ�
    }

    #region ���ؿ�������
    /// <summary>
    /// ��ʼ�����������б�
    /// </summary>
    private void InitaliezCardDataList()
    {
        // �첽���� Addressable ��Դ���� �������� Addressables Group �� Label Ϊ CardData
        Addressables.LoadAssetsAsync<CardDataSO>("CardData", null).Completed += OnCardDataLoaded;
    }

    /// <summary>
    /// ���Ƽ������ʱ����
    /// </summary>
    /// <param name="handle">�������</param>
    private void OnCardDataLoaded(AsyncOperationHandle<IList<CardDataSO>> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)    // ���سɹ�
        {
            cardDataList = new List<CardDataSO>(handle.Result); // ��ӵ����������б�
        }
        else
        {
            Debug.LogError("No Card Data Found.");
        }
    }
    #endregion

    /// <summary>
    /// ��ȡ����
    /// </summary>
    /// <returns></returns>
    public GameObject GetCardObject()
    {
        GameObject cardObj = poolTool.GetObjectFromPool();
        cardObj.transform.localScale = Vector3.zero;    // ��ʼ����СΪ 0
        return cardObj;
    }

    /// <summary>
    /// �ͷſ���
    /// </summary>
    /// <param name="card"></param>
    public void DiscardCardObject(GameObject card)
    {
        poolTool.ReleaseObjectToPool(card);
    }

    /// <summary>
    /// ��ȡһ�ſ���
    /// </summary>
    /// <returns>��������</returns>
    public CardDataSO GetNewCardData()
    {
        int randomIndex;
        do
        {
            randomIndex = UnityEngine.Random.Range(0, cardDataList.Count);  // �����ȡ����
        } while (preCardIndex == randomIndex);  // ��ǰһ�Ų�ͬ

        preCardIndex = randomIndex;
        return cardDataList[randomIndex];
    }

    /// <summary>
    /// ����һ���¿��� ��ӵ���ǰ���ƿ�
    /// </summary>
    /// <param name="cardData">��������</param>
    public void UnlockCard(CardDataSO cardData)
    {
        // ���ҵ�ǰ����
        CardLibraryEntry target = currentCardLibraryData.cardLibraryList.Find(t => cardData == t.cardData);
        // �Ѵ���
        if (target != null)
        {
            // ���� +1
            target.amount++;
        }
        else
        {
            CardLibraryEntry newCardEntry = new CardLibraryEntry();     // �¿���������
            newCardEntry.cardData = cardData;
            newCardEntry.amount = 1;

            currentCardLibraryData.cardLibraryList.Add(newCardEntry);   // ����¿�����
        }
    }
}
