using UnityEngine;
using UnityEngine.UIElements;

public class HealthBarController : MonoBehaviour
{
    public CharacterBase currentCharacter;  // ��ǰ��ɫ

    public Transform healthBarTrans;        // Ѫ�� Transform

    private VisualElement rootElement;      // �����Ԫ��

    private ProgressBar healthBar;          // Ѫ�� UI
    private VisualElement defenseElement;   // �������� UI
    private Label defenseAmountLabel;       // ����ֵ Label

    private void Awake()
    {
        currentCharacter = GetComponent<CharacterBase>();
        rootElement = GetComponent<UIDocument>().rootVisualElement;

        InitHealthBar(); // ��ʼ�� Ѫ��
    }

    /// <summary>
    /// �� UI Ԫ���ƶ���ָ������������
    /// </summary>
    /// <param name="element">UI Ԫ��</param>
    /// <param name="worldPos">��������</param>
    /// <param name="size">�ߴ�</param>
    private void MoveToWorldPosition(VisualElement element, Vector3 worldPos, Vector2 size)
    {
        // ����������ת���� Panel Rect ����
        Rect rect = RuntimePanelUtils.CameraTransformWorldToPanelRect(element.panel, worldPos, size, Camera.main);
        // �� UI Ԫ���ƶ��� rect λ��
        element.transform.position = rect.position;
    }

    [ContextMenu("Set UI Position")]
    public void InitHealthBar()
    {
        healthBar = rootElement.Q<ProgressBar>("HealthBar");                    // ���� healthBar UI Ԫ��
        healthBar.highValue = currentCharacter.MaxHP;                           // ����Ѫ�����ֵ
        MoveToWorldPosition(healthBar, healthBarTrans.position, Vector2.zero);  // ��Ѫ���ƶ��� Ѫ�� Transform λ��

        defenseElement = rootElement.Q<VisualElement>("Defense");       // ���ҷ���ͼ��
        defenseAmountLabel = defenseElement.Q<Label>("DefenseAmount");  // ���ҷ���ֵ Label
        defenseElement.style.display = DisplayStyle.None;               // ����ʾ����ͼ��
    }

    private void Update()
    {
        UpdateHealthBar();
        UpdateDefenseElement();
    }

    /// <summary>
    /// ����Ѫ��
    /// </summary>
    public void UpdateHealthBar()
    {
        // ��ɫ����
        if (currentCharacter.isDead)
        {
            healthBar.style.display = DisplayStyle.None;    // �ر���ʾ UI
            return;
        }

        if (healthBar != null)
        {
            // ����Ѫ����ʾѪ��
            healthBar.title = currentCharacter.CurrentHP.ToString() + "/" + currentCharacter.MaxHP.ToString();
            healthBar.value = currentCharacter.CurrentHP;
            // �Ƴ���ʽ��
            healthBar.RemoveFromClassList("high-health");
            healthBar.RemoveFromClassList("medium-health");
            healthBar.RemoveFromClassList("low-health");

            float percent = (float)currentCharacter.CurrentHP / (float)currentCharacter.MaxHP;
            if (percent < 0.3f)
            {
                healthBar.AddToClassList("low-health");     // ��ӵ�Ѫ����ʽ
            }
            else if (percent < 0.6f)
            {
                healthBar.AddToClassList("medium-health");  // �����Ѫ����ʽ
            }
            else
            {
                healthBar.AddToClassList("high-health");    // ��Ӹ�Ѫ����ʽ
            }
        }
    }

    /// <summary>
    /// ���·���ͼ��
    /// </summary>
    public void UpdateDefenseElement()
    {
        // ���÷���ͼ��
        defenseElement.style.display = currentCharacter.CurrentDefense > 0 ? DisplayStyle.Flex : DisplayStyle.None;
        // ���÷���ֵ
        defenseAmountLabel.text = currentCharacter.CurrentDefense.ToString();
    }
}
