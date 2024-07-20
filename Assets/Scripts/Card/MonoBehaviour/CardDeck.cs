using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ƶ�
/// </summary>
public class CardDeck : MonoBehaviour
{
    public CardManager cardManager;             // ���ƹ�����
    public CardLayoutManager cardLayoutManager; // ���Ʋ��ֹ�����

    private List<CardDataSO> drawDeck = new List<CardDataSO>();      // �����ƶ�
    private List<CardDataSO> discardDeck = new List<CardDataSO>();   // ���ƶ�
    private List<Card> handCardObjectList = new List<Card>();        // ��ǰ���ƣ�ÿ�غϣ�

    // TODO ����
    private void Start()
    {
        InitalizeDeck();
        DrawCard(3);
    }

    /// <summary>
    /// ��ʼ���ƶ�
    /// </summary>
    public void InitalizeDeck()
    {
        drawDeck.Clear();
        // ����ǰ���еĿ��ƿ�Ŀ�����ӵ��������б�
        foreach (var cardEntry in cardManager.currentCardLibraryData.cardLibraryList)
        {
            for (int i = 0; i < cardEntry.amount; i++)
            {
                drawDeck.Add(cardEntry.cardData);
            }
        }

        // TODO ϴ�� ���³��ƻ������б�
    }

    [ContextMenu("���Ʋ���")]
    public void DrawCardTest()
    {
        DrawCard(1);
    }

    /// <summary>
    /// ����
    /// </summary>
    /// <param name="amount">����</param>
    private void DrawCard(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            if (drawDeck.Count <= 0)
            {
                // TODO ϴ�� ���³��ƻ������б�
            }
            CardDataSO currentCardData = drawDeck[0];   // ����� 0 �ſ���
            drawDeck.RemoveAt(0);                       // �Ƴ��ÿ���

            Card card = cardManager.GetCardObject().GetComponent<Card>();   // �Ӷ���ػ�ȡһ�� Card ����
            card.Init(currentCardData);     // ʹ�ó���Ŀ������ݳ�ʼ������
            handCardObjectList.Add(card);   // ��ӵ������б�
        }

        SetCardLayout();    // �������Ʋ���
    }

    /// <summary>
    /// ���ÿ��Ʋ���
    /// </summary>
    private void SetCardLayout()
    {
        for (int i = 0; i < handCardObjectList.Count; i++)
        {
            Card currentCard = handCardObjectList[i];
            CardTransform cardTransform = cardLayoutManager.GetCardTransform(i, handCardObjectList.Count);  // ���㿨��λ�ú���ת
            currentCard.transform.SetPositionAndRotation(cardTransform.position, cardTransform.rotation);   // ���ÿ���λ�ú���ת
        }
    }
}
