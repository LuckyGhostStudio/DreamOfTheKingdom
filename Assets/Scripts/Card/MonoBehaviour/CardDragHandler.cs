using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Card currentCard;
    private bool canMoved;      // �����ƶ�
    private bool canExecuted;   // ����ִ�ж�Ӧ����

    private void Awake()
    {
        currentCard = GetComponent<Card>();
    }

    /// <summary>
    /// ��ʼ��ק
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        switch (currentCard.cardData.cardType)
        {
            case CardType.Attack:
                break;
            case CardType.Defense:
            case CardType.Abilities:
                canMoved = true;
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// ��ק��
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        if (canMoved)
        {
            currentCard.isAnimatiing = true;
            Vector3 screenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);  // �����Ļ����
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);                       // �����������
            currentCard.transform.position = worldPos;                                          // ���ÿ���λ��
            
            canExecuted = worldPos.y > 1.0f;     // ���Ƶ�ָ������ʱ�ɱ�ִ��
        }
    }

    /// <summary>
    /// ������ק
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        if (canExecuted)    // ��ִ�ж�Ӧ����
        {

        }
        else
        {
            currentCard.ResetCardTransform();   // ����λ��
            currentCard.isAnimatiing = false;
        }
    }
}
