using System;
using System.Collections.Generic;
using Enums;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace UI
{
    public class Dice : MonoBehaviour
    {
        public List<Sprite> diceImages;
        public Image image;
        public void ChangeImage(int rollResult)
        {
            image.sprite = diceImages[rollResult - 1];
        }

        public void RollADice()
        {
            var rollResult = Random.Range(1, 7);
            ChangeImage(rollResult);
            EventCenter.GetInstance().EventTrigger(Events.PlayerRollDice, rollResult);
        }
    }
}