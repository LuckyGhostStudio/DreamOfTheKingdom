using UnityEngine;
using UnityEngine.UIElements;

public class HealthBarController : MonoBehaviour
{
    public CharacterBase currentCharacter;  // 当前角色

    public Transform healthBarTrans;        // 血条 Transform

    private VisualElement rootElement;      // 根结点元素

    private ProgressBar healthBar;          // 血条 UI
    private VisualElement defenseElement;   // 防御盾牌 UI
    private Label defenseAmountLabel;       // 防御值 Label

    private void Awake()
    {
        currentCharacter = GetComponent<CharacterBase>();
        rootElement = GetComponent<UIDocument>().rootVisualElement;

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
        healthBar = rootElement.Q<ProgressBar>("HealthBar");                    // 查找 healthBar UI 元素
        healthBar.highValue = currentCharacter.MaxHP;                           // 设置血条最大值
        MoveToWorldPosition(healthBar, healthBarTrans.position, Vector2.zero);  // 将血条移动到 血条 Transform 位置

        defenseElement = rootElement.Q<VisualElement>("Defense");       // 查找防御图标
        defenseAmountLabel = defenseElement.Q<Label>("DefenseAmount");  // 查找防御值 Label
        defenseElement.style.display = DisplayStyle.None;               // 不显示防御图标
    }

    private void Update()
    {
        UpdateHealthBar();
        UpdateDefenseElement();
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
            // 移除样式表
            healthBar.RemoveFromClassList("high-health");
            healthBar.RemoveFromClassList("medium-health");
            healthBar.RemoveFromClassList("low-health");

            float percent = (float)currentCharacter.CurrentHP / (float)currentCharacter.MaxHP;
            if (percent < 0.3f)
            {
                healthBar.AddToClassList("low-health");     // 添加低血量样式
            }
            else if (percent < 0.6f)
            {
                healthBar.AddToClassList("medium-health");  // 添加中血量样式
            }
            else
            {
                healthBar.AddToClassList("high-health");    // 添加高血量样式
            }
        }
    }

    /// <summary>
    /// 更新防御图标
    /// </summary>
    public void UpdateDefenseElement()
    {
        // 设置防御图标
        defenseElement.style.display = currentCharacter.CurrentDefense > 0 ? DisplayStyle.Flex : DisplayStyle.None;
        // 设置防御值
        defenseAmountLabel.text = currentCharacter.CurrentDefense.ToString();
    }
}
