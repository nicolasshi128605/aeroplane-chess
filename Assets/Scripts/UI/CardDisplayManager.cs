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

        private void Awake()
        {
            EventCenter.GetInstance().AddEventListener(Events.UpdateCardInHandUI, UpdateUI);
        }

        private void UpdateUI()
        {
            foreach (var cardUI in currentCardsInHand)
            {
                Destroy(cardUI);
            }

            currentCardsInHand = new List<CardUI>();

            foreach (var cardName in Global.Player.cardInHand)
            {
                var cardSo = Global.DeckManager.GetCardSo(cardName);
                var cardUI = Instantiate(cardPrefab, transform);
                cardUI.Init(cardSo);
            }
        }
    }
}