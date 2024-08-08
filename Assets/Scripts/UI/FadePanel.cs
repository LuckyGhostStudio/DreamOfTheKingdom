using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

public class FadePanel : MonoBehaviour
{
    private VisualElement fade; // ���뽥�����

    private void Awake()
    {
        fade = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("Fade");
    }

    /// <summary>
    /// ����
    /// </summary>
    /// <param name="duration"></param>
    public void FadeIn(float duration)
    {
        // 0 -> 1 ����
        DOVirtual.Float(0, 1, duration, value =>
        {
            fade.style.opacity = value; // ���� Fade ��͸����
        }).SetEase(Ease.InQuad);        // ��������
    }

    /// <summary>
    /// ����
    /// </summary>
    /// <param name="duration"></param>
    public void FadeOut(float duration)
    {
        // 1 -> 0 ����
        DOVirtual.Float(1, 0, duration, value =>
        {
            fade.style.opacity = value; // ���� Fade ��͸����
        }).SetEase(Ease.InQuad);        // ��������
    }
}
