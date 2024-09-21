using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Enums;
using Managers;
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
            EventCenter.GetInstance().EventTrigger(Events.PlaySound, new SoundManager.SoundConfig
            {
                name = "DiceClick",
                volume = 1f
            });
            var rollResult = Random.Range(1, 7);
            var numberList = GenerateRandomList(10);
            numberList.Add(rollResult);
            StartCoroutine(RollAnimation(numberList));
            image.transform.DOScale(1.3f, 0.5f).SetEase(Ease.OutCubic).OnComplete(() =>
            {
                image.transform.DOScale(1f, 0.5f).SetEase(Ease.InCubic);
            });
            image.transform.DOLocalRotate(new Vector3(0f, 0f, 30f), 0.25f).SetEase(Ease.OutCubic).OnComplete(() =>
            {
                image.transform.DOLocalRotate(new Vector3(0f, 0f, -30f), 0.5f).SetEase(Ease.InOutCubic)
                    .OnComplete(() =>
                    {
                        image.transform.DOLocalRotate(new Vector3(0f, 0f, 0f), 0.25f).SetEase(Ease.InCubic);
                    });
            });

            DOVirtual.DelayedCall(1.2f,
                () =>
                {
                    image.transform.DOScale(1.3f, 0.35f).SetEase(Ease.OutQuint).OnComplete(() =>
                    {
                        image.transform.DOScale(1f, 0.35f).SetEase(Ease.InQuint);
                    });
                });

            DOVirtual.DelayedCall(2f, () =>
            {
                EventCenter.GetInstance().EventTrigger(Events.PlaySound, new SoundManager.SoundConfig
                {
                    name = "DiceDone",
                    volume = 1f
                });
                if (isPlayerDice)
                {
                    EventCenter.GetInstance().EventTrigger(Events.PlayerRollDice, rollResult);
                }
                else
                {
                    EventCenter.GetInstance().EventTrigger(Events.BotRollDice, rollResult);
                }
            });
        }

        public IEnumerator RollAnimation(List<int> animationList)
        {
            var list = new List<int>(animationList);
            while (list.Count > 0)
            {
                ChangeImage(list[0]);
                list.RemoveAt(0);
                yield return new WaitForSeconds(0.1f);
            }
        }

        private void BotRollDice()
        {
            DOVirtual.DelayedCall(1f, RollADice);
        }

        public static List<int> GenerateRandomList(int length)
        {
            List<int> randomList = new List<int>();
            int previousNumber = -1;

            for (int i = 0; i < length; i++)
            {
                int nextNumber;
                do
                {
                    nextNumber = Random.Range(1, 7); // 生成1到6的随机数
                } while (nextNumber == previousNumber); // 确保不与前一个数字重复

                randomList.Add(nextNumber);
                previousNumber = nextNumber;
            }

            return randomList;
        }
    }
}