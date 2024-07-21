using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardDataSO", menuName = "Card/CardDataSO")]
public class CardDataSO : ScriptableObject
{
    public string cardName;         // ����
    public Sprite cardImage;        // ͼƬ
    public int cost;                // ���ĵ���
    public CardType cardType;       // ����

    [TextArea]
    public string description;      // ����

    public List<Effect> effexts;    // Ч���б�
}
