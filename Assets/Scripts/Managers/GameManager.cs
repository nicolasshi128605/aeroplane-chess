using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DG.Tweening;
using Enums;
using Tile;
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

        private const string FolderPath = "Assets/Scripts/CardEffect";

        private void Awake()
        {
            EventCenter.GetInstance().AddEventListener(Events.GameStart, SpawnPlayer);
            EventCenter.GetInstance().AddEventListener(Events.PlayerTurnEnd, BotTurnStart);
            EventCenter.GetInstance().AddEventListener(Events.BotTurnEnd, PlayerTurnStart);
            EventCenter.GetInstance().AddEventListener(Events.PlayerPlayCardEnd,
                () =>
                {
                    DOVirtual.DelayedCall(0.2f,
                        () => { EventCenter.GetInstance().EventTrigger(Events.PlayerAttack); });
                });

            EventCenter.GetInstance().AddEventListener(Events.PlayerAttack,
                () =>
                {
                    CrossAttack(true, 1);
                    DOVirtual.DelayedCall(1f,
                        () => { EventCenter.GetInstance().EventTrigger(Events.PlayerTurnEnd); });
                });

            EventCenter.GetInstance().AddEventListener(Events.BotAttack,
                () =>
                {
                    CrossAttack(false, 1);
                    DOVirtual.DelayedCall(1f,
                        () => { EventCenter.GetInstance().EventTrigger(Events.BotTurnEnd); });
                });
        }

        private void Start()
        {
            EventCenter.GetInstance().EventTrigger(Events.GameStart);
            DOVirtual.DelayedCall(0.5f, () => { EventCenter.GetInstance().EventTrigger(Events.PlayerTurnStart); });
        }

        public void PlayerTurnStart()
        {
            DOVirtual.DelayedCall(0.5f, () => { EventCenter.GetInstance().EventTrigger(Events.PlayerTurnStart); });
        }

        public void BotTurnStart()
        {
            DOVirtual.DelayedCall(0.5f, () => { EventCenter.GetInstance().EventTrigger(Events.BotTurnStart); });
        }

        private void SpawnPlayer()
        {
            var player = Instantiate(playerPrefab);
            player.Init(true);
            playerStartTile.SetPlayerHere(player);
            AttachAllCardScripts(player);

            var bot = Instantiate(playerPrefab);
            bot.Init(false);
            botStartTile.SetPlayerHere(bot);
            AttachAllCardScripts(bot);
        }

        private void AttachAllCardScripts(Player.Player player)
        {
            string[] files = Directory.GetFiles(FolderPath, "*.cs", SearchOption.AllDirectories);

            List<Type> monoBehaviourTypes = new List<Type>();

            foreach (string filePath in files)
            {
                string fileName = Path.GetFileNameWithoutExtension(filePath);
                string scriptClassName = fileName;

                Type scriptType = GetTypeByName(scriptClassName);
                if (scriptType != null && scriptType.IsSubclassOf(typeof(MonoBehaviour)))
                {
                    monoBehaviourTypes.Add(scriptType);
                }
            }

            foreach (Type type in monoBehaviourTypes)
            {
                if (player.playerCardManager.GetComponent(type) == null)
                {
                    player.playerCardManager.gameObject.AddComponent(type);
                }
            }

            Global.Player.playerCardManager.LoadAllCards();
        }

        private static Type GetTypeByName(string className)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .FirstOrDefault(type => type.Name == className);
        }

        private void OnApplicationQuit()
        {
            EventCenter.GetInstance().Clear();
        }

        public void CrossAttackAtTile(GameTile tile, bool isPlayer, int damageAmount)
        {
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

            var nextLeftTile = tile.downTile;
            while (nextLeftTile != null)
            {
                if (target.currentTIle == nextLeftTile)
                {
                    target.TakeDamage(damageAmount);
                    return;
                }

                nextLeftTile = nextLeftTile.leftTile;
            }

            var nextRightTile = tile.downTile;
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
            var attack = Instantiate(attackPrefab, user.transform.position, quaternion.identity);
            attack.PlayAttack();
            CrossAttackAtTile(user.currentTIle, isPlayer, damageAmount);
        }
    }
}