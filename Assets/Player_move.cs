using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefaultNamespace;
using DG.Tweening;

public class Player_move : MonoBehaviour
{
    private Dice dice;
    public int CurrentLocation = 0;
    public Vector3[] moveLocations = new Vector3[]
    {
        // First set of tiles (j == 0)
        new Vector3(75f, 115f, 0f),
        new Vector3(45f, 115f, 0f),
        new Vector3(15f, 115f, 0f),
        new Vector3(-15f, 115f, 0f),
        new Vector3(-45f, 115f, 0f),
        new Vector3(-75f, 115f, 0f),

        // First set of tiles (j == 1)
        new Vector3(75f, -115f, 0f),
        new Vector3(45f, -115f, 0f),
        new Vector3(15f, -115f, 0f),
        new Vector3(-15f, -115f, 0f),
        new Vector3(-45f, -115f, 0f),
        new Vector3(-75f, -115f, 0f),

        // Second set of tiles (j == 0)
        new Vector3(115f, 75f, 0f),
        new Vector3(115f, 45f, 0f),
        new Vector3(115f, 15f, 0f),
        new Vector3(115f, -15f, 0f),
        new Vector3(115f, -45f, 0f),
        new Vector3(115f, -75f, 0f),

        // Second set of tiles (j == 1)
        new Vector3(-115f, 75f, 0f),
        new Vector3(-115f, 45f, 0f),
        new Vector3(-115f, 15f, 0f),
        new Vector3(-115f, -15f, 0f),
        new Vector3(-115f, -45f, 0f),
        new Vector3(-115f, -75f, 0f)
    };

    // Start is called before the first frame update
    void Start()
    {
        dice = FindObjectOfType<Dice>();
        if (dice != null)
        {
            // You can call RollAndMovePlayer here if you want to start with a roll
            // RollAndMovePlayer();
        }
        else
        {
            Debug.LogError("Dice component not found on any GameObjects.");
        }
    }

    public void RollAndMovePlayer()
    {
        Debug.Log("RollAndMovePlayer called");
        dice.Play();
        int rollPoint = dice.GetLastRollResult();
        Debug.Log("Last roll result: " + rollPoint);
        if (rollPoint > 0)
        {
            MovePlayer(rollPoint);
        }
        else
        {
            Debug.LogError("Invalid roll point: " + rollPoint);
        }
    }

    void MovePlayer(int rollPoint)
    {
        Sequence moveSequence = DOTween.Sequence();
        for (var i = 0; i < rollPoint; i++)
        {
            CurrentLocation = (CurrentLocation + 1) % moveLocations.Length;
            moveSequence.Append(transform.DOLocalMove(moveLocations[CurrentLocation], 1f).SetEase(Ease.Linear));
        }
        moveSequence.Play();
        Debug.Log("Player moved to location: " + transform.position);
    }
}
