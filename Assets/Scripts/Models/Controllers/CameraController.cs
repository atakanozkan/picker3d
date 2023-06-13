using System;
using Models.Managers;
using UnityEngine;

namespace Models.Controllers
{
    public class CameraController: MonoBehaviour
    {
        public Transform target;
        public float speed = 5f;
        public Vector3 distanceOffset;

        private void Start()
        {
            UpdateOffset();
        }

        private void LateUpdate()
        {
            Vector3 newPosition = target.position;
            newPosition.x = newPosition.x + distanceOffset.x;
            newPosition.y = distanceOffset.y;
            newPosition.z = distanceOffset.z;
            transform.position = Vector3.Lerp(transform.position, newPosition, speed * Time.deltaTime);
        }

        public void UpdateOffset()
        {
            transform.position = target.position + distanceOffset;
        }
    }

}

