using UnityEngine;
using System.Collections.Generic;

public class PlayerSpawner : MonoBehaviour
{
    public List<Sprite> pointImage;
    public GameObject playerPrefab;

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
            new Vector3(-115f, 115f, 0f), // Top-left corner
            new Vector3(115f, -115f, 0f), // Bottom-right corner
            new Vector3(-115f, -115f, 0f) // Bottom-left corner
        };

        foreach (Vector3 position in spawnPositions)
        {
            Instantiate(playerPrefab, position, Quaternion.identity);
        }
    }
}