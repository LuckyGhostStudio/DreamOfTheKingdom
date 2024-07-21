using System;

/// <summary>
/// ��������
/// </summary>
[Flags]     // ���Ϊ�ɶ�ѡ��ʹ�� 2^n ����ʹ��ֵ֮ͬ���� or ����ֵΪ 1��
public enum RoomType
{
    MinorEnemy = 1, // ��ͨ���˷���
    EliteEnemy = 2, // ��Ӣ���˷���
    Treasure = 4,   // ���䷿��
    Shop = 8,       // �̵귿��
    Rest = 16,      // ��Ϣ����
    Boss = 32       // Boss����
}

/// <summary>
/// ����״̬
/// </summary>
public enum RoomState
{
    Locked,     // ������
    Visited,    // �ѷ���
    Attainable  // �ɵ���
}

/// <summary>
/// ��������
/// </summary>
public enum CardType
{
    Attack,     // ����
    Defense,    // ����
    Abilities,  // ����
}

/// <summary>
/// Ч��Ŀ������
/// </summary>
public enum EffectTargetType
{
    Self,   // �Լ�
    Target, // Ŀ��
    All     // ȫ��
}