using DG.Tweening;
using Enums;
using Tile;
using TMPro;
using UI;
using Unity.Mathematics;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public GameTile playerStartTile;
        public GameTile botStartTile;
        public Player.Player playerPrefab;
        public Attack.Attack attackPrefab;

        public GameStartPage gameStartPage;
        public GameEndPage gameEndPage;

        public CanvasGroup blackCover;
        public GameObject cover;

        public bool isGameEnd;

        private void Awake()
        {
            blackCover.gameObject.SetActive(true);
            DOVirtual.DelayedCall(1f,
                () =>
                {
                    blackCover.DOFade(0f, 1f).From(1f).OnComplete(() =>
                    {
                        blackCover.gameObject.SetActive(false);
                        gameStartPage.startButton.gameObject.SetActive(true);
                        gameStartPage.buttonCanvas.DOFade(1f, 0.5f).From(0f);
                    });
                });
            Global.GameManager = this;
            gameStartPage.gameObject.SetActive(true);
            gameStartPage.startButton.gameObject.SetActive(false);
            gameEndPage.gameObject.SetActive(false);
            EventCenter.GetInstance().AddEventListener(Events.GameStart, SpawnPlayer);
            EventCenter.GetInstance().AddEventListener(Events.PlayerTurnEnd, BotTurnStart);
            EventCenter.GetInstance().AddEventListener(Events.BotTurnEnd, PlayerTurnStart);
            EventCenter.GetInstance().AddEventListener(Events.PlayerPlayCardEnd,
                () =>
                {
                    DOVirtual.DelayedCall(0.5f,
                        () => { EventCenter.GetInstance().EventTrigger(Events.PlayerAttack); });
                });

            EventCenter.GetInstance().AddEventListener(Events.PlayerAttack,
                () =>
                {
                    if (Global.doubleDMG)
                    {
                        CrossAttack(true, 2);
                    }
                    else
                    {
                        CrossAttack(true, 1);
                    }

                    DOVirtual.DelayedCall(1.5f,
                        () =>
                        {
                            Global.Clear();
                            EventCenter.GetInstance().EventTrigger(Events.PlayerTurnEnd);
                        });
                });

            EventCenter.GetInstance().AddEventListener(Events.BotAttack,
                () =>
                {
                    CrossAttack(false, 1);
                    DOVirtual.DelayedCall(1.5f,
                        () => { EventCenter.GetInstance().EventTrigger(Events.BotTurnEnd); });
                });

            EventCenter.GetInstance().AddEventListener(Events.GameLose,
                () =>
                {
                    EventCenter.GetInstance().EventTrigger(Events.PlaySound, new SoundManager.SoundConfig
                    {
                        name = "Lose",
                        volume = 1f
                    });
                    DOVirtual.DelayedCall(1.5f,
                        () =>
                        {
                            gameEndPage.gameObject.SetActive(true);
                            gameEndPage.text.text = "Game Over";
                        });
                });

            EventCenter.GetInstance().AddEventListener(Events.GameWIn,
                () =>
                {
                    EventCenter.GetInstance().EventTrigger(Events.PlaySound, new SoundManager.SoundConfig
                    {
                        name = "Win",
                        volume = 1f
                    });
                    DOVirtual.DelayedCall(1.5f,
                        () =>
                        {
                            gameEndPage.gameObject.SetActive(true);
                            gameEndPage.text.text = "You Win";
                        });
                });

            EventCenter.GetInstance().AddEventListener(Events.PlayStartEffect,
                () =>
                {
                    DOVirtual.DelayedCall(5f,
                        () => { EventCenter.GetInstance().EventTrigger(Events.PlayerTurnStart); });
                });
        }

        private void Start()
        {
            EventCenter.GetInstance().EventTrigger(Events.GameStart);
        }

        public void PlayerTurnStart()
        {
            if (isGameEnd) return;
            DOVirtual.DelayedCall(0.5f, () =>
            {
                if (isGameEnd) return;
                EventCenter.GetInstance().EventTrigger(Events.PlayerTurnStart);
            });
        }

        public void BotTurnStart()
        {
            if (isGameEnd) return;
            DOVirtual.DelayedCall(0.5f, () =>
            {
                if (isGameEnd) return;
                EventCenter.GetInstance().EventTrigger(Events.BotTurnStart);
            });
        }

        private void SpawnPlayer()
        {
            var player = Instantiate(playerPrefab);
            player.Init(true);
            playerStartTile.SetPlayerHere(player, triggerSound: false);
            AttachAllCardScripts(player);

            var bot = Instantiate(playerPrefab);
            bot.Init(false);
            botStartTile.SetPlayerHere(bot, triggerSound: false);
            AttachAllCardScripts(bot);

            EventCenter.GetInstance().EventTrigger(Events.UpdateHpUI);
        }

        private void AttachAllCardScripts(Player.Player player)
        {
            Global.Player.playerCardManager.LoadAllCards();
        }

        private void OnApplicationQuit()
        {
            Global.Clear();
            EventCenter.GetInstance().Clear();
        }

        public void CrossAttackAtTile(GameTile tile, bool isPlayer, int damageAmount)
        {
            tile.TileShake();
            var target = isPlayer ? Global.Bot : Global.Player;
            if (target.currentTIle == tile)
            {
                target.TakeDamage(damageAmount);
                return;
            }

            var nextUpTile = tile.upTile;
            while (nextUpTile != null)
            {
                if (target.currentTIle == nextUpTile)
                {
                    target.TakeDamage(damageAmount);
                    return;
                }

                nextUpTile = nextUpTile.upTile;
            }

            var nextDownTile = tile.downTile;
            while (nextDownTile != null)
            {
                if (target.currentTIle == nextDownTile)
                {
                    target.TakeDamage(damageAmount);
                    return;
                }

                nextDownTile = nextDownTile.downTile;
            }

            var nextLeftTile = tile.leftTile;
            while (nextLeftTile != null)
            {
                if (target.currentTIle == nextLeftTile)
                {
                    target.TakeDamage(damageAmount);
                    return;
                }

                nextLeftTile = nextLeftTile.leftTile;
            }

            var nextRightTile = tile.rightTile;
            while (nextRightTile != null)
            {
                if (target.currentTIle == nextRightTile)
                {
                    target.TakeDamage(damageAmount);
                    return;
                }

                nextRightTile = nextRightTile.rightTile;
            }
        }

        public void CrossAttack(bool isPlayer, int damageAmount)
        {
            var user = isPlayer ? Global.Player : Global.Bot;
            user.JumpAttack(1f);
            DOVirtual.DelayedCall(1f, () =>
            {
                var attack = Instantiate(attackPrefab, user.transform.position, quaternion.identity);
                attack.PlayAttack();
                CrossAttackAtTile(user.currentTIle, isPlayer, damageAmount);
                EventCenter.GetInstance().EventTrigger(Events.PlaySound, new SoundManager.SoundConfig
                {
                    name = "Attack",
                    volume = 1f
                });
            });
        }
    }
}