using UnityEngine;
using UnityEngine.UIElements;

public class RestRoomPanel : MonoBehaviour
{
    private VisualElement rootElement;

    private Button restButton;      // ��Ϣ��ť
    private Button backToMapButton; // ���ص�ͼ��ť

    public Effect restHealEffect;   // ��Ϣ��ѪЧ��

    private CharacterBase player;

    [Header("���ص�ͼ�¼��㲥")]
    public ObjectEventSO loadMapEvent;

    private void OnEnable()
    {
        rootElement = GetComponent<UIDocument>().rootVisualElement;

        restButton = rootElement.Q<Button>("RestButton");
        backToMapButton = rootElement.Q<Button>("BackToMapButton");

        player = FindAnyObjectByType<Player>(FindObjectsInactive.Include);  // ���� Player ��ʹδ����

        restButton.clicked += OnRestButtonClicked;
        backToMapButton.clicked += OnBackToMapButtonClicked;
    }

    /// <summary>
    /// ��Ϣ��ť����ʱ����
    /// </summary>
    private void OnRestButtonClicked()
    {
        restHealEffect.Execute(player, null);   // ִ�л�ѪЧ��
        restButton.SetEnabled(false);
    }

    /// <summary>
    /// ���ص�ͼ��ť����ʱ����
    /// </summary>
    private void OnBackToMapButtonClicked()
    {
        loadMapEvent.RaiseEvent(null, this);    // �������ص�ͼ�¼�
    }
}
