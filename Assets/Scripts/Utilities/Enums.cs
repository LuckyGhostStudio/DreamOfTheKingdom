/// <summary>
/// ��������
/// </summary>
public enum RoomType
{
    MinorEnemy, // ��ͨ���˷���
    EliteEnemy, // ��Ӣ���˷���
    Treasure,   // ���䷿��
    Shop,       // �̵귿��
    Rest,       // ��Ϣ����
    Boss        // Boss����
}

/// <summary>
/// ����״̬
/// </summary>
public enum RoomState
{
    Locked,     // ������
    Visited,    // �ѷ���
    Attainable  // �ɷ���
}
