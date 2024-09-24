using DG.Tweening;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class GameEndPage : MonoBehaviour
    {
        public Button restartButton;
        public TMP_Text text;
        public CanvasGroup canvasGroup;

        private void Awake()
        {
            canvasGroup.DOFade(1, 1f).From(0f);
            restartButton.onClick.RemoveAllListeners();
            restartButton.onClick.AddListener(() =>
            {
                Global.GameManager.blackCover.gameObject.SetActive(true);
                Global.GameManager.blackCover.DOFade(1f, 1f).From(0f).OnComplete(() =>
                {
                    EventCenter.GetInstance().Clear();
                    Global.Clear();
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                });
            });
        }
    }
}