using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Helpers.Enums;
using Models.Managers;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Player player;
    private bool canMove;
    private float movement;
    private void Start()
    {
        player = GameManager.instance.GetPlayer();
    }

    private void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (!canMove)
        {
            return;
        }
        Vector3 playerPosition = player.transform.position;

        float positionForward = playerPosition.x - (player.forwardSpeed * Time.deltaTime);
    
        player.GetRigidbody().MovePosition(new Vector3(
            positionForward,
            playerPosition.y,
            Mathf.Clamp(playerPosition.z + (movement * player.horizontalSpeed * Time.fixedDeltaTime), -3.5f, 3f)
        ));
    }



    public void CheckStateMoving(GameState state)
    {
        if (state.HasFlag(GameState.Moving))
        {
            canMove = true;
        }
        else
        {
            canMove = false;
        }
    }
    private void OnEnable()
    {
        GameManager.instance.OnGameStateChanged += CheckStateMoving;
    }

    private void OnDisable()
    {
        GameManager.instance.OnGameStateChanged -= CheckStateMoving;
    }

    public void SetHorizontalMovement(float movement)
    {
        this.movement = movement;
    }
}
