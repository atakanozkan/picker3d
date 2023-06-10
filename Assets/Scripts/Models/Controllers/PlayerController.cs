using System;
using System.Collections;
using System.Collections.Generic;
using Helpers.Enums;
using Models.Managers;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Player player;
    private bool canMoveForward;
    private void Start()
    {
        player = GameManager.instance.GetPlayer();
    }

    private void Update()
    {
        MovePlayerForward();
    }

    public void MovePlayerHorizontal(float movement)
    {
        Vector3 playerPosition = player.transform.position;
        Vector3 targetPosition = new Vector3(
            playerPosition.x,
            playerPosition.y,
            playerPosition.z + movement
        );
        player.transform.position = Vector3.Lerp(playerPosition, targetPosition, player.horizontalSpeed * Time.deltaTime);

    }

    public void MovePlayerForward()
    {
        if (!canMoveForward)
        {
            return;
        }
        
        Vector3 playerPosition = player.transform.position;
        Vector3 targetPosition = new Vector3(
            playerPosition.x-player.forwardSpeed*Time.deltaTime,
            playerPosition.y,
            playerPosition.z
        );
        player.transform.position = targetPosition;
    }

    public void CheckStateMoving(GameState state)
    {
        if (state.HasFlag(GameState.Moving))
        {
            canMoveForward = true;
        }
        else
        {
            canMoveForward = false;
        }
    }
    private void OnEnable()
    {
        GameManager.instance.onPlayerMoveHorizontal += MovePlayerHorizontal;
        GameManager.instance.OnGameStateChanged += CheckStateMoving;
    }

    private void OnDisable()
    {
        GameManager.instance.onPlayerMoveHorizontal -= MovePlayerHorizontal;
        GameManager.instance.OnGameStateChanged -= CheckStateMoving;
    }
}
