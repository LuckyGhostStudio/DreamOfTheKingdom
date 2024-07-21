using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardDataSO", menuName = "Card/CardDataSO")]
public class CardDataSO : ScriptableObject
{
    public string cardName;         // 名称
    public Sprite cardImage;        // 图片
    public int cost;                // 消耗点数
    public CardType cardType;       // 类型

    [TextArea]
    public string description;      // 描述

    public List<Effect> effexts;    // 效果列表
}
