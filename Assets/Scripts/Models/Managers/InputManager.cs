using System;
using UnityEngine;
using Helpers.Enums;

namespace Models.Managers
{
    public class InputManager : MonoBehaviour
    {
    
        private Vector3 lastPosition;
        private Vector3 targetPosition;
        private float offset;
        private Camera cam;
        private PlayerController playerController;
        private bool canMoveHorizontal = false;

        private void Start()
        {
            cam= Camera.main;
            playerController = GameManager.instance.GetPlayerController();
        }

        void Update()
        {
            GetHorizontalInput();
        }

        private void GetHorizontalInput()
        {
            if (!GameManager.instance.currentGameState.HasFlag(GameState.Moving))
            {
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                lastPosition = Input.mousePosition;
            }

            else if (Input.GetMouseButton(0))
            {
                targetPosition = Input.mousePosition;
                offset = (targetPosition.x - lastPosition.x) / Screen.width * 2.5f;
                lastPosition = targetPosition;
            }
            else
            {
                offset = 0;
            }
            playerController.SetHorizontalMovement(offset);
        }
    }
    
}

