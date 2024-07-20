using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// �ƶ�
/// </summary>
public class CardDeck : MonoBehaviour
{
    public CardManager cardManager;             // ���ƹ�����
    public CardLayoutManager cardLayoutManager; // ���Ʋ��ֹ�����

    public Vector3 deckPosition;    // �ƶѷ���λ��

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
            card.Init(currentCardData);             // ʹ�ó���Ŀ������ݳ�ʼ������
            card.transform.position = deckPosition; // ��ʼλ��
            handCardObjectList.Add(card);           // ��ӵ������б�
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

            currentCard.isAnimatiing = true;    // ���ڶ���

            float delay = Mathf.Log10(1.2f + i);     // ÿ�����ƶ��ӳ�
            // ���ƴ� 0 ���ŵ� 1 ����
            currentCard.transform.DOScale(Vector3.one, 0.2f).SetDelay(delay).onComplete = () => // ���Ž���ʱִ��
            {
                // ���ƴӳ�ʼλ���ƶ���Ŀ��λ��
                currentCard.transform.DOMove(cardTransform.position, 0.3f).onComplete = () =>   // �ƶ�����ʱִ��
                {
                    currentCard.isAnimatiing = false;   // ��������
                };
                currentCard.transform.DORotateQuaternion(cardTransform.rotation, 0.5f); // ��ת��Ŀ��ֵ
            };

            currentCard.GetComponent<SortingGroup>().sortingOrder = i;  // ���ÿ��� sorting layer
            currentCard.UpdatePositionAndRotation(cardTransform.position, cardTransform.rotation);  // ����ԭ�ǶȺ���ת
        }
    }
}
