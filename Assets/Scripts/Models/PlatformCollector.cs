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
    #region PUBLIC FILEDS
        public bool isPlatformUp = false;
        public bool isCollisonClosed = false;
        private int _numberOfCollected = 0;
        private int _numberOfLimit;
    #endregion

    #region SERIALIZEFIELDS
        [SerializeField] private TextMeshPro text;
        [SerializeField] private GameObject upPoint;
        [SerializeField] private List<Ball> collectedBalls;
        [SerializeField] private MeshRenderer cubeRenderer;
        [SerializeField] private BoxCollider dropLine;
        [SerializeField] private List<GameObject> listRedBarricades;
        [SerializeField] private List<GameObject> barricadeLineParents;
    #endregion

 

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

    private void CheckStageDone()
    {
        if (_numberOfCollected < _numberOfLimit)
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
                EnableMovementNextStage();
            });
    }

    private void EnableMovementNextStage()
    {
        RotateTheBarricadeLines();
        isPlatformUp = true;
        collectedBalls.Clear();
        GameManager.instance.OnStageEnd?.Invoke();
        GameManager.instance.ChangeGameState(GameState.Moving);
    }
    
    private IEnumerator DelayCollection()
    {
        yield return new WaitForSeconds(2);
        GameManager.instance.OnCollectedBallEvent?.Invoke();
        CheckStageDone();
    }


    private void WaitDroppingFinish(GameState state)
    {
        if (state.HasFlag(GameState.Dropping))
        {
            StartCoroutine(DelayCollection());
        }
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
    
    private void OnEnable()
    {
        GameManager.instance.OnGameStateChanged += WaitDroppingFinish;
    }

    private void OnDisable()
    {
        GameManager.instance.OnGameStateChanged -= WaitDroppingFinish;
    }
}
