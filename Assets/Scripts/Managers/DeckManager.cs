using System;
using System.Collections.Generic;
using Card;
using DG.Tweening;
using Enums;
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

        private Dictionary<string, CardSO> _cardNameToCardSoDict;

        private void Awake()
        {
            Global.DeckManager = this;
            EventCenter.GetInstance().AddEventListener(Events.GameStart, Init);

            EventCenter.GetInstance().AddEventListener(Events.GameStart, () =>
            {
                DOVirtual.DelayedCall(0.5f, () =>
                {
                    for (int i = 0; i < 3; i++)
                    {
                        Global.Player.playerCardManager.cardInHand.Add(DrawACard());
                    }

                    EventCenter.GetInstance().EventTrigger(Events.UpdateCardInHandUI);
                    Global.CardDisplayManager.StartEffect();
                });
            });
        }

        public void Init()
        {
            _cardNameToCardSoDict = new Dictionary<string, CardSO>();
            foreach (var cardInfo in deckSetup)
            {
                for (var i = 0; i < cardInfo.cardNumber; i++)
                {
                    deckInGame.Add(cardInfo.cardObject.cardName);
                }

                _cardNameToCardSoDict.Add(cardInfo.cardObject.name, cardInfo.cardObject);
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

        public CardSO GetCardSo(string cardName)
        {
            return _cardNameToCardSoDict[cardName];
        }

        public void DrawACardToPlayHand()
        {
            if (Global.Player.playerCardManager.cardInHand.Count >= 7) return;
            EventCenter.GetInstance().EventTrigger(Events.PlaySound, new SoundManager.SoundConfig
            {
                name = "Draw",
                volume = 1f
            });
            Global.Player.playerCardManager.cardInHand.Add(DrawACard());
            EventCenter.GetInstance().EventTrigger(Events.UpdateCardInHandUI);
            EventCenter.GetInstance().EventTrigger(Events.PlayerDrawCard);
        }
    }
}