using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public SpriteRenderer cardSprite;

    public TextMeshPro costText;        // ���ĵ��� Text
    public TextMeshPro descriptionText; // �������� Text
    public TextMeshPro typeText;        // �������� Text

    public CardDataSO cardData;         // ��������

    [Header("ԭʼ����")]
    public Vector3 originalPosition;    // ԭλ��
    public Quaternion originalRotation; // ԭ��ת
    public int originalSortingLayer;    // ԭ Sorting Layer

    public bool isAnimatiing;   // ���ڽ��ж���

    public Player player;

    [Header("���տ����¼��㲥")]
    public ObjectEventSO discardCardEvent;  // ���տ����¼�

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

        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    /// <summary>
    /// ����ԭλ�ú���ת
    /// </summary>
    /// <param name="position">ԭλ��</param>
    /// <param name="rotation">ԭ��ת</param>
    public void UpdatePositionAndRotation(Vector3 position, Quaternion rotation)
    {
        originalPosition = position;
        originalRotation = rotation;
        originalSortingLayer = GetComponent<SortingGroup>().sortingOrder;
    }

    /// <summary>
    /// ���ָ�뻬��ʱ����
    /// </summary>
    /// <param name="eventData">ָ���¼�����</param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isAnimatiing) return;
        transform.position = new Vector3(originalPosition.x, -3.5f, originalPosition.z);
        transform.rotation = Quaternion.identity;               // ��ת��ΪĬ��
        GetComponent<SortingGroup>().sortingOrder = 20;         // Sorting Layer����Ϊ���
    }

    /// <summary>
    /// ���ָ�뻬��ʱ����
    /// </summary>
    /// <param name="eventData">ָ���¼�����</param>
    public void OnPointerExit(PointerEventData eventData)
    {
        if (isAnimatiing) return;
        ResetCardTransform();   // ���ÿ��� Transform
    }

    /// <summary>
    /// ���ÿ��� Transform
    /// </summary>
    public void ResetCardTransform()
    {
        if (isAnimatiing) return;
        transform.position = originalPosition;
        transform.rotation = originalRotation;
        GetComponent<SortingGroup>().sortingOrder = originalSortingLayer;
    }

    /// <summary>
    /// ִ�п���Ч��
    /// </summary>
    /// <param name="from">������</param>
    /// <param name="target">Ŀ��</param>
    public void ExecuteCardEffects(CharacterBase from, CharacterBase target)
    {
        // TODO ���ٶ�Ӧ����
        discardCardEvent.RaiseEvent(this, this);    // �������տ����¼�

        // ִ�п���Ч��
        foreach (Effect effect in cardData.effexts)
        {
            effect.Execute(from, target);
        }
    }
}
