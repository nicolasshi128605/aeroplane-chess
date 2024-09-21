using System.Collections.Generic;
using Enums;
using Managers;
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
                    EventCenter.GetInstance().EventTrigger(Events.PlaySound, new SoundManager.SoundConfig
                    {
                        name = "UseCard",
                        volume = 1f
                    });
                    card.ApplyCardEffect();
                    Global.CardDescription.text.text = "";
                    break;
                }
            }

            cardInHand.RemoveAt(index);
            Global.CardDisplayManager.Hide();
            EventCenter.GetInstance().EventTrigger(Events.UpdateCardInHandUI);
        }
    }
}