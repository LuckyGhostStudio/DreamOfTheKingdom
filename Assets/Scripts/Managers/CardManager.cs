using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class CardManager : MonoBehaviour
{
    public PoolTool poolTool;
    public List<CardDataSO> cardDataList;   // ���������б�

    private void Awake()
    {
        InitaliezCardDataList();
    }

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
}
