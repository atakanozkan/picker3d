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
    [SerializeField] private GameObject upPoint;
    [SerializeField] private List<Ball> collectedBalls;
    [SerializeField] private MeshRenderer cubeRenderer;
    [SerializeField] private BoxCollider dropLine;
    [SerializeField] private List<GameObject> listRedBarricades;
    [SerializeField] private List<GameObject> barricadeLineParents;
    public bool isPlatformUp = false;
    public bool isCollisonClosed = false;
    private int numberofcollected = 0;
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
        numberofcollected++;
        String collectedMessage = collectedBalls.Count+"/"+numberOfLimit;
        SetText(collectedMessage);
    }

    public void SetLimit(int limit)
    {
        numberOfLimit = limit;
    }

    private void CheckStageDone()
    {
        if (numberofcollected < numberOfLimit)
        {
            return;
        }

        if (!isCollisonClosed)
        {
            isCollisonClosed = true;
        }
        else
        {
            return;
        }
        MoveThePlatform();
        GameManager.instance.onStageEnd?.Invoke();
    }
    
    private void MoveThePlatform()
    {
        Color targetColor = new Color(80/255f, 248/255f, 6/255f); // Color #50F806
        Vector3 targetVector = new Vector3(transform.position.x,
            upPoint.transform.position.y,
            transform.position.z);
            
        transform.DOMove(targetVector,2f)
            .OnStart(() =>
            {
                UnActivateRedBarricades();
                cubeRenderer.material.color = cubeRenderer.material.color;
            })
            .OnUpdate(() => 
            {
                cubeRenderer.material.color = Color.Lerp(cubeRenderer.material.color, targetColor, Time.deltaTime);
            })
            .OnComplete(() =>
            {
                cubeRenderer.material.color = targetColor;
                RotateTheBarricadeLines();
                isPlatformUp = true;
                GameManager.instance.ChangeGameState(GameState.Moving);
               SetLimit(GameManager.instance.GetPlatformController().GetLCurrentLimitBall());
            });
    }
    
    
    
    private IEnumerator DelayCollection()
    {
        yield return new WaitForSeconds(2);
        GameManager.instance.onCollectedBallEvent?.Invoke();
        CheckStageDone();
    }

    private void WaitDroppingFinish(GameState state)
    {
        if (state.HasFlag(GameState.Dropping))
        {
            StartCoroutine(DelayCollection());
        }
    }

    public void UnActivateRedBarricades()
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
    
    public void SetText(String str)
    {
        text.text = str;
    }

    private void UpdateNextPlatform()
    {
        
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
