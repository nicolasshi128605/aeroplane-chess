using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GameStartPage : MonoBehaviour
    {
        public Button startButton;

        private void Awake()
        {
            startButton.onClick.RemoveAllListeners();
            startButton.onClick.AddListener(() => { gameObject.SetActive(false); });
        }
    }
}