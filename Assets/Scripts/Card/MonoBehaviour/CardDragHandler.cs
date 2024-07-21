using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject arrowPrefab;      // ��ͷԤ����
    private GameObject currentArrow;    // ��ǰ�ļ�ͷ

    private Card currentCard;

    private bool canMoved;      // �����ƶ������� ��Ѫ�ȣ�
    private bool canExecuted;   // ����ִ�ж�Ӧ���ܣ������ȣ�

    private CharacterBase targetCharacter;  // Ŀ������

    private void Awake()
    {
        currentCard = GetComponent<Card>();
    }

    private void OnDisable()
    {
        canMoved = false;
        canExecuted = false;
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
                currentArrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);   // ���ɼ�ͷ
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
    /// <param name="eventData">�¼�����</param>
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
        else
        {
            if (!eventData.pointerEnter) return;
            
            // ָ���� Enemy ��
            if (eventData.pointerEnter.CompareTag("Enemy"))
            {
                canExecuted = true;
                targetCharacter = eventData.pointerEnter.GetComponent<CharacterBase>();     // ��ȡĿ��
                return;
            }
            canExecuted = false;
            targetCharacter = null;
        }
    }

    /// <summary>
    /// ������ק
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        if (currentArrow)
        {
            Destroy(currentArrow);
        }

        if (canExecuted)    // ��ִ�ж�Ӧ����
        {
            currentCard.ExecuteCardEffects(currentCard.player, targetCharacter);    // ִ�п���Ч��
        }
        else
        {
            currentCard.ResetCardTransform();   // ���ÿ���λ��
            currentCard.isAnimatiing = false;
        }
    }
}
