using System;
using System.Collections.Generic;
using Card;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    [Serializable]
    public class CardInfo
    {
        public CardSO cardObject;
        public int cardNumber;
    }

    public class DeckManager : MonoBehaviour
    {
        public List<CardInfo> deckSetup;
        public List<string> deckInGame;

        private List<string> _deckRecord;

        public void Init()
        {
            foreach (var cardInfo in deckSetup)
            {
                for (var i = 0; i < cardInfo.cardNumber; i++)
                {
                    deckInGame.Add(cardInfo.cardObject.cardName);
                }
            }

            _deckRecord = new List<string>(deckInGame);
        }

        public string DrawACard()
        {
            if (deckInGame.Count == 0)
            {
                deckInGame = new List<string>(_deckRecord);
            }

            var randomIndex = Random.Range(0, deckInGame.Count);
            var drawCardName = deckInGame[randomIndex];
            deckInGame.RemoveAt(randomIndex);
            return drawCardName;
        }
    }
}