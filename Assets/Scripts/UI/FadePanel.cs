using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

public class FadePanel : MonoBehaviour
{
    private VisualElement fade; // 渐入渐出面板

    private void Awake()
    {
        fade = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("Fade");
    }

    /// <summary>
    /// 渐入
    /// </summary>
    /// <param name="duration"></param>
    public void FadeIn(float duration)
    {
        // 0 -> 1 渐变
        DOVirtual.Float(0, 1, duration, value =>
        {
            fade.style.opacity = value; // 设置 Fade 不透明度
        }).SetEase(Ease.InQuad);        // 渐变曲线
    }

    /// <summary>
    /// 渐出
    /// </summary>
    /// <param name="duration"></param>
    public void FadeOut(float duration)
    {
        // 1 -> 0 渐变
        DOVirtual.Float(1, 0, duration, value =>
        {
            fade.style.opacity = value; // 设置 Fade 不透明度
        }).SetEase(Ease.InQuad);        // 渐变曲线
    }
}
