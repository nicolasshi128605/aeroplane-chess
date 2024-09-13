using Enums;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Card
{
    public class CardDrawer : MonoBehaviour
    {
        public Button button;
        public CanvasGroup canvasGroup;

        private void Awake()
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(PlayerDrawACardAndEndTurn);
            EventCenter.GetInstance().AddEventListener(Events.PlayerPlayCardStart, Show);
            EventCenter.GetInstance().AddEventListener(Events.PlayerPlayCardEnd, Hide);
            Hide();
        }

        public void PlayerDrawACardAndEndTurn()
        {
            Global.DeckManager.DrawACardToPlayHand();
            EventCenter.GetInstance().EventTrigger(Events.PlayerPlayCardEnd);
        }

        public void Hide()
        {
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }

        public void Show()
        {
            canvasGroup.alpha = 1f;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
    }
}