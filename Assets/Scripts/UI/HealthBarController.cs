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

    private VisualElement buffElement;      // buff UI
    private Label buffRoundLabel;           // buff �غ��� Label

    [Header("Buff �� Debuff ͼƬ")]
    public Sprite buffSprite;
    public Sprite debuffSprite;

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

        buffElement = rootElement.Q<VisualElement>("Buff");     // ���� buff ͼ��
        buffRoundLabel = buffElement.Q<Label>("BuffRound");     // ���� buff round ֵ Label
        buffElement.style.display = DisplayStyle.None;          // ����ʾ buff ͼ��
    }

    private void Update()
    {
        UpdateHealthBar();      // ����Ѫ��
        UpdateDefenseElement(); // ���·���ͼ��
        UpdateBuffElement();    // ���� buff ͼ��
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
        // ���÷���ͼ��ɼ���
        defenseElement.style.display = currentCharacter.CurrentDefense > 0 ? DisplayStyle.Flex : DisplayStyle.None;
        // ���÷���ֵ
        defenseAmountLabel.text = currentCharacter.CurrentDefense.ToString();
    }

    /// <summary>
    /// ���� Buff ͼ��
    /// </summary>
    public void UpdateBuffElement()
    {
        // ���� buff round ͼ��ɼ���
        buffElement.style.display = currentCharacter.strengthRound.currentValue >= 1 ? DisplayStyle.Flex : DisplayStyle.None;
        // ���� buff round ͼ��
        buffElement.style.backgroundImage = currentCharacter.baseStrength > 1.0f ? new StyleBackground(buffSprite) : new StyleBackground(debuffSprite);
        // ���� buff round ֵ
        buffRoundLabel.text = currentCharacter.strengthRound.currentValue.ToString();
    }
}
