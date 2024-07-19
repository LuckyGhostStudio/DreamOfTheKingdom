using System;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    private void Awake()
    {
        InitaliezCardDataList();

        // ��ʼ����ǰ���еĿ��ƿ�
        foreach (var card in newGameCardLibraryData.cardLibraryList)
        {
            currentCardLibraryData.cardLibraryList.Add(card);
        }
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
        return poolTool.GetObjectFromPool();
    }

    /// <summary>
    /// �ͷſ���
    /// </summary>
    /// <param name="card"></param>
    public void DiscardCardObject(GameObject card)
    {
        poolTool.ReleaseObjectToPool(card);
    }
}
