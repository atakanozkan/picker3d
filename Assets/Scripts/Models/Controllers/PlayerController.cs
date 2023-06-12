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
    [SerializeField] private Rigidbody myRb;
    private bool canMove;
    private Vector3 lastPosition;
    private float movement;
    private void Start()
    {
        player = GameManager.instance.GetPlayer();
    }

    private void Update()
    {
        GetHorizontalMovement();
    }
    private void FixedUpdate()
    {   
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (canMove)
        {
            myRb.MovePosition(new Vector3(
                myRb.transform.position.x- (player.forwardSpeed * Time.fixedDeltaTime),
                myRb.transform.position.y,
                Mathf.Clamp(myRb.transform.position.z + (movement * player.horizontalSpeed * Time.fixedDeltaTime), -3.5f, 3f)
            ));
        }
    }

    private void GetHorizontalMovement()
    {
        if (canMove)
        {
            if (Input.GetMouseButtonDown(0))
            {
                lastPosition = Input.mousePosition;
            }
            else if (Input.GetMouseButton(0))
            {
                movement = (Input.mousePosition.x - lastPosition.x) / Screen.width * 2.5f;
                lastPosition = Input.mousePosition;
            }
            else
            {
                movement = 0;
            }   
        }
    }

    private void CheckStateMoving(GameState state)
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