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
            distance = player.transform.position - transform.position;
        }

        private void Update()
        {
            FollowPlayer();
        }

        private void FollowPlayer()
        {
            transform.position = player.transform.position - distance;
        }
    }
}

