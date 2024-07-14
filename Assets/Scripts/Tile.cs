using UnityEngine;

public class Tile : MonoBehaviour
{
    public void ChangeColor(int i)
    {
        var spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.color = (i % 4) switch
        {
            0 => new Color(1f, 0f, 0f, 0.5f),
            1 => Color.blue,
            2 => Color.green,
            3 => Color.yellow,
            _ => spriteRenderer.color
        };
    }
}