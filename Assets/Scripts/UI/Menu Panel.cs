using UnityEngine;
using UnityEngine.UIElements;

public class MenuPanel : MonoBehaviour
{
    private VisualElement rootElement;

    private Button newGameButton;   // ����Ϸ��ť
    private Button quitGameButton;  // �˳���Ϸ��ť

    [Header("����Ϸ��ʼ�¼��㲥")]
    public ObjectEventSO newGameEvent;

    private void OnEnable()
    {
        rootElement = GetComponent<UIDocument>().rootVisualElement;

        newGameButton = rootElement.Q<Button>("NewGameButton");
        quitGameButton = rootElement.Q<Button>("QuitGameButton");

        newGameButton.clicked += OnNewGameButtonClicked;
        quitGameButton.clicked += OnQuitGameButtonClicked;
    }

    /// <summary>
    /// �˳���Ϸ��ť����ʱ����
    /// </summary>
    private void OnQuitGameButtonClicked()
    {
        Application.Quit();
    }

    /// <summary>
    /// ��ʼ����Ϸ��ť����ʱ����
    /// </summary>
    private void OnNewGameButtonClicked()
    {
        newGameEvent.RaiseEvent(null, this);    // ��������Ϸ��ʼ�¼�
    }
}
