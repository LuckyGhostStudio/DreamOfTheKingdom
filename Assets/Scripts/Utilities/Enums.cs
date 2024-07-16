/// <summary>
/// 房间类型
/// </summary>
public enum RoomType
{
    MinorEnemy, // 普通敌人房间
    EliteEnemy, // 精英敌人房间
    Treasure,   // 宝箱房间
    Shop,       // 商店房间
    Rest,       // 休息房间
    Boss        // Boss房间
}

/// <summary>
/// 房间状态
/// </summary>
public enum RoomState
{
    Locked,     // 已锁定
    Visited,    // 已访问
    Attainable  // 可访问
}
