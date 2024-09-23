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

        private void Awake()
        {
            restartButton.onClick.RemoveAllListeners();
            restartButton.onClick.AddListener(() =>
            {
                EventCenter.GetInstance().Clear();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            });
        }
    }
}