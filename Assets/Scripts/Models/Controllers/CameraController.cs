using System;
using Models.Managers;
using UnityEngine;

namespace Models.Controllers
{
    using UnityEngine;

    
    public class CameraController: MonoBehaviour
    {
        public Transform target;
        public float speed = 5f;
        private Vector3 offset;

        private void Start()
        {
            offset = transform.position - target.position;
        }

        private void LateUpdate()
        {
            Vector3 newPosition = target.position + offset;
            newPosition.z = transform.position.z;

            transform.position = Vector3.Lerp(transform.position, newPosition, speed * Time.deltaTime);
        }
    }

}

