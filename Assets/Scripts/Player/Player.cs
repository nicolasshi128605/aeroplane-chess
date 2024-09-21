using DG.Tweening;
using Enums;
using Managers;
using Tile;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        public SpriteRenderer image;
        public Transform shadow;
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

        public void Jump(float duration)
        {
            image.transform.DOLocalMoveY(1.6f, duration / 2f).From(0.6f).SetEase(Ease.OutSine).OnComplete(() =>
            {
                image.transform.DOLocalMoveY(0.6f, duration / 2f).From(1.6f).SetEase(Ease.InSine);
            });
            shadow.DOScale(0.8f, duration / 2f).From(1f).SetEase(Ease.OutSine).OnComplete(() =>
            {
                shadow.DOScale(1f, duration / 2f).From(0.8f).SetEase(Ease.InSine);
            });
        }

        public void JumpAttack(float duration)
        {
            image.transform.DOLocalMoveY(2f, duration / 2f).From(0.6f).SetEase(Ease.OutQuint).OnComplete(() =>
            {
                image.transform.DOLocalMoveY(0.6f, duration / 2f).From(2f).SetEase(Ease.InQuint).OnComplete(() =>
                {
                    image.transform.DOShakePosition(0.2f, 0.5f, 100);
                });
            });
            shadow.DOScale(0.6f, duration / 2f).From(1f).SetEase(Ease.OutQuint).OnComplete(() =>
            {
                shadow.DOScale(1f, duration / 2f).From(0.6f).SetEase(Ease.InQuint);
            });
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
            EventCenter.GetInstance().EventTrigger(Events.ShakeHpUI, isPlayer);
            image.transform.DOShakePosition(0.8f, 0.5f, 100);
            image.DOColor(new Color(1f, 0f, 0f, 1f), 0.2f);
            DOVirtual.DelayedCall(0.4f, () => { image.DOColor(new Color(1f, 1f, 1f, 1f), 0.2f); });
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