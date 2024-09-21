using Card;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Button button;
    public CardSO cardSo;
    public TMP_Text nameDisplay;
    public Image image;
    public CanvasGroup canvasGroup;

    private int currentIndex;


    public void Init(CardSO cardSo, int currentIndex)
    {
        this.cardSo = cardSo;
        this.currentIndex = currentIndex;
        nameDisplay.text = cardSo.displayName;
        image.sprite = cardSo.image;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => { Global.Player.playerCardManager.PlayCard(currentIndex); });
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Global.CardDescription.text.text = cardSo.description;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Global.CardDescription.text.text = "";
    }
}