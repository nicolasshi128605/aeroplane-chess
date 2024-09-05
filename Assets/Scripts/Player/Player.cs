using Enums;
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

        private void Awake()
        {
            EventCenter.GetInstance().AddEventListener<int>(Events.PlayerRollDice, PlayerMove);
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