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
        private bool canMoveHorizontal = false;

        private void Start()
        {
            cam= Camera.main;
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
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                
                if (Physics.Raycast(ray, out hit))
                {
                    lastPosition = hit.point;
                }
            }

            if (Input.GetMouseButton(0))
            {
                canMoveHorizontal = true;
            }

            if (Input.GetMouseButtonUp(0))
            {
                canMoveHorizontal = false;
            }

            if (canMoveHorizontal)
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    float diff = hit.point.x - targetPosition.x;
                    targetPosition = hit.point;
                    offset = targetPosition.z - lastPosition.z;
                    lastPosition = targetPosition;
                    GameManager.instance.onPlayerMoveHorizontal?.Invoke(offset);
                }
                
            }}
    }
    
}

