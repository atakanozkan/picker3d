using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Helpers.Enums;
using Models.Managers;
using TMPro;
using UnityEngine;

public class PlatformCollector : MonoBehaviour
{
    public bool isPlatformUp = false;
    public bool isCollisonClosed = false;
    private const float DROP_TIME = 2f;
    private int _numberOfCollected = 0;
    private int _numberOfLimit;
    private bool timerStart;
    private float remainingDropTime;
    private int lastLimit;

    [SerializeField] private TextMeshPro text;
    [SerializeField] private GameObject upPoint;
    [SerializeField] private List<Ball> collectedBalls;
    [SerializeField] private MeshRenderer cubeRenderer;
    [SerializeField] private BoxCollider dropLine;
    [SerializeField] private List<GameObject> listRedBarricades;
    [SerializeField] private List<GameObject> barricadeLineParents;
    [SerializeField] private PoolItemType stageType;

    private void Start()
    {
        SetLimit(GameManager.instance.GetPlatformController().GetLCurrentLimitBall());
        String initialMessage = "0/" + _numberOfLimit;
        SetText(initialMessage);
    }

    private void Update()
    {
        int tempLimit = GameManager.instance.GetPlatformController().GetLCurrentLimitBall();
        if (_numberOfLimit != tempLimit)
        {
            SetLimit(tempLimit);
        }

        StartAndCheckTimer();
    }

    private void StartAndCheckTimer()
    {
        if (timerStart)
        {
            if (isPlatformUp)
            {
                timerStart = false;
                return;
            }
            if (remainingDropTime > 0)
            {
                remainingDropTime -= Time.deltaTime;
            }
            else
            {
                if (_numberOfCollected >= lastLimit)
                {
                    GameManager.instance.OnCollectedBallEvent?.Invoke();
                    CheckStageDone();
                }
                else
                {
                    GameManager.instance.ChangeGameState(GameState.Lose);
                }
                
                remainingDropTime = 0;
                timerStart = false;
            }
        }
    }

    private void CheckStageDone()
    {
        if (!isCollisonClosed)
        {
            isCollisonClosed = true;
        }
        MoveThePlatform();
    }
    
    private void MoveThePlatform()
    {
        Color targetColor = new Color(80/255f, 248/255f, 6/255f); // Color #50F806
        Vector3 targetVector = new Vector3(transform.position.x,
            upPoint.transform.position.y,
            transform.position.z);

        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(targetVector,2f).OnStart(UnActivateRedBarricades));    
        sequence.Insert(0, cubeRenderer.material.DOColor(targetColor, 2f));
        
        sequence.OnComplete(() => 
        {
            GameManager.instance.ChangeGameState(GameState.Moving);
            cubeRenderer.material.color = targetColor;
            EnableMovementNextStage();
        });
    }

    private void EnableMovementNextStage()
    {

        RotateTheBarricadeLines();
        isPlatformUp = true;
        collectedBalls.Clear();
        GameManager.instance.OnStageEnd?.Invoke();
        Debug.Log("XXXXXXXXX");

    }

    private void WaitDroppingFinish(GameState state)
    {
        PoolItemType itemType = LevelManager.instance.GetCurrentStageData().ItemType;
        if (state.HasFlag(GameState.Dropping) && itemType == stageType)
        {
            lastLimit = _numberOfLimit;
            remainingDropTime = DROP_TIME;
            timerStart = true;
        }
    }
    
    public void CollectArrivedBall(Ball ball)
    {
        collectedBalls.Add(ball);
        _numberOfCollected++;
        String collectedMessage = collectedBalls.Count+"/"+_numberOfLimit;
        SetText(collectedMessage);
    }

    private void SetLimit(int limit)
    {
        _numberOfLimit = limit;
        String resetMessage = 0+"/"+_numberOfLimit;
        SetText(resetMessage);
    }

    private void UnActivateRedBarricades()
    {
        foreach (var barricade in listRedBarricades)
        {
            barricade.SetActive(false);
        }
    }

    private void RotateTheBarricadeLines()
    {
        foreach (var barricade in barricadeLineParents )
        {
            barricade.transform.DORotate(new Vector3(90, 0, 0), 1.5f);
        }
    }

    private void SetText(String str)
    {
        text.text = str;
    }

    private void ResetCollector()
    {
        _numberOfCollected = 0;
        collectedBalls.Clear();
        isPlatformUp = false;
        isCollisonClosed = false;
    }

    private void OnEnable()
    {
        GameManager.instance.OnGameStateChanged += WaitDroppingFinish;
        GameManager.instance.OnTryAgain += ResetCollector;
    }

    private void OnDisable()
    {
        GameManager.instance.OnGameStateChanged -= WaitDroppingFinish;
        GameManager.instance.OnTryAgain -= ResetCollector;
    }
}
