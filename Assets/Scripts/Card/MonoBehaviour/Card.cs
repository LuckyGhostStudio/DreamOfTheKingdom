using TMPro;
using UnityEngine;

public class Card : MonoBehaviour
{
    public SpriteRenderer cardSprite;

    public TextMeshPro costText;        // ���ĵ��� Text
    public TextMeshPro descriptionText; // �������� Text
    public TextMeshPro typeText;        // �������� Text

    public CardDataSO cardData;         // ��������

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
            CardType.Attack => "����",
            CardType.Defense => "����",
            CardType.Abilities => "����",
            _ => throw new System.NotImplementedException(),
        };
    }
}
