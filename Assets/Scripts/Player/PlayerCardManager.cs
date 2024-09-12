using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Player
{
    public class PlayerCardManager : MonoBehaviour
    {
        public List<Card.CardEffect> cardEffects;
        public List<string> cardInHand = new List<string>();

        public void LoadAllCards()
        {
            cardEffects = new List<Card.CardEffect>(GetComponentsInChildren<Card.CardEffect>());
        }

        public void PlayCard(int index)
        {
            foreach (var card in cardEffects)
            {
                if (card.cardName == cardInHand[index])
                {
                    card.ApplyCardEffect();
                    break;
                }
            }

            cardInHand.RemoveAt(index);
            EventCenter.GetInstance().EventTrigger(Events.UpdateCardInHandUI);
        }
    }
}