using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardLibrarySO", menuName = "Card/CardLibrarySO")]
public class CardLibrarySO : ScriptableObject
{
    public List<CardLibraryEntry> cardLibraryList; // ���ƿ��б�
}

/// <summary>
/// ���ƿ��ÿ����
/// </summary>
[System.Serializable]
public class CardLibraryEntry
{
    public CardDataSO cardData; // ��������
    public int amount;          // ��������
}