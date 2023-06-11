using System;
using Models.Managers;
using UnityEngine;

namespace Models.Controllers
{
    public class CameraController : MonoBehaviour
    {
        private Player player;
        private Vector3 distance;

        private void Start()
        {
            player = GameManager.instance.GetPlayer();
            distance = new Vector3(3f,7f,0f);
        }

        private void Update()
        {
            FollowPlayer();
        }

        private void FollowPlayer()
        {
            transform.position = player.transform.position + distance;
        }
    }
}

