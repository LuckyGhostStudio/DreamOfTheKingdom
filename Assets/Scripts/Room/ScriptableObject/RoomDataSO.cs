using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "RoomDataSO", menuName = "Map/RoomDataSO")]
public class RoomDataSO : ScriptableObject
{
    public Sprite roomIcon;             // 房间图标
    public RoomType roomType;           // 房间类型
    public AssetReference sceneToLoad;  // 需要加载的场景（进入该房间后）
}
