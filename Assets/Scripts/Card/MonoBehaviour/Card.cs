using TMPro;
using UnityEngine;

public class Card : MonoBehaviour
{
    public SpriteRenderer cardSprite;

    public TextMeshPro costText;        // 消耗点数 Text
    public TextMeshPro descriptionText; // 卡牌描述 Text
    public TextMeshPro typeText;        // 卡牌类型 Text

    public CardDataSO cardData;         // 卡牌数据

    private void Start()
    {
        Init(cardData);
    }

    public void Init(CardDataSO data)
    {
        cardData = data;
        cardSprite.sprite = data.cardImage;
        costText.text = data.cost.ToString();
        descriptionText.text = data.description;
        typeText.text = data.cardType switch
        {
            CardType.Attack => "攻击",
            CardType.Defense => "技能",
            CardType.Abilities => "能力",
            _ => throw new System.NotImplementedException(),
        };
    }
}
