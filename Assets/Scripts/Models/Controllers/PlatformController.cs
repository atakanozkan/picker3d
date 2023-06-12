using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects;
public class PlatformController : MonoBehaviour
{
    public List<StageBehaviour> listStages;
    public List<Ball> currentBalls;
    private int currentIndex;
    private StageBehaviour currentStageBehaviour;

    private void Start()
    {
        currentIndex = LevelManager.instance.GetCurrentStagIndex();
        currentStageBehaviour = listStages[currentIndex];
    }

    public void AddStageToList(StageBehaviour behaviour)
    {
        listStages.Add(behaviour);
    }

    public StageBehaviour GetCurrentStage()
    {
        return currentStageBehaviour;
    }

    public int GetLCurrentLimitBall()
    {
        Stage data = LevelManager.instance.GetCurrentStageData();
        return data.ballNeeded;
    }

    public void UpdateCurrentBehaviour(int stageIndex)
    {
        currentIndex = stageIndex;
        currentStageBehaviour = listStages[currentIndex];
    }
}
