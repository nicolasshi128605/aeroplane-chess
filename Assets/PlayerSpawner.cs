using UnityEngine;
using System.Collections.Generic;
using DefaultNamespace;

public class PlayerSpawner : MonoBehaviour
{
    public List<Sprite> pointImage;
    public GameObject playerPrefab;
    public Dice dice;

    // Start is called before the first frame update
    void Start()
    {
        SpawnPlayersAtCorners();
    }

    void SpawnPlayersAtCorners()
    {
        Vector3[] spawnPositions = new Vector3[]
        {
            new Vector3(115f, 115f, 0f), // Top-right corner
            new Vector3(-115f, -115f, 0f) // Bottom-left corner
        };

        for (var index = 0; index < spawnPositions.Length; index++)
        {
            var position = spawnPositions[index];
            var playerGenerated = Instantiate(playerPrefab, position, Quaternion.identity);

            if (index == 0)
            {
                var playerMove = playerGenerated.GetComponent<Player_move>();
                playerMove.Init(true);
                dice.playerMove = playerMove;
            }
            else
            {
                var playerMove = playerGenerated.GetComponent<Player_move>();
                playerMove.Init(false);
                dice.botMove = playerMove;
            }
        }
    }
}