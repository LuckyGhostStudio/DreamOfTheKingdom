using UnityEngine;

public class DragArrow : MonoBehaviour
{
    private LineRenderer arrowLine;

    public int pointsCount;      // 控制点个数
    public float arcModifier;

    private Vector3 mousePos;

    private void Awake()
    {
        arrowLine = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));

        SetArrowPosition();     // 设置箭头位置
    }

    /// <summary>
    /// 设置箭头位置
    /// </summary>
    public void SetArrowPosition()
    {
        Vector3 cardPosition = transform.position;                          // 卡牌位置
        Vector3 normalizedDirection = (mousePos - cardPosition).normalized; // 从卡牌指向鼠标的方向

        // 垂直于卡牌到鼠标方向的向量
        Vector3 perpendicular = new Vector3(-normalizedDirection.y, normalizedDirection.x, normalizedDirection.z);

        Vector3 offset = perpendicular * arcModifier;                   // 控制点的偏移量（perpendicular 方向偏移）

        Vector3 controlPoint = (cardPosition + mousePos) / 2 + offset;  // 中间的控制点 = 中心点向 perpendicular 方向偏移量

        arrowLine.positionCount = pointsCount;                          // 设置 arrowLine 的点的数量

        for (int i = 0; i < pointsCount; i++)
        {
            float t = i / (float)(pointsCount - 1);     // 曲线上的位置参数

            Vector3 point = CalculateQuadraticBezierPoint(t, cardPosition, controlPoint, mousePos);
            arrowLine.SetPosition(i, point);    // 设置箭头的点位置
        }
    }

    /// <summary>
    /// 计算二次贝塞尔曲线上的点
    /// </summary>
    /// <param name="t">曲线参数 = [0, 1]</param>
    /// <param name="p0">控制点 0</param>
    /// <param name="p1">控制点 1</param>
    /// <param name="p2">控制点 2</param>
    /// <returns></returns>
    Vector3 CalculateQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        Vector3 p = (1 - t) * (1 - t) * p0; // 第一项
        p += 2 * (1 - t) * t * p1;          // 第二项
        p += (t * t) * p2;                  // 第三项
        return p;
    }
}
