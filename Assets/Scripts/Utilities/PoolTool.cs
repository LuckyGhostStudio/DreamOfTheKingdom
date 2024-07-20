using UnityEngine;
using UnityEngine.Pool;

public class PoolTool : MonoBehaviour
{
    public GameObject objPrefab;            // ����Ԥ����
    private ObjectPool<GameObject> pool;    // �����

    private void Awake()
    {
        // ��ʼ�������
        pool = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(objPrefab, transform),    // ��������ʱ����
            actionOnGet: (obj) => obj.SetActive(true),              // �ӳ��л�ȡ����ʱ����
            actionOnRelease: (obj) => obj.SetActive(false),         // �ͷŶ��󵽳���ʱ����
            actionOnDestroy: (obj) => Destroy(obj),                 // ���ٶ���ʱ����
            collectionCheck: false,
            defaultCapacity: 10,
            maxSize: 20
        );

        // Ԥ���������
        PreFillPool(7);
    }

    /// <summary>
    /// Ԥ���������
    /// </summary>
    /// <param name="count">��������</param>
    private void PreFillPool(int count)
    {
        GameObject[] preFillArray = new GameObject[count];

        // ���� count �������ڶ�����У������Ķ���������״̬��
        for (int i = 0; i < count; i++)
        {
            preFillArray[i] = pool.Get();   // ��������
        }
        // ���������Ԥ�ȴ����Ķ����ͷŻسأ����ö���
        foreach (var obj in preFillArray)
        {
            pool.Release(obj);              // �ͷŶ���
        }
    }

    /// <summary>
    /// �ӳ��л�ȡ����
    /// </summary>
    /// <returns></returns>
    public GameObject GetObjectFromPool()
    {
        return pool.Get();
    }

    /// <summary>
    /// ������Żس���
    /// </summary>
    /// <param name="obj">����</param>
    public void ReleaseObjectToPool(GameObject obj)
    {
        pool.Release(obj);
    }
}
