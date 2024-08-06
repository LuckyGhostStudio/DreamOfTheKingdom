using UnityEngine;
using UnityEngine.UIElements;

public class GameWinPanel : MonoBehaviour
{
    private VisualElement rootElement;  // ���ڵ�Ԫ��

    private Button pickCardButton;      // ѡ���ư�ť
    private Button backToMapButton;     // ���ص�ͼ��ť

    [Header("���ص�ͼ�¼��㲥")]
    public ObjectEventSO loadMapEvent;

    [Header("ѡ�����¼��㲥")]
    public ObjectEventSO pickCardEvent;

    private void Awake()
    {
        rootElement = GetComponent<UIDocument>().rootVisualElement;

        pickCardButton = rootElement.Q<Button>("PickCardButton");
        backToMapButton = rootElement.Q<Button>("BackToMapButton");

        pickCardButton.clicked += OnPickCardButtonClicked;
        backToMapButton.clicked += OnBackToMapButtonClicked;
    }

    /// <summary>
    /// ѡ���ư�ť����ʱ����
    /// </summary>
    private void OnPickCardButtonClicked()
    {
        pickCardEvent.RaiseEvent(null, this);   // ����ѡ�����¼�
    }

    /// <summary>
    /// ���ص�ͼ��ť����ʱ����
    /// </summary>
    private void OnBackToMapButtonClicked()
    {
        loadMapEvent.RaiseEvent(null, this);    // �������ص�ͼ�¼�
    }

    /// <summary>
    /// ����ѡ����ҳ��ʱ����
    /// </summary>
    public void OnFinishPickCardEvent()
    {
        pickCardButton.style.display = DisplayStyle.None;   // ����ѡ���ư�ť
    }
}
