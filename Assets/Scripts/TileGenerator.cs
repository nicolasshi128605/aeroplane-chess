using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    //小白块
    public Tile tileToGenerated;
    public int numberToGenerated;

    private void Awake()
    {
        GenerateTiles();
    }


    public void GenerateTiles()
    {
        for (var j = 0; j < 2; j++)
        {
            for (var i = 0; i < 6; i++)
            {
                var tile = Instantiate(tileToGenerated, new Vector3(375 - 150 * i, j == 0 ? 575 : -575, 0f) / 5f,
                    Quaternion.identity, transform);
                tile.ChangeColor(i);
            }
        }

        for (var j = 0; j < 2; j++)
        {
            for (var i = 0; i < 6; i++)
            {
                var tile = Instantiate(tileToGenerated, new Vector3(j == 0 ? 575 : -575, 375 - 150 * i, 0f) / 5f,
                    Quaternion.identity, transform);
            }
        }
    }
}