using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject arrowPrefab;      // 箭头预制体
    private GameObject currentArrow;    // 当前的箭头

    private Card currentCard;
    private bool canMoved;      // 可以移动
    private bool canExecuted;   // 可以执行对应技能

    private void Awake()
    {
        currentCard = GetComponent<Card>();
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
    /// <param name="eventData"></param>
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

        }
        else
        {
            currentCard.ResetCardTransform();   // 重置位置
            currentCard.isAnimatiing = false;
        }
    }
}
