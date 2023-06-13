using Models.Collectors;
using UnityEngine;

public class CollectingDetector : MonoBehaviour
{
    public PlatformCollector collector;
    private bool hasEntered;
    private void OnCollisionEnter(Collision collision)
    {
        if (collector.isCollisonClosed)
        {
            return;
        }

        GameObject collider = collision.collider.gameObject;
        if (collider.CompareTag("Ball"))
        {
            Ball ball = collider.GetComponent<Ball>();
            collector.CollectArrivedBall(ball);
            ball.Explode();
        }
    }
}
