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
        for (var j = 0; j < 4; j++)  // 4 rows
        {
            for (var i = 0; i < 6; i++)  // 6 tiles per row
            {
                var tile = Instantiate(tileToGenerated, new Vector3(550 - 150 * i, 375 - 150 * j, 0f) / 5f,
                    Quaternion.identity, transform);
                tile.ChangeColor(i);
            }
        }

        
        
        
        
        
        
        
    }
}