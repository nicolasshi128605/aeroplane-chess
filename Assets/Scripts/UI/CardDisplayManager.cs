using System.Collections.Generic;
using DG.Tweening;
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
            Global.CardDisplayManager = this;
            EventCenter.GetInstance().AddEventListener(Events.UpdateCardInHandUI, UpdateUI);
            EventCenter.GetInstance().AddEventListener(Events.PlayerPlayCardStart, Show);
            EventCenter.GetInstance().AddEventListener(Events.PlayerPlayCardEnd, Hide);
            EventCenter.GetInstance().AddEventListener(Events.PlayerDrawCard, PlayDrawAnimation);

            EventCenter.GetInstance()
                .AddEventListener(Events.PlayStartEffect, () =>
                {
                    DOVirtual.DelayedCall(5f, () =>
                    {
                        for (var index = 0; index < currentCardsInHand.Count; index++)
                        {
                            var card = currentCardsInHand[index];
                            DOVirtual.DelayedCall(0.2f * index, () =>
                            {
                                card.gameObject.SetActive(true);
                                card.transform.DOLocalMoveY(0f, 1f).From(-200f).SetEase(Ease.OutBack);
                                card.canvasGroup.DOFade(1f, 0.5f).From(0f);
                            });
                        }
                    });
                });

            Hide();
        }

        public void StartEffect()
        {
            foreach (var card in currentCardsInHand)
            {
                card.gameObject.SetActive(false);
            }
            
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

        public void PlayDrawAnimation()
        {
            currentCardsInHand[^1].transform.DOLocalMoveY(0f, 1f).From(-200f).SetEase(Ease.OutBack);
            currentCardsInHand[^1].canvasGroup.DOFade(1f, 0.5f).From(0f);
        }

        public void Show()
        {
            canvasGroup.alpha = 1f;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }

        public void Hide()
        {
            canvasGroup.alpha = 0.3f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }
}