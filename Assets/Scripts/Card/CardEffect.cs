using System;
using Enums;
using UnityEngine;

namespace Card
{
    public class CardEffect : MonoBehaviour
    {
        public string cardName;

        private void Awake()
        {
            cardName = GetType().Name;
        }

        public virtual void ApplyCardEffect()
        {
            EventCenter.GetInstance().EventTrigger(Events.PlayerPlayCardEnd);
        }
    }
}