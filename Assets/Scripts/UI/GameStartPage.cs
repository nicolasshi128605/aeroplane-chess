using DG.Tweening;
using Enums;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GameStartPage : MonoBehaviour
    {
        public Button startButton;
        public CanvasGroup buttonCanvas;
        public CanvasGroup canvasGroup;

        private void Awake()
        {
            startButton.onClick.RemoveAllListeners();
            startButton.onClick.AddListener(() =>
            {
                canvasGroup.DOFade(0f, 0.5f).OnComplete(() => { gameObject.SetActive(false); });
                EventCenter.GetInstance().EventTrigger(Events.PlayStartEffect);
            });
        }
    }
}