using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject arrowPrefab;      // 箭头预制体
    private GameObject currentArrow;    // 当前的箭头

    private Card currentCard;

    private bool canMoved;      // 可以移动（防御 回血等）
    private bool canExecuted;   // 可以执行对应技能（攻击等）

    private CharacterBase targetCharacter;  // 目标人物

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
    /// 开始拖拽
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        switch (currentCard.cardData.cardType)
        {
            case CardType.Attack:
                currentArrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);   // 生成箭头
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
    /// 拖拽中
    /// </summary>
    /// <param name="eventData">事件数据</param>
    public void OnDrag(PointerEventData eventData)
    {
        if (canMoved)
        {
            currentCard.isAnimatiing = true;
            Vector3 screenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);  // 鼠标屏幕坐标
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);                       // 鼠标世界坐标
            currentCard.transform.position = worldPos;                                          // 设置卡牌位置
            
            canExecuted = worldPos.y > 1.0f;     // 卡牌到指定区域时可被执行
        }
        else
        {
            if (!eventData.pointerEnter) return;
            
            // 指针在 Enemy 上
            if (eventData.pointerEnter.CompareTag("Enemy"))
            {
                canExecuted = true;
                targetCharacter = eventData.pointerEnter.GetComponent<CharacterBase>();     // 获取目标
                return;
            }
            canExecuted = false;
            targetCharacter = null;
        }
    }

    /// <summary>
    /// 结束拖拽
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        if (currentArrow)
        {
            Destroy(currentArrow);
        }

        if (canExecuted)    // 可执行对应技能
        {
            currentCard.ExecuteCardEffects(currentCard.player, targetCharacter);    // 执行卡牌效果
        }
        else
        {
            currentCard.ResetCardTransform();   // 重置卡牌位置
            currentCard.isAnimatiing = false;
        }
    }
}
