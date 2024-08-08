using UnityEngine;
using UnityEngine.AddressableAssets;

public class PersistentLoader : MonoBehaviour
{
    public AssetReference persistentScene;

    private void Awake()
    {
        Addressables.LoadSceneAsync(persistentScene);   // º”‘ÿ Persistent ≥°æ∞
    }
}
