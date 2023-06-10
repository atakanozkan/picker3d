using System;
using System.Collections;
using System.Collections.Generic;
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

    private void OnEnable()
    {
        GameManager.instance.onPlayerMoveHorizontal += MovePlayerHorizontal;
    }

    private void OnDisable()
    {
        GameManager.instance.onPlayerMoveHorizontal -= MovePlayerHorizontal;
    }
}
