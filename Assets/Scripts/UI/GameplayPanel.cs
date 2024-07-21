using UnityEngine;
using UnityEngine.UIElements;

public class GameplayPanel : MonoBehaviour
{
    private VisualElement rootElement;      // ���ڵ�Ԫ��

    private Label energyAmountLabel;        // ���� Label
    private Label drawDeckAmountLabel;      // �������� Label
    private Label DiscardDeckAmountLabel;   // �������� Label
    private Label turnLabel;                // �غ� Label

    private Button endTurnButton;           // �����غ� Button

    private void OnEnable()
    {
        rootElement = GetComponent<UIDocument>().rootVisualElement;

        energyAmountLabel = rootElement.Q<Label>("EnergyAmount");
        drawDeckAmountLabel = rootElement.Q<Label>("DrawDeckAmount");
        DiscardDeckAmountLabel = rootElement.Q<Label>("DiscardAmount");
        turnLabel = rootElement.Q<Label>("TurnLabel");
        endTurnButton = rootElement.Q<Button>("EndTurn");

        energyAmountLabel.text = "0";
        drawDeckAmountLabel.text = "0";
        DiscardDeckAmountLabel.text = "0";
        turnLabel.text = "��Ϸ��ʼ";
    }

    /// <summary>
    /// ���³��ƶѿ�������
    /// </summary>
    /// <param name="amount">����</param>
    public void UpdateDrawDeckAmount(int amount)
    {
        drawDeckAmountLabel.text = amount.ToString();
    }

    /// <summary>
    /// �������ƶѿ�������
    /// </summary>
    /// <param name="amount">����</param>
    public void UpdateDiscardDeckAmount(int amount)
    {
        DiscardDeckAmountLabel.text = amount.ToString();
    }
}
