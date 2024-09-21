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

        public int hp;
        public int hpMax;

        public void Init(bool isPlayer)
        {
            this.isPlayer = isPlayer;
            if (isPlayer)
            {
                EventCenter.GetInstance().AddEventListener<int>(Events.PlayerRollDice, PlayerMove);
                Global.Player = this;
                hpMax = 5;
                hp = 5;
            }
            else
            {
                EventCenter.GetInstance().AddEventListener<int>(Events.BotRollDice, PlayerMove);
                Global.Bot = this;
                hpMax = 8;
                hp = 8;
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

        public void Heal(int healAmount)
        {
            if (hp + healAmount >= hpMax)
            {
                hp = hpMax;
            }
            else
            {
                hp += healAmount;
            }

            EventCenter.GetInstance().EventTrigger(Events.UpdateHpUI);
        }

        public void TakeDamage(int damageAmount)
        {
            hp -= damageAmount;
            EventCenter.GetInstance().EventTrigger(Events.UpdateHpUI);
            CheckDeath();
        }

        public void CheckDeath()
        {
            if (hp <= 0)
            {
            }
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