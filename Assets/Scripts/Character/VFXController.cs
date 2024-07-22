using Spine.Unity;
using UnityEngine;

public class VFXController : MonoBehaviour
{
    public GameObject buff;      // buff Ч��
    public GameObject debuff;    // debuff Ч��

    private float timeCounter = 0;

    private float buffDuration;     // buff ��������ʱ��
    private float debuffDuration;   // debuff ��������ʱ��

    private void Awake()
    {
        buffDuration = buff.GetComponent<SkeletonAnimation>().skeletonDataAsset.GetSkeletonData(true).FindAnimation("buff").Duration;
        debuffDuration = debuff.GetComponent<SkeletonAnimation>().skeletonDataAsset.GetSkeletonData(true).FindAnimation("debuff").Duration;
    }

    private void Update()
    {
        if (buff.activeInHierarchy)
        {
            timeCounter += Time.deltaTime;
            if (timeCounter > buffDuration)
            {
                timeCounter = 0.0f;
                buff.SetActive(false);
            }
        }

        if (debuff.activeInHierarchy)
        {
            timeCounter += Time.deltaTime;
            if (timeCounter > debuffDuration)
            {
                timeCounter = 0.0f;
                debuff.SetActive(false);
            }
        }
    }
}
