using UnityEngine;
using Models.Managers;
public class Player : MonoBehaviour
{
    public float horizontalSpeed = 5.0f;
    public float forwardSpeed = 10.0f;
    public bool hasEnteredEndStage = false;
    
    [SerializeField] private Rigidbody _rigidbody;
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        
        if (!hasEnteredEndStage && other.CompareTag("StageEnd"))
        {
            StageBehaviour stageBehaviour = GameManager.instance.GetPlatformController().GetCurrentStage();
            if (!stageBehaviour.stageDropDone)
            {
                hasEnteredEndStage = true;
                stageBehaviour.stageDropDone = true;
                GameManager.instance.ChangeGameState(GameState.Dropping);
            }
        }
        else if (other.CompareTag("LevelEnd"))
        {
            bool check = LevelManager.instance.CheckIfLevelLast();
            if (check)
            {
                GameManager.instance.ChangeGameState(GameState.Complete);
            }
            else
            {
                GameManager.instance.ChangeGameState(GameState.Win);
            }
  
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
        else if (other.CompareTag("StageEnd"))
        {
            hasEnteredEndStage = false;
        }
        else if(other.CompareTag("LevelEnd"))
        {
            hasEnteredEndStage = false;
        }
    }

    public Rigidbody GetRigidbody()
    {
        return _rigidbody;
    }

}
