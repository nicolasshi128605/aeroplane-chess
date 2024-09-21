using System;
using DG.Tweening;
using Enums;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayerHp : MonoBehaviour
    {
        public bool isPlayer;
        public TMP_Text hpText;
        public Image fillImage;

        private void Awake()
        {
            EventCenter.GetInstance().AddEventListener(Events.UpdateHpUI, UpdateUI);
            EventCenter.GetInstance().AddEventListener<bool>(Events.ShakeHpUI, ShakeUI);
        }

        private void UpdateUI()
        {
            var text = isPlayer
                ? Global.Player.hp + "/" + Global.Player.hpMax
                : Global.Bot.hp + "/" + Global.Bot.hpMax;
            hpText.text = text;
            fillImage.fillAmount = isPlayer
                ? (float)Global.Player.hp / Global.Player.hpMax
                : (float)Global.Bot.hp / Global.Bot.hpMax;
        }

        private void ShakeUI(bool isPlayerShake)
        {
            if (isPlayer == isPlayerShake)
            {
                transform.DOShakePosition(1f, 20f, 100);
            }
        }
    }
}