using UnityEngine;
using UnityEngine.UIElements;

public class HealthBarController : MonoBehaviour
{
    public Transform healthBarTrans;        // Ѫ�� Transform
    private UIDocument healthBarDocument;   // Ѫ�� UIDocument
    private ProgressBar healthBar;          // Ѫ�� UI

    private void Awake()
    {
        healthBarDocument = GetComponent<UIDocument>();
        healthBar = healthBarDocument.rootVisualElement.Q<ProgressBar>("HealthBar");
        MoveToWorldPosition(healthBar, healthBarTrans.position, Vector2.zero);  // ��Ѫ���ƶ��� Ѫ�� Transform λ��
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
    public void SetPosition()
    {
        healthBarDocument = GetComponent<UIDocument>();
        healthBar = healthBarDocument.rootVisualElement.Q<ProgressBar>("HealthBar");
        MoveToWorldPosition(healthBar, healthBarTrans.position, Vector2.zero);  // ��Ѫ���ƶ��� Ѫ�� Transform λ��
    }
}
