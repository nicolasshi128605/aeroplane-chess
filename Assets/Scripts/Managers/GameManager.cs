using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DG.Tweening;
using Enums;
using Tile;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public GameTile playerStartTile;
        public GameTile botStartTile;
        public Player.Player playerPrefab;

        private const string FolderPath = "Assets/Scripts/CardEffect";

        private void Awake()
        {
            EventCenter.GetInstance().AddEventListener(Events.GameStart, SpawnPlayer);
            EventCenter.GetInstance().AddEventListener(Events.PlayerTurnEnd, BotTurnStart);
            EventCenter.GetInstance().AddEventListener(Events.BotTurnEnd, PlayerTurnStart);
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
            AttachAllCardScripts(player);
            player.Init(true);
            playerStartTile.SetPlayerHere(player);

            var bot = Instantiate(playerPrefab);
            AttachAllCardScripts(bot);
            bot.Init(false);
            botStartTile.SetPlayerHere(bot);
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
    }
}