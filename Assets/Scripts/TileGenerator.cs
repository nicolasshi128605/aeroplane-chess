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
        for (var i = 0; i < numberToGenerated; i++)
        {
            Instantiate(tileToGenerated, new Vector3(i * 100f, 0f, 0f), new Quaternion(), transform);
        }
    }
}