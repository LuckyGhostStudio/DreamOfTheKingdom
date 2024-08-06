using UnityEngine;
using UnityEngine.UIElements;

public class GameOverPanel : MonoBehaviour
{
    private Button backToStartButton;   // ���ؿ�ʼ���水ť

    [Header("���� Menu �¼��㲥")]
    public ObjectEventSO loadMenuEvent;

    private void OnEnable()
    {
        GetComponent<UIDocument>().rootVisualElement.Q<Button>("BackToStartButton").clicked += OnGameOverPanelClicked;
    }

    private void OnGameOverPanelClicked()
    {
        loadMenuEvent.RaiseEvent(null, this);
    }
}
