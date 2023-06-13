using Models.Managers;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField] private Rigidbody myRb;
    
    [SerializeField] private Player player;
    private bool _canMove;
    private Vector3 _lastPosition;
    private float _movement;
    
    private void Update()
    {
        GetHorizontalMovement();
    }
    private void FixedUpdate()
    {   
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (_canMove)
        {
            Vector3 myRbPosition = myRb.transform.position;
            
            myRb.MovePosition(new Vector3(
                myRbPosition.x- (player.forwardSpeed * Time.fixedDeltaTime),
                myRbPosition.y,
                Mathf.Clamp(myRbPosition.z + (_movement * player.horizontalSpeed * Time.fixedDeltaTime), -3.5f, 3f)
            ));
        }
    }

    private void GetHorizontalMovement()
    {
        if (_canMove)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _lastPosition = Input.mousePosition;
            }
            else if (Input.GetMouseButton(0))
            {
                _movement = (Input.mousePosition.x - _lastPosition.x) / Screen.width;
                _lastPosition = Input.mousePosition;
            }
            else
            {
                _movement = 0;
            }   
        }
    }

    public void MoveToNextLevel(int nextLevel)
    {
        PlatformController controller = GameManager.instance.GetPlatformController();
        StageBehaviour behaviour = controller.GetWantedStage(nextLevel, 0);
        Vector3 startPoint = behaviour.startPoint.transform.position;
        
        myRb.MovePosition(
            new Vector3(
                startPoint.x,
                myRb.position.y,
                0f
                )
            );
    }

    private void CheckStateMoving(GameState state)
    {
        if (state.HasFlag(GameState.Moving))
        {
            _canMove = true;
        }
        else
        {
            _canMove = false;
        }
    }
    private void OnEnable()
    {
        GameManager.instance.OnGameStateChanged += CheckStateMoving;
    }

    private void OnDisable()
    {
        GameManager.instance.OnGameStateChanged -= CheckStateMoving;
    }
}

