using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PickCardPanel : MonoBehaviour
{
    public CardManager cardManager;

    private VisualElement rootElement;      // ���ڵ�Ԫ��

    public VisualTreeAsset cardTemplate;    // ���� UI Document
    private VisualElement cardContainer;    // ���ÿ��� UI ������

    private CardDataSO currentCardData;     // ��ǰ��������

    private Button confirmButton;           // ȷ�ϰ�ť

    private List<Button> cardButtons = new List<Button>();  // ���� Button �б�

    [Header("����ѡ����ҳ���¼��㲥")]
    public ObjectEventSO finishPickCardEvent;

    private void OnEnable()
    {
        rootElement = GetComponent<UIDocument>().rootVisualElement;
        cardContainer = rootElement.Q<VisualElement>("Container");
        confirmButton = rootElement.Q<Button>("ConfirmButton");

        confirmButton.clicked += OnConfirmButtonClicked;

        for (int i = 0; i < 3; i++)
        {
            var card = cardTemplate.Instantiate();          // ���ɿ�Ƭ UI ʵ��
            CardDataSO data = cardManager.GetNewCardData(); // ��ȡ��������

            InitCard(card, data);       // ��ʼ������ UI

            Button cardButton = card.Q<Button>("Card");     // ��ǰ���� UI ��ť 

            cardContainer.Add(card);    // ���Ϊ������������
            cardButtons.Add(cardButton);

            cardButton.clicked += () => OnCardButtonClicked(cardButton, data);  // ��ӵ������
        }
    }

    /// <summary>
    /// ȷ�ϰ�ť����ʱ����
    /// </summary>
    private void OnConfirmButtonClicked()
    {
        cardManager.UnlockCard(currentCardData);    // ��ӵ�ǰ���Ƶ����ƿ�
        finishPickCardEvent.RaiseEvent(null, this); // ��������ѡ��ҳ������¼�
    }

    /// <summary>
    /// ���Ʊ����ʱ����
    /// </summary>
    /// <param name="cardButton">���ư�ť</param>
    private void OnCardButtonClicked(Button cardButton, CardDataSO data)
    {
        currentCardData = data;  // ��ǰ����Ŀ�������

        Debug.Log("Current Card " + currentCardData.cardName);

        // ���õ�ǰ�ѵ���Ŀ��ư�ťΪ������״̬
        for (int i = 0; i < cardButtons.Count; i++)
        {
            cardButtons[i].SetEnabled(cardButtons[i] != cardButton);
        }
    }

    /// <summary>
    /// ��ʼ������ UI ����
    /// </summary>
    /// <param name="cardElement">���� UI Ԫ�ظ��ڵ�</param>
    /// <param name="cardData">��������</param>
    public void InitCard(VisualElement cardElement, CardDataSO cardData)
    {
        VisualElement cardSpriteElement = cardElement.Q<VisualElement>("CardSprite");   // ��ȡ����ͼƬ UI
        Label cardCost = cardElement.Q<Label>("EnergyCost");                            // ��ȡ�������� UI
        Label cardDescription = cardElement.Q<Label>("CardDescription");                // ��ȡ�������� UI
        Label cardType = cardElement.Q<Label>("CardType");                              // ��ȡ�������� UI
        Label cardName = cardElement.Q<Label>("CardName");                              // ��ȡ�������� UI

        cardSpriteElement.style.backgroundImage = new StyleBackground(cardData.cardImage);
        cardCost.text = cardData.cost.ToString();
        cardDescription.text = cardData.description;
        cardName.text = cardData.cardName;

        cardType.text = cardData.cardType switch
        {
            CardType.Attack => "����",
            CardType.Defense => "����",
            CardType.Abilities => "����",
            _ => throw new System.NotImplementedException(),
        };
    }
}
