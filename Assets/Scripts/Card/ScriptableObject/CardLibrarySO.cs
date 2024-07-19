using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardLibrarySO", menuName = "Card/CardLibrarySO")]
public class CardLibrarySO : ScriptableObject
{
    public List<CardLibraryEntry> cardLibraryList; // 卡牌库列表
}

/// <summary>
/// 卡牌库的每个项
/// </summary>
[System.Serializable]
public class CardLibraryEntry
{
    public CardDataSO cardData; // 卡牌数据
    public int amount;          // 卡牌数量
}