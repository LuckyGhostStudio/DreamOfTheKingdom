using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "RoomDataSO", menuName = "Map/RoomDataSO")]
public class RoomDataSO : ScriptableObject
{
    public Sprite roomIcon;             // ����ͼ��
    public RoomType roomType;           // ��������
    public AssetReference sceneToLoad;  // ��Ҫ���صĳ���������÷����
}
