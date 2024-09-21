using DG.Tweening;
using UnityEngine;

namespace Attack
{
    public class Attack : MonoBehaviour
    {
        public SpriteRenderer horizontalAttack;
        public SpriteRenderer verticalAttack;

        private void Start()
        {
            horizontalAttack.DOFade(0f, 0f);
            verticalAttack.DOFade(0f, 0f);
        }

        public void PlayAttack()
        {
            //appear
            horizontalAttack.transform.localScale = new Vector3(100f, 0.5f, 1f);
            verticalAttack.transform.localScale = new Vector3(0.5f, 100f, 1f);
            horizontalAttack.DOFade(1f, 0.1f).From(0f);
            verticalAttack.DOFade(1f, 0.1f).From(0f);


            //disappear
            horizontalAttack.transform.DOScaleY(0f, 0.6f).SetDelay(0.2f).SetEase(Ease.InCubic);
            verticalAttack.transform.DOScaleX(0f, 0.6f).SetDelay(0.2f).SetEase(Ease.InCubic);
        }
    }
}