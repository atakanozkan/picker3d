using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Helpers.Enums;
using Models.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class PlatformCollector : MonoBehaviour
{
    [SerializeField] private TextMeshPro text;
    [SerializeField] private GameObject barricadeLine;
    [SerializeField] private BoxCollider _collider;
    [SerializeField] private GameObject upPoint;
    [SerializeField] private List<Ball> collectedBalls; 
    public bool isPlatformUp = false;
    public bool isCollisonClosed = false;
    private int numberOfLimit;

    private void Start()
    {
        SetLimit(GameManager.instance.GetPlatformController().GetLCurrentLimitBall());
        String initialMessage = "0/" + numberOfLimit;
        SetText(initialMessage);
    }

    public void CollectArrivedBall(Ball ball)
    {
        collectedBalls.Add(ball);
        String collectedMessage = collectedBalls.Count+"/"+numberOfLimit;
        SetText(collectedMessage);
    }

    public void SetLimit(int limit)
    {
        numberOfLimit = limit;
    }

    private void MoveThePlatform()
    {
        transform.DOMoveY(upPoint.transform.position.y, 1f, true).OnComplete(
            () => { isPlatformUp = true; GameManager.instance.ChangeGameState(GameState.Moving); }
        );
    }
    
    private IEnumerator DelayCollection()
    {
        yield return new WaitForSeconds(2);
        GameManager.instance.onCollectedBallEvent?.Invoke();
        isCollisonClosed = true;
        MoveThePlatform();
    }

    private void WaitDroppingFinish(GameState state)
    {
        if (state.HasFlag(GameState.Dropping))
        {
            StartCoroutine(DelayCollection());
        }
    }
    
    public void SetText(String str)
    {
        text.text = str;
    }

    private void OnEnable()
    {
        GameManager.instance.OnGameStateChanged += WaitDroppingFinish;
    }

    private void OnDisable()
    {
        GameManager.instance.OnGameStateChanged -= WaitDroppingFinish;
    }
}
