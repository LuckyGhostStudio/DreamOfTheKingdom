using UnityEngine;
using UnityEngine.Pool;

public class PoolTool : MonoBehaviour
{
    public GameObject objPrefab;            // 对象预制体
    private ObjectPool<GameObject> pool;    // 对象池

    private void Awake()
    {
        // 初始化对象池
        pool = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(objPrefab, transform),    // 创建对象时调用
            actionOnGet: (obj) => obj.SetActive(true),              // 从池中获取对象时调用
            actionOnRelease: (obj) => obj.SetActive(false),         // 释放对象到池中时调用
            actionOnDestroy: (obj) => Destroy(obj),                 // 销毁对象时调用
            collectionCheck: false,
            defaultCapacity: 10,
            maxSize: 20
        );

        // 预先填充对象池
        PreFillPool(7);
    }

    /// <summary>
    /// 预先填充对象池
    /// </summary>
    /// <param name="count">对象数量</param>
    private void PreFillPool(int count)
    {
        GameObject[] preFillArray = new GameObject[count];

        // 创建 count 个对象在对象池中（创建的对象是启用状态）
        for (int i = 0; i < count; i++)
        {
            preFillArray[i] = pool.Get();   // 创建对象
        }
        // 将对象池中预先创建的对象释放回池（禁用对象）
        foreach (var obj in preFillArray)
        {
            pool.Release(obj);              // 释放对象
        }
    }

    /// <summary>
    /// 从池中获取对象
    /// </summary>
    /// <returns></returns>
    public GameObject GetObjectFromPool()
    {
        return pool.Get();
    }

    /// <summary>
    /// 将对象放回池中
    /// </summary>
    /// <param name="obj">对象</param>
    public void ReleaseObjectToPool(GameObject obj)
    {
        pool.Release(obj);
    }
}
