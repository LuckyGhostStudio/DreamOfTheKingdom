using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyAction Data", menuName = "Enemy/EnemyAction Data")]
public class EnemyActionDataSO : ScriptableObject
{
    public List<EnemyAction> actions;   // ��Ϊ�б�
}

/// <summary>
/// ������Ϊ
/// </summary>
[System.Serializable]
public class EnemyAction
{
    public Sprite intentSprite; // ��ͼͼ��
    public Effect effect;       // Ч��
}