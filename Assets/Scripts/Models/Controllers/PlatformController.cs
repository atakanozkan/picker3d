using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects;
public class PlatformController : MonoBehaviour
{
    public List<StageBehaviour> listStages;
    public List<Ball> currentBalls;
    private StageBehaviour currentStageBehaviour;

    private void Start()
    {
        currentStageBehaviour = listStages[LevelManager.instance.GetCurrentStagIndex()];
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
    
    
}
