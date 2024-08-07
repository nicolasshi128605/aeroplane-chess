using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefaultNamespace;
using DG.Tweening;

public class Player_move : MonoBehaviour
{
    private Dice dice;
    public int currentLocation;
    public Sprite upImage;
    public Sprite downImage;
    public Sprite leftImage;
    public Sprite rightImage;
    public Vector3[] moveLocations;
    public SpriteRenderer spriteRenderer;


    public void Init(bool isPlayer)
    {
        if (isPlayer)
        {
            currentLocation = 0;
            UpdateImageToLeft();
        }
        else
        {
            currentLocation = 14;
            UpdateImageToRight();
        }
    }
    
    
    private void Awake()
    {
        //todo 添加新增的格子坐标，并摆好格子位置
        moveLocations = new Vector3[]
        {
            // First set of tiles (j == 0)
            new Vector3(115f, 115f, 0f), // Top-right corner
            new Vector3(75f, 115f, 0f),
            new Vector3(45f, 115f, 0f),
            new Vector3(15f, 115f, 0f),
            new Vector3(-15f, 115f, 0f),
            new Vector3(-45f, 115f, 0f),
            new Vector3(-75f, 115f, 0f),

            // Second set of tiles (j == 1)
            new Vector3(-115f, 115f, 0f), // Top-left corner
            new Vector3(-115f, 75f, 0f),
            new Vector3(-115f, 45f, 0f),
            new Vector3(-115f, 15f, 0f),
            new Vector3(-115f, -15f, 0f),
            new Vector3(-115f, -45f, 0f),
            new Vector3(-115f, -75f, 0f),

            // First set of tiles (j == 1)
            //todo 这一组反了
            new Vector3(-115f, -115f, 0f), // Bottom-left corner
            new Vector3(-75f, -115f, 0f),
            new Vector3(-45f, -115f, 0f),
            new Vector3(-15f, -115f, 0f),
            new Vector3(15f, -115f, 0f),
            new Vector3(45f, -115f, 0f),
            new Vector3(75f, -115f, 0f),

            // Second set of tiles (j == 0)
            new Vector3(115f, -115f, 0f), // Bottom-right corner
            new Vector3(115f, -75f, 0f),
            new Vector3(115f, -45f, 0f),
            new Vector3(115f, -15f, 0f),
            new Vector3(115f, 15f, 0f),
            new Vector3(115f, 45f, 0f),
            new Vector3(115f, 75f, 0f),
        };
    }

    // Start is called before the first frame update
    void Start()
    {
        dice = FindObjectOfType<Dice>();
    }

    void Update()
    {
    }


    // public void RollAndMovePlayer()
    // {
    //     Debug.Log("RollAndMovePlayer called");
    //     dice.Play();
    //     int rollPoint = dice.GetLastRollResult();
    //     Debug.Log("Last roll result: " + rollPoint);
    //     if (rollPoint > 0)
    //     {
    //         MovePlayer(rollPoint);
    //     }
    //     else
    //     {
    //         Debug.LogError("It is not your turn yet!");
    //     }
    // }

    // public void MovePlayer(int rollPoint)
    // {
    //     for (var i = 0; i < rollPoint; i++)
    //     {
    //         currentLocation = (currentLocation + 1) % moveLocations.Length;
    //         transform.DOLocalMove(moveLocations[currentLocation], 1f).SetEase(Ease.Linear);
    //     }
    //
    //     Debug.Log("Player moved to location: " + transform.position);
    // }

    public IEnumerator MovePlayerCoroutine(int rollPoint)
    {
        for (var i = 0; i < rollPoint; i++)
        {
            currentLocation = (currentLocation + 1) % moveLocations.Length;
            UpdatePlayerImage();
            transform.DOMove(moveLocations[currentLocation], 0.2f).SetEase(Ease.Linear);
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void UpdatePlayerImage()
    {
        if (currentLocation == 0)
        {
            UpdateImageToLeft();
        }
        else if (currentLocation == 7)
        {
            UpdateImageToDown();
        }
        else if (currentLocation == 14)
        {
            UpdateImageToRight();
        }
        else if (currentLocation == 21)
        {
            UpdateImageToUp();
        }
    }

    public void UpdateImageToUp()
    {
        spriteRenderer.sprite = upImage;
    }

    public void UpdateImageToDown()
    {
        spriteRenderer.sprite = downImage;
    }

    public void UpdateImageToLeft()
    {
        spriteRenderer.sprite = leftImage;
    }

    public void UpdateImageToRight()
    {
        spriteRenderer.sprite = rightImage;
    }
}