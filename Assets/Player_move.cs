using System.Collections;
using UnityEngine;
using DefaultNamespace;
using DG.Tweening;

public class Player_move : MonoBehaviour
{
    private Dice dice;
    public int CurrentLocation = 0;
    public List<Sprite> pointImage;
    public Vector3[] moveLocations;

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

    public void MovePlayer(int rollPoint)
    {
        for (var i = 0; i < rollPoint; i++)
        {
            CurrentLocation = (CurrentLocation + 1) % moveLocations.Length;
            transform.DOLocalMove(moveLocations[CurrentLocation], 1f).SetEase(Ease.Linear);
            if (CurrentLocation==0)
            {
                spriteRenderer.sprite = pointImage[2f];
            }else if(CurrentLocation==7)
            {
                spriteRenderer.sprite = pointImage[0f];
            }else if(CurrentLocation==14)
            {
                spriteRenderer.sprite = pointImage[3f];
            }else if(CurrentLocation==21)
            {
                spriteRenderer.sprite = pointImage[1f];
            }
        }

        Debug.Log("Player moved to location: " + transform.position);
    }

    public IEnumerator MovePlayerCoroutine(int rollPoint)
    {
        for (var i = 0; i < rollPoint; i++)
        {
            CurrentLocation = (CurrentLocation + 1) % moveLocations.Length;
            transform.DOMove(moveLocations[CurrentLocation], 0.2f).SetEase(Ease.Linear);
            yield return new WaitForSeconds(0.2f);
        }
    }
}