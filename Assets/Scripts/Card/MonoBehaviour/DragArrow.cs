using UnityEngine;

public class DragArrow : MonoBehaviour
{
    private LineRenderer arrowLine;

    public int pointsCount;      // ���Ƶ����
    public float arcModifier;

    private Vector3 mousePos;

    private void Awake()
    {
        arrowLine = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));

        SetArrowPosition();     // ���ü�ͷλ��
    }

    /// <summary>
    /// ���ü�ͷλ��
    /// </summary>
    public void SetArrowPosition()
    {
        Vector3 cardPosition = transform.position;                          // ����λ��
        Vector3 normalizedDirection = (mousePos - cardPosition).normalized; // �ӿ���ָ�����ķ���

        // ��ֱ�ڿ��Ƶ���귽�������
        Vector3 perpendicular = new Vector3(-normalizedDirection.y, normalizedDirection.x, normalizedDirection.z);

        Vector3 offset = perpendicular * arcModifier;                   // ���Ƶ��ƫ������perpendicular ����ƫ�ƣ�

        Vector3 controlPoint = (cardPosition + mousePos) / 2 + offset;  // �м�Ŀ��Ƶ� = ���ĵ��� perpendicular ����ƫ����

        arrowLine.positionCount = pointsCount;                          // ���� arrowLine �ĵ������

        for (int i = 0; i < pointsCount; i++)
        {
            float t = i / (float)(pointsCount - 1);     // �����ϵ�λ�ò���

            Vector3 point = CalculateQuadraticBezierPoint(t, cardPosition, controlPoint, mousePos);
            arrowLine.SetPosition(i, point);    // ���ü�ͷ�ĵ�λ��
        }
    }

    /// <summary>
    /// ������α����������ϵĵ�
    /// </summary>
    /// <param name="t">���߲��� = [0, 1]</param>
    /// <param name="p0">���Ƶ� 0</param>
    /// <param name="p1">���Ƶ� 1</param>
    /// <param name="p2">���Ƶ� 2</param>
    /// <returns></returns>
    Vector3 CalculateQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        Vector3 p = (1 - t) * (1 - t) * p0; // ��һ��
        p += 2 * (1 - t) * t * p1;          // �ڶ���
        p += (t * t) * p2;                  // ������
        return p;
    }
}
