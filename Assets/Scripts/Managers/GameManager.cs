using Enums;
using Tile;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public GameTile playerStartTile;
        public Player.Player playerPrefab;


        private void Awake()
        {
            EventCenter.GetInstance().AddEventListener(Events.GameStart, SpawnPlayer);
        }

        private void Start()
        {
            EventCenter.GetInstance().EventTrigger(Events.GameStart);
        }

        private void SpawnPlayer()
        {
            var player = Instantiate(playerPrefab);
            playerStartTile.SetPlayerHere(player);
        }

        private void OnApplicationQuit()
        {
            EventCenter.GetInstance().Clear();
        }
    }
}