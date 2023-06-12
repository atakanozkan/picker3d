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
        }

        void Update()
        {
        }
    }
    
}

