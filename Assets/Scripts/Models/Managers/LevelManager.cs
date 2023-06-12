using System;
using System.Collections;
using System.Collections.Generic;
using Helpers.Enums;
using Helpers.Patterns;
using Models.Managers;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private List<Level> levelList;
    [SerializeField] private int currentLevelIndex;
    [SerializeField] private int currentStageIndex;

    private void Start()
    {
        LoadCurrentLevel();

    }

    private void LoadCurrentLevel()
    {
        currentLevelIndex = PlayerPrefs.GetInt("LevelIndex", 0);
        currentStageIndex = PlayerPrefs.GetInt("StageIndex", 0);

        int indexLevel = 0;
        
        if (currentLevelIndex == 0 && currentStageIndex== 0) // not played level is 0
        {
            currentLevelIndex = 0;
            currentStageIndex = 0;
            SaveData();
        }
        

        
        GameManager.instance.GetPlatformBuilder().BuildLevel(levelList[indexLevel],currentStageIndex);
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt("LevelIndex",currentLevelIndex);
        PlayerPrefs.SetInt("StageIndex",currentStageIndex);
    }

    public int GetCurrentLevelIndex()
    {
        return currentLevelIndex;
    }

    public int GetCurrentStagIndex()
    {
        return currentStageIndex;
    }

    public Level GetCurrentLevelData()
    {
        return levelList[currentLevelIndex];
    }

    public Stage GetCurrentStageData()
    {
        return levelList[currentLevelIndex].stages[currentStageIndex];
    }

    private void GoToNextLevel(GameState state)
    {
        if (state == GameState.Win)
        {
            currentLevelIndex++;
            currentStageIndex = 0;
        }
    }

    private void UnlockNextStage()
    {
        CheckAndIncreaseStage();
        GameManager.instance.GetPlatformController().UpdateCurrentBehaviour(currentStageIndex);
    }

    private void CheckAndIncreaseStage()
    {
        Level level = GetCurrentLevelData();
        if (level.stages.Count <= currentStageIndex)
        {
            return;
        }

        currentStageIndex++;
    }
    
    private void OnApplicationQuit()
    {
        SaveData();
    }

    private void OnEnable()
    {
        GameManager.instance.OnGameStateChanged += GoToNextLevel;
        GameManager.instance.onStageEnd += UnlockNextStage;
    }

    private void OnDisable()
    {
        GameManager.instance.OnGameStateChanged -= GoToNextLevel;
        GameManager.instance.onStageEnd -= UnlockNextStage;
    }
}
