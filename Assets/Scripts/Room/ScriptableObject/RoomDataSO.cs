using UnityEngine;
using UnityEngine.AddressableAssets;

/// <summary>
/// �������� SO
/// </summary>
[CreateAssetMenu(fileName = "RoomDataSO", menuName = "Map/RoomDataSO")]
public class RoomDataSO : ScriptableObject
{
    public Sprite roomIcon;             // ����ͼ��
    public RoomType roomType;           // ��������
    public AssetReference sceneToLoad;  // ��Ҫ���صĳ���������÷����
}
