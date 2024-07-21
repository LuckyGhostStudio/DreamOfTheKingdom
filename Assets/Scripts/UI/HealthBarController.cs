using UnityEngine;
using UnityEngine.UIElements;

public class HealthBarController : MonoBehaviour
{
    public CharacterBase currentCharacter;  // 当前角色

    public Transform healthBarTrans;        // 血条 Transform
    private UIDocument healthBarDocument;   // 血条 UIDocument
    private ProgressBar healthBar;          // 血条 UI

    private void Awake()
    {
        currentCharacter = GetComponent<CharacterBase>();

        InitHealthBar(); // 初始化 血条
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
    public void InitHealthBar()
    {
        healthBarDocument = GetComponent<UIDocument>();
        healthBar = healthBarDocument.rootVisualElement.Q<ProgressBar>("HealthBar");    // 查找 healthBar UI 元素
        healthBar.highValue = currentCharacter.MaxHP;                           // 设置血条最大值
        MoveToWorldPosition(healthBar, healthBarTrans.position, Vector2.zero);  // 将血条移动到 血条 Transform 位置
    }

    private void Update()
    {
        UpdateHealthBar();
    }

    /// <summary>
    /// 更新血条
    /// </summary>
    public void UpdateHealthBar()
    {
        // 角色死亡
        if (currentCharacter.isDead)
        {
            healthBar.style.display = DisplayStyle.None;    // 关闭显示 UI
            return;
        }

        if (healthBar != null)
        {
            // 设置血条显示血量
            healthBar.title = currentCharacter.CurrentHP.ToString() + "/" + currentCharacter.MaxHP.ToString();
            healthBar.value = currentCharacter.CurrentHP;
        }
    }
}
