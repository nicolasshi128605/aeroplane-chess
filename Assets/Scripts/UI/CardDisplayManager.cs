using System.Collections.Generic;
using Enums;
using Managers;
using UnityEngine;

namespace UI
{
    public class CardDisplayManager : MonoBehaviour
    {
        public CardUI cardPrefab;

        public List<CardUI> currentCardsInHand = new List<CardUI>();

        public CanvasGroup canvasGroup;

        private void Awake()
        {
            EventCenter.GetInstance().AddEventListener(Events.UpdateCardInHandUI, UpdateUI);
            EventCenter.GetInstance().AddEventListener(Events.PlayerPlayCardStart, Show);
            EventCenter.GetInstance().AddEventListener(Events.PlayerPlayCardEnd, Hide);
            Hide();
        }

        private void UpdateUI()
        {
            foreach (var cardUI in currentCardsInHand)
            {
                Destroy(cardUI.gameObject);
            }

            currentCardsInHand = new List<CardUI>();

            for (var index = 0; index < Global.Player.playerCardManager.cardInHand.Count; index++)
            {
                var cardName = Global.Player.playerCardManager.cardInHand[index];
                var cardSo = Global.DeckManager.GetCardSo(cardName);
                var cardUI = Instantiate(cardPrefab, transform);
                cardUI.Init(cardSo, index);
                currentCardsInHand.Add(cardUI);
            }
        }

        public void Show()
        {
            canvasGroup.alpha = 1f;
        }

        public void Hide()
        {
            canvasGroup.alpha = 0f;
        }
    }
}