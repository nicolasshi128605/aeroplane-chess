using Card;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    public Button button;
    public CardSO cardSo;
    public TMP_Text nameDisplay;

    private int currentIndex;


    public void Init(CardSO cardSo, int currentIndex)
    {
        this.cardSo = cardSo;
        this.currentIndex = currentIndex;
        nameDisplay.text = cardSo.name;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() =>
        {
            Global.Player.playerCardManager.PlayCard(currentIndex);
        });
    }
}