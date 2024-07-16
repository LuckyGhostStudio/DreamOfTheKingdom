using System;

/// <summary>
/// 房间类型
/// </summary>
[Flags]     // 标记为可多选（使用 2^n 可以使不同值之间做 or 运算值为 1）
public enum RoomType
{
    MinorEnemy = 1, // 普通敌人房间
    EliteEnemy = 2, // 精英敌人房间
    Treasure = 4,   // 宝箱房间
    Shop = 8,       // 商店房间
    Rest = 16,      // 休息房间
    Boss = 32       // Boss房间
}

/// <summary>
/// 房间状态
/// </summary>
public enum RoomState
{
    Locked,     // 已锁定
    Visited,    // 已访问
    Attainable  // 可到达
}
