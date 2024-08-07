using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using TreeEditor;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class Dice : MonoBehaviour
    {
        public List<Sprite> pointImage;
        public bool canRoll;
        public bool BotRoll;
        private int lastRollResult;

        public Button rollButton;

        public Player_move playerMove;
        public Player_move botMove;

        public bool isPlayerMoving;

        private void Start()
        {
            rollButton.interactable = true;
            canRoll = true;
            isPlayerMoving = true;
        }

        public void Play()
        {
            Roll();
        }

        [Button]
        public int Roll()
        {
            Debug.Log("Rolled set to true");
            if (!canRoll) return -1;

            rollButton.interactable = false;
            canRoll = false;
            var result = Random.Range(1, 7);
            var animationList = new List<int>();
            for (var i = 0; i < 5; i++)
            {
                var randomValue = Random.Range(1, 7);
                animationList.Add(randomValue);
            }

            animationList.Add(result);

            PlayRollAnimation(animationList);

            lastRollResult = result;
            DOVirtual.DelayedCall(5f, () =>
            {
                rollButton.interactable = true;
                canRoll = true;
            });
            return result;
        }

        private void PlayRollAnimation(List<int> animationList)
        {
            var spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            StartCoroutine(ChangeImage(0, animationList, spriteRenderer));
        }

        private IEnumerator ChangeImage(int count, List<int> animationList, SpriteRenderer spriteRenderer)
        {
            if (count == animationList.Count)
            {
                TryMove(animationList[^1]);
                PlayRollEndEffect();
                yield break;
            }

            var currentIndex = animationList[count] - 1;
            spriteRenderer.sprite = pointImage[currentIndex];

            yield return new WaitForSeconds(0.1f + count * 0.1f);
            StartCoroutine(ChangeImage(count + 1, animationList, spriteRenderer));
        }

        public int GetLastRollResult()
        {
            return lastRollResult;
        }

        public void PlayRollEndEffect()
        {
            transform.DOScale(1.5f, 0.2f).OnComplete(() =>
            {
                transform.DOScale(1f, 0.2f);
            });
        }

        public void TryMove(int movePoint)
        {
            if (isPlayerMoving)
            {
                MovePlayer(movePoint);
            }
            else
            {
                MoveBot(movePoint);
            }
        }

        public void MovePlayer(int movePoint)
        {
            isPlayerMoving = !isPlayerMoving;
            StartCoroutine(playerMove.MovePlayerCoroutine(movePoint));
            //playerMove.MovePlayer(movePoint);
        }

        public void MoveBot(int movePoint)
        {
            isPlayerMoving = !isPlayerMoving;
            StartCoroutine(botMove.MovePlayerCoroutine(movePoint));
        }
    }
}