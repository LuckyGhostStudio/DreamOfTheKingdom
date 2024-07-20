using System.Collections.Generic;
using UnityEngine;

public class CardLayoutManager : MonoBehaviour
{
    public bool isHorizontal;
    public float maxWidth = 7.0f;       // 一排卡牌总宽度（最左卡牌位置到最右卡牌位置距离）
    public float currentSpacing = 2.0f; // 卡牌当前的间隙

    [Header("弧形参数")]
    public float angleBetweenCards = 7f;    // 两张卡牌之间的角度差
    public float radius = 17f;              // 弧形半径

    public Vector3 centerPoint;     // 卡牌布局中心点坐标

    private List<Vector3> cardPositions = new List<Vector3>();          // 卡牌位置列表
    private List<Quaternion> cardRotations = new List<Quaternion>();    // 卡牌旋转列表

    private void Awake()
    {
        centerPoint = isHorizontal ? Vector3.up * -4.5f : Vector3.up * -21.5f;
    }

    /// <summary>
    /// 返回卡牌位置和旋转
    /// </summary>
    /// <param name="index">卡牌编号</param>
    /// <param name="totalCards">卡牌总数</param>
    /// <returns></returns>
    public CardTransform GetCardTransform(int index, int totalCards)
    {
        CalculatePositions(totalCards, isHorizontal);
        return new CardTransform(cardPositions[index], cardRotations[index]);
    }

    /// <summary>
    /// 计算卡牌位置
    /// </summary>
    /// <param name="numberOfCards">卡牌数量</param>
    /// <param name="horizontal">水平布局</param>
    private void CalculatePositions(int numberOfCards, bool horizontal)
    {
        cardPositions.Clear();
        cardRotations.Clear();

        if (horizontal)
        {
            float currentWidth = currentSpacing * (numberOfCards - 1);  // 当前宽度
            float totalWidth = Mathf.Min(currentWidth, maxWidth);       // 总宽度取最小值

            currentSpacing = totalWidth > 0 ? totalWidth / (numberOfCards - 1) : 0;    // 根据总宽度计算卡牌间隙

            for (int i = 0; i < numberOfCards; i++)
            {
                float xPos = -totalWidth / 2 + i * currentSpacing;   // 最左侧 + i 个间隙

                Vector3 pos = new Vector3(xPos, centerPoint.y, 0.0f);   // 位置
                Quaternion rot = Quaternion.identity;                   // 旋转

                cardPositions.Add(pos);
                cardRotations.Add(rot);
            }
        }
        else
        {
            float cardAngle = angleBetweenCards * (numberOfCards - 1) / 2;  // 当前角度

            for (int i = 0; i < numberOfCards; i++)
            {
                float zRotAngle = cardAngle - i * angleBetweenCards;                // z 轴旋转值
                Vector3 pos = FanCardPosition(zRotAngle);                           // 卡牌位置
                Quaternion rot = Quaternion.Euler(new Vector3(0, 0, zRotAngle));    // 旋转值

                cardPositions.Add(pos);
                cardRotations.Add(rot);
            }
        }
    }

    /// <summary>
    /// 计算扇形卡牌位置
    /// </summary>
    /// <param name="angle">角度</param>
    /// <returns></returns>
    private Vector3 FanCardPosition(float angle)
    {
        return new Vector3(
            centerPoint.x - Mathf.Sin(angle * Mathf.Deg2Rad) * radius,
            centerPoint.y + Mathf.Cos(angle * Mathf.Deg2Rad) * radius,
            0
        );
    }
}
