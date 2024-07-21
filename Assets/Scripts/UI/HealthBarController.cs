using UnityEngine;
using UnityEngine.UIElements;

public class HealthBarController : MonoBehaviour
{
    public Transform healthBarTrans;        // 血条 Transform
    private UIDocument healthBarDocument;   // 血条 UIDocument
    private ProgressBar healthBar;          // 血条 UI

    private void Awake()
    {
        healthBarDocument = GetComponent<UIDocument>();
        healthBar = healthBarDocument.rootVisualElement.Q<ProgressBar>("HealthBar");
        MoveToWorldPosition(healthBar, healthBarTrans.position, Vector2.zero);  // 将血条移动到 血条 Transform 位置
    }

    /// <summary>
    /// 将 UI 元素移动到指定的世界坐标
    /// </summary>
    /// <param name="element">UI 元素</param>
    /// <param name="worldPos">世界坐标</param>
    /// <param name="size">尺寸</param>
    private void MoveToWorldPosition(VisualElement element, Vector3 worldPos, Vector2 size)
    {
        // 将世界坐标转换到 Panel Rect 坐标
        Rect rect = RuntimePanelUtils.CameraTransformWorldToPanelRect(element.panel, worldPos, size, Camera.main);
        // 将 UI 元素移动到 rect 位置
        element.transform.position = rect.position;
    }

    [ContextMenu("Set UI Position")]
    public void SetPosition()
    {
        healthBarDocument = GetComponent<UIDocument>();
        healthBar = healthBarDocument.rootVisualElement.Q<ProgressBar>("HealthBar");
        MoveToWorldPosition(healthBar, healthBarTrans.position, Vector2.zero);  // 将血条移动到 血条 Transform 位置
    }
}
