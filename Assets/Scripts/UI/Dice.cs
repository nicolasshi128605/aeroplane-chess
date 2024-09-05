using System;
using System.Collections.Generic;
using DG.Tweening;
using Enums;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace UI
{
    public class Dice : MonoBehaviour
    {
        public List<Sprite> diceImages;
        public Sprite unknownImage;
        public Image image;
        public CanvasGroup canvasGroup;
        public Button playerRollButton;
        public bool isPlayerDice;

        private void Awake()
        {
            if (isPlayerDice)
            {
                EventCenter.GetInstance().AddEventListener(Events.PlayerTurnStart, DisplayDice);
                EventCenter.GetInstance().AddEventListener(Events.BotTurnStart, HideDice);
            }
            else
            {
                EventCenter.GetInstance().AddEventListener(Events.BotTurnStart, DisplayDice);
                EventCenter.GetInstance().AddEventListener(Events.BotTurnStart, BotRollDice);
                EventCenter.GetInstance().AddEventListener(Events.PlayerTurnStart, HideDice);
            }

            if (playerRollButton != null && isPlayerDice)
            {
                playerRollButton.onClick.RemoveAllListeners();
                playerRollButton.onClick.AddListener(RollADice);
                playerRollButton.onClick.AddListener(() => { playerRollButton.gameObject.SetActive(false); });
            }
        }

        private void Start()
        {
            HideDice();
        }

        public void ShowUnknownImage()
        {
            image.sprite = unknownImage;
        }

        public void DisplayDice()
        {
            canvasGroup.alpha = 1f;
            if (isPlayerDice && playerRollButton != null)
            {
                playerRollButton.gameObject.SetActive(true);
            }
        }

        public void HideDice()
        {
            canvasGroup.alpha = 0.1f;
            ShowUnknownImage();
            if (isPlayerDice && playerRollButton != null)
            {
                playerRollButton.gameObject.SetActive(false);
            }
        }

        public void ChangeImage(int rollResult)
        {
            image.sprite = diceImages[rollResult - 1];
        }

        public void RollADice()
        {
            var rollResult = Random.Range(1, 7);
            ChangeImage(rollResult);
            if (isPlayerDice)
            {
                EventCenter.GetInstance().EventTrigger(Events.PlayerRollDice, rollResult);
            }
            else
            {
                EventCenter.GetInstance().EventTrigger(Events.BotRollDice, rollResult);
            }
        }

        private void BotRollDice()
        {
            DOVirtual.DelayedCall(1f, RollADice);
        }
    }
}