using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyAction Data", menuName = "Enemy/EnemyAction Data")]
public class EnemyActionDataSO : ScriptableObject
{
    public List<EnemyAction> actions;   // 行为列表
}

/// <summary>
/// 敌人行为
/// </summary>
[System.Serializable]
public class EnemyAction
{
    public Sprite intentSprite; // 意图图标
    public Effect effect;       // 效果
}