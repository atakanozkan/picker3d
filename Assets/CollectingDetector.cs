using System;
using System.Collections;
using System.Collections.Generic;
using Models.Managers;
using Objects;
using UnityEngine;

public class CollectingDetector : MonoBehaviour
{
    public PlatformCollector collector;
    private void OnCollisionEnter(Collision collision)
    {
        if (collector.isCollisonClosed)
        {
            return;
        }
        GameObject collider = collision.collider.gameObject;
        if (collider.CompareTag("Ball"))
        {
            Debug.Log("Ball Received!");
            Ball ball = collider.GetComponent<Ball>();
            collector.CollectArrivedBall(ball);
            ball.Explode();
        }
    }
}
