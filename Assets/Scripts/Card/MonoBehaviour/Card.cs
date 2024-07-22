using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public SpriteRenderer cardSprite;

    public TextMeshPro costText;        // 消耗点数 Text
    public TextMeshPro descriptionText; // 卡牌描述 Text
    public TextMeshPro typeText;        // 卡牌类型 Text

    public CardDataSO cardData;         // 卡牌数据

    [Header("原始参数")]
    public Vector3 originalPosition;    // 原位置
    public Quaternion originalRotation; // 原旋转
    public int originalSortingLayer;    // 原 Sorting Layer

    public bool isAnimatiing;   // 正在进行动画
    public bool isAvailiable;   // 是否可用

    public Player player;

    [Header("卡牌消耗能量事件广播")]
    public IntEventSO costEvent;            // 消耗能量事件

    [Header("回收卡牌事件广播")]
    public ObjectEventSO discardCardEvent;  // 回收卡牌事件

    private void Start()
    {
        Init(cardData);
    }

    /// <summary>
    /// 初始化卡牌数据
    /// </summary>
    /// <param name="data">卡牌数据</param>
    /// <exception cref="System.NotImplementedException"></exception>
    public void Init(CardDataSO data)
    {
        cardData = data;
        cardSprite.sprite = data.cardImage;
        costText.text = data.cost.ToString();
        descriptionText.text = data.description;
        typeText.text = data.cardType switch
        {
            CardType.Attack => "攻击",
            CardType.Defense => "防御",
            CardType.Abilities => "能力",
            _ => throw new System.NotImplementedException(),
        };

        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    /// <summary>
    /// 更新原位置和旋转
    /// </summary>
    /// <param name="position">原位置</param>
    /// <param name="rotation">原旋转</param>
    public void UpdatePositionAndRotation(Vector3 position, Quaternion rotation)
    {
        originalPosition = position;
        originalRotation = rotation;
        originalSortingLayer = GetComponent<SortingGroup>().sortingOrder;
    }

    /// <summary>
    /// 鼠标指针滑入时调用
    /// </summary>
    /// <param name="eventData">指针事件数据</param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isAnimatiing) return;
        transform.position = new Vector3(originalPosition.x, -3.5f, originalPosition.z);
        transform.rotation = Quaternion.identity;               // 旋转置为默认
        GetComponent<SortingGroup>().sortingOrder = 20;         // Sorting Layer设置为最高
    }

    /// <summary>
    /// 鼠标指针滑出时调用
    /// </summary>
    /// <param name="eventData">指针事件数据</param>
    public void OnPointerExit(PointerEventData eventData)
    {
        if (isAnimatiing) return;
        ResetCardTransform();   // 重置卡牌 Transform
    }

    /// <summary>
    /// 重置卡牌 Transform
    /// </summary>
    public void ResetCardTransform()
    {
        if (isAnimatiing) return;
        transform.position = originalPosition;
        transform.rotation = originalRotation;
        GetComponent<SortingGroup>().sortingOrder = originalSortingLayer;
    }

    /// <summary>
    /// 执行卡牌效果
    /// </summary>
    /// <param name="from">发出者</param>
    /// <param name="target">目标</param>
    public void ExecuteCardEffects(CharacterBase from, CharacterBase target)
    {
        costEvent.RaiseEvent(cardData.cost, this);  // 触发卡牌消耗事件
        discardCardEvent.RaiseEvent(this, this);    // 触发回收卡牌事件

        // 执行卡牌效果
        foreach (Effect effect in cardData.effexts)
        {
            effect.Execute(from, target);
        }
    }

    /// <summary>
    /// 更新卡牌状态
    /// </summary>
    public void UpdateStatus()
    {
        isAvailiable = cardData.cost <= player.CurrentMana;         // Mana 值足够
        costText.color = isAvailiable ? Color.green : Color.red;    // 设置 cost 文本颜色
    }
}
