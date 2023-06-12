using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models.Managers;
using Helpers.Enums;
public class Player : MonoBehaviour
{
    public float horizontalSpeed = 5.0f;
    public float forwardSpeed = 10.0f;
    [SerializeField] private Rigidbody _rigidbody;
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.CompareTag("StageEnd"))
        {
            StageBehaviour stageBehaviour = GameManager.instance.GetPlatformController().GetCurrentStage();
            if (!stageBehaviour.stageDropDone )
            {
                stageBehaviour.stageDropDone = true;
                GameManager.instance.ChangeGameState(GameState.Dropping);
            }
        }
        else if (other.CompareTag("LevelEnd"))
        {
            
        }
        else if (other.CompareTag("Ball"))
        {
            Ball ball = other.GetComponent<Ball>();
            ball.SetInside(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Ball ball = other.GetComponent<Ball>();
            ball.SetInside(false);
        }
    }

    public Rigidbody GetRigidbody()
    {
        return _rigidbody;
    }
}
