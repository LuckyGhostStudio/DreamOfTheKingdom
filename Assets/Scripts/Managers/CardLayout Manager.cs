using System.Collections.Generic;
using UnityEngine;

public class CardLayoutManager : MonoBehaviour
{
    public bool isHorizontal;
    public float maxWidth = 7.0f;       // һ�ſ����ܿ�ȣ�������λ�õ����ҿ���λ�þ��룩
    public float currentSpacing = 2.0f; // ���Ƶ�ǰ�ļ�϶

    [Header("���β���")]
    public float angleBetweenCards = 7f;    // ���ſ���֮��ĽǶȲ�
    public float radius = 17f;              // ���ΰ뾶

    public Vector3 centerPoint;     // ���Ʋ������ĵ�����

    private List<Vector3> cardPositions = new List<Vector3>();          // ����λ���б�
    private List<Quaternion> cardRotations = new List<Quaternion>();    // ������ת�б�

    private void Awake()
    {
        centerPoint = isHorizontal ? Vector3.up * -4.5f : Vector3.up * -21.5f;
    }

    /// <summary>
    /// ���ؿ���λ�ú���ת
    /// </summary>
    /// <param name="index">���Ʊ��</param>
    /// <param name="totalCards">��������</param>
    /// <returns></returns>
    public CardTransform GetCardTransform(int index, int totalCards)
    {
        CalculatePositions(totalCards, isHorizontal);
        return new CardTransform(cardPositions[index], cardRotations[index]);
    }

    /// <summary>
    /// ���㿨��λ��
    /// </summary>
    /// <param name="numberOfCards">��������</param>
    /// <param name="horizontal">ˮƽ����</param>
    private void CalculatePositions(int numberOfCards, bool horizontal)
    {
        cardPositions.Clear();
        cardRotations.Clear();

        if (horizontal)
        {
            float currentWidth = currentSpacing * (numberOfCards - 1);  // ��ǰ���
            float totalWidth = Mathf.Min(currentWidth, maxWidth);       // �ܿ��ȡ��Сֵ

            currentSpacing = totalWidth > 0 ? totalWidth / (numberOfCards - 1) : 0;    // �����ܿ�ȼ��㿨�Ƽ�϶

            for (int i = 0; i < numberOfCards; i++)
            {
                float xPos = -totalWidth / 2 + i * currentSpacing;   // ����� + i ����϶

                Vector3 pos = new Vector3(xPos, centerPoint.y, 0.0f);   // λ��
                Quaternion rot = Quaternion.identity;                   // ��ת

                cardPositions.Add(pos);
                cardRotations.Add(rot);
            }
        }
        else
        {
            float cardAngle = angleBetweenCards * (numberOfCards - 1) / 2;  // ��ǰ�Ƕ�

            for (int i = 0; i < numberOfCards; i++)
            {
                float zRotAngle = cardAngle - i * angleBetweenCards;                // z ����תֵ
                Vector3 pos = FanCardPosition(zRotAngle);                           // ����λ��
                Quaternion rot = Quaternion.Euler(new Vector3(0, 0, zRotAngle));    // ��תֵ

                cardPositions.Add(pos);
                cardRotations.Add(rot);
            }
        }
    }

    /// <summary>
    /// �������ο���λ��
    /// </summary>
    /// <param name="angle">�Ƕ�</param>
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
