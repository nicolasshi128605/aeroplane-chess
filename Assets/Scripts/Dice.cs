using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class Dice : MonoBehaviour
    {
        public List<Sprite> pointImage;
        public bool canRoll;

        private void Start()
        {
            canRoll = true;
        }

        public void Play()
        {
            var rollPoint = Roll();

            //玩家移动
        }

        [Button]
        public int Roll()
        {
            if (!canRoll) return -1;

            canRoll = false;
            DOVirtual.DelayedCall(8f, () => { canRoll = true; });

            var result = Random.Range(1, 7);

            var animationList = new List<int>();
            for (var i = 0; i < 5; i++)
            {
                var randomValue = Random.Range(1, 7);
                animationList.Add(randomValue);
            }

            animationList.Add(result);

            PlayRollAnimation(animationList);

            return result;
        }

        private void PlayRollAnimation(List<int> animationList)
        {
            var spriteRenderer = GetComponentInChildren<SpriteRenderer>();

            StartCoroutine(ChangeImage(0, animationList, spriteRenderer));
        }

        private IEnumerator ChangeImage(int count, List<int> animationList, SpriteRenderer spriteRenderer)
        {
            if (count == animationList.Count) yield break;

            var currentIndex = animationList[count] - 1;
            spriteRenderer.sprite = pointImage[currentIndex];

            yield return new WaitForSeconds(0.1f + count * 0.1f);

            StartCoroutine(ChangeImage(count + 1, animationList, spriteRenderer));
        }
    }
}