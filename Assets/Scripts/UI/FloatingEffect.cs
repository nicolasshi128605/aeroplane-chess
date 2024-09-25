using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class FloatingEffect : MonoBehaviour
    {
        public float floatHeight = 0.5f;
        public float floatDuration = 2f;

        private Vector3 originalPosition;

        void Start()
        {
            originalPosition = transform.localPosition;

            StartFloating();
        }

        void StartFloating()
        {
            transform.DOLocalMoveY(originalPosition.y + floatHeight, floatDuration)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo);
        }
    }
}