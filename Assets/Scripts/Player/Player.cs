using System.Collections.Generic;
using Enums;
using Managers;
using Tile;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        public SpriteRenderer image;
        public Sprite upImage;
        public Sprite downImage;
        public Sprite leftImage;
        public Sprite rightImage;
        public PlayerCardManager playerCardManager;

        public GameTile currentTIle;
        public bool isPlayer;

        public void Init(bool isPlayer)
        {
            this.isPlayer = isPlayer;
            if (isPlayer)
            {
                EventCenter.GetInstance().AddEventListener<int>(Events.PlayerRollDice, PlayerMove);
                Global.Player = this;
            }
            else
            {
                EventCenter.GetInstance().AddEventListener<int>(Events.BotRollDice, PlayerMove);
            }
        }

        public void PlayerMove(int moveStep)
        {
            currentTIle.MovePlayerToNextTile(moveStep, this);
        }

        public void ChangeToUp()
        {
            image.sprite = upImage;
        }

        public void ChangeToDown()
        {
            image.sprite = downImage;
        }

        public void ChangeToLeft()
        {
            image.sprite = leftImage;
        }

        public void ChangeToRight()
        {
            image.sprite = rightImage;
        }
    }
}