using Card;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    public Button button;
    public CardSO cardSo;
    public TMP_Text nameDisplay;


    public void Init(CardSO cardSo)
    {
        this.cardSo = cardSo;
        nameDisplay.text = cardSo.name;
    }
}