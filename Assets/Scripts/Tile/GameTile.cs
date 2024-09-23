using System;
using DefaultNamespace.Enums;
using DG.Tweening;
using Enums;
using Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Tile
{
    public class GameTile : MonoBehaviour
    {
        public GameTile nextGameTile;
        public TileType tileType;
        public SpriteRenderer image;
        public Transform nextArrow;

        public GameTile upTile;
        public GameTile downTile;
        public GameTile leftTile;
        public GameTile rightTile;

        public GameTile teleportTile;

        public float rayDistance = 1f;
        public LayerMask tileLayerMask;

        public SpriteRenderer color;
        public SpriteRenderer arrowColor;

        private void Awake()
        {
            Init();

            EventCenter.GetInstance().AddEventListener(Events.PlayStartEffect, () =>
            {
                var currentY = transform.position.y;
                var delay = Random.Range(0.5f, 1.5f);
                color.DOFade(0f, 0f);
                arrowColor.DOFade(0f, 0f);
                color.DOFade(1f, 0.5f).SetDelay(delay);
                transform.DOMoveY(currentY, 0.5f).From(currentY + 2f).SetDelay(delay).OnComplete(() =>
                {
                    arrowColor.DOFade(1f, 0.5f);
                }).SetDelay(delay);
            });
        }

        public void SetPlayerHere(Player.Player player, bool triggerEffect = false, bool triggerSound = true)
        {
            player.transform.position = transform.position;
            player.currentTIle = this;
            if (upTile != null && upTile == nextGameTile)
            {
                player.ChangeToUp();
            }
            else if (downTile != null && downTile == nextGameTile)
            {
                player.ChangeToDown();
            }
            else if (leftTile != null && leftTile == nextGameTile)
            {
                player.ChangeToLeft();
            }
            else if (rightTile != null && rightTile == nextGameTile)
            {
                player.ChangeToRight();
            }

            TileShake();
            if (triggerSound)
            {
                EventCenter.GetInstance().EventTrigger(Events.PlaySound, new SoundManager.SoundConfig
                {
                    name = "step",
                    volume = 0.3f
                });
            }

            if (triggerEffect)
            {
                TileEffect(player);
            }
        }

        public void TileShake()
        {
            image.transform.DOLocalMoveY(-0.2f, 0.15f).SetEase(Ease.OutSine).OnComplete(() =>
            {
                image.transform.DOLocalMoveY(0f, 0.15f).SetEase(Ease.InSine);
            });
        }

        private void TileEffect(Player.Player player)
        {
            var waitTime = 0f;
            switch (tileType)
            {
                case TileType.White:
                    break;
                case TileType.Red:
                    if (player.isPlayer)
                    {
                        Global.DeckManager.DrawACardToPlayHand();
                        waitTime = 1.2f;
                    }

                    break;
                case TileType.Blue:
                    waitTime = 1.2f;
                    player.PlayTeleportEffectUp();
                    EventCenter.GetInstance().EventTrigger(Events.PlaySound, new SoundManager.SoundConfig
                    {
                        name = "Teleport",
                        volume = 1f
                    });
                    DOVirtual.DelayedCall(0.51f, () =>
                    {
                        player.PlayTeleportEffectDown();
                        teleportTile.SetPlayerHere(player);
                        EventCenter.GetInstance().EventTrigger(Events.PlaySound, new SoundManager.SoundConfig
                        {
                            name = "Teleport",
                            volume = 1f
                        });
                    });
                    Debug.Log("Player teleported to the target location from a blue tile!");
                    break;
                case TileType.Green:
                    waitTime = 1.2f;
                    player.Heal(1);
                    Debug.Log("Player stepped on a green tile! HP increased by 1.");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            DOVirtual.DelayedCall(waitTime, () =>
            {
                if (player.isPlayer)
                {
                    EventCenter.GetInstance().EventTrigger(Events.PlayerPlayCardStart);
                }
                else
                {
                    DOVirtual.DelayedCall(0.5f, () => { EventCenter.GetInstance().EventTrigger(Events.BotAttack); });
                }
            });
        }

        public void MovePlayerToNextTile(int remainingMoveStep, Player.Player player)
        {
            if (remainingMoveStep > 0)
            {
                player.Jump(0.5f);
                player.transform.DOMove(nextGameTile.transform.position, 0.5f).SetEase(Ease.Linear).OnComplete(() =>
                {
                    nextGameTile.SetPlayerHere(player);
                    remainingMoveStep -= 1;
                    nextGameTile.MovePlayerToNextTile(remainingMoveStep, player);
                });
            }
            else
            {
                SetPlayerHere(player, true);
            }
        }

        private void Init()
        {
            RotateArrow();
            DetectTiles();
            AssignColorBaseOnType();
        }

        private void RotateArrow()
        {
            var direction = nextGameTile.transform.position - transform.position;

            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            nextArrow.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }

        private void DetectTiles()
        {
            Vector2 upDirection = Vector2.up;
            Vector2 downDirection = Vector2.down;
            Vector2 leftDirection = Vector2.left;
            Vector2 rightDirection = Vector2.right;


            RaycastHit2D hitUp = Physics2D.Raycast((Vector2)transform.position + upDirection, upDirection, rayDistance,
                tileLayerMask);
            RaycastHit2D hitDown = Physics2D.Raycast((Vector2)transform.position + downDirection, downDirection,
                rayDistance, tileLayerMask);
            RaycastHit2D hitLeft = Physics2D.Raycast((Vector2)transform.position + leftDirection, leftDirection,
                rayDistance, tileLayerMask);
            RaycastHit2D hitRight = Physics2D.Raycast((Vector2)transform.position + rightDirection, rightDirection,
                rayDistance, tileLayerMask);

            if (hitUp.collider != null)
            {
                upTile = hitUp.collider.GetComponent<GameTile>();
            }
            else
            {
                upTile = null;
            }

            if (hitDown.collider != null)
            {
                downTile = hitDown.collider.GetComponent<GameTile>();
            }
            else
            {
                downTile = null;
            }

            if (hitLeft.collider != null)
            {
                leftTile = hitLeft.collider.GetComponent<GameTile>();
            }
            else
            {
                leftTile = null;
            }

            if (hitRight.collider != null)
            {
                rightTile = hitRight.collider.GetComponent<GameTile>();
            }
            else
            {
                rightTile = null;
            }
        }

        private void AssignColorBaseOnType()
        {
            switch (tileType)
            {
                case TileType.White:
                    break;
                case TileType.Red:
                    image.color = Color.red;
                    break;
                case TileType.Blue:
                    image.color = Color.blue;
                    break;
                case TileType.Green:
                    image.color = Color.green;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}