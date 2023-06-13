using System.Collections.Generic;
using Helpers.Patterns;
using Models.Managers;
using UnityEngine;
using Models.Builders;

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
        

        
        GameManager.instance.GetPlatformBuilder().BuildAllLevels(levelList,currentStageIndex,currentLevelIndex);
    }

    private void TryAgain()
    {
        PlatformBuilder builder = GameManager.instance.GetPlatformBuilder();
        PlatformController controller = GameManager.instance.GetPlatformController();
        Player player = GameManager.instance.GetPlayer();
        foreach (var stage in controller.listStages)
        {
            Destroy(stage.gameObject);
        }
        controller.listStages.Clear();
        controller.SetCurrentIndex(0);
        currentStageIndex = 0;
        PoolManager.instance.ResetAllPools();
        player.hasEnteredEndStage = false;
        builder.BuildAllLevels(levelList,currentStageIndex,currentLevelIndex);
        controller.UpdateCurrentBehaviour(0);
    }

    private void RestartGame()
    {
        PlatformBuilder builder = GameManager.instance.GetPlatformBuilder();
        PlatformController controller = GameManager.instance.GetPlatformController();
        Player player = GameManager.instance.GetPlayer();
        foreach (var stage in controller.listStages)
        {
            Destroy(stage.gameObject);
        }
        controller.listStages.Clear();
        controller.SetCurrentIndex(0);
        currentStageIndex = 0;
        currentLevelIndex = 0;
        SaveData();
        PoolManager.instance.ResetAllPools();
        player.hasEnteredEndStage = false;
        builder.BuildAllLevels(levelList,currentStageIndex,currentLevelIndex);
        controller.UpdateCurrentBehaviour(0);
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt("LevelIndex",currentLevelIndex);
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

    public List<Level> GetALlLevels()
    {
        return levelList;
    }

    public bool CheckIfLevelLast()
    {
        if (currentLevelIndex + 1 >= levelList.Count)
        {
            return true;
        }

        return false;
    }

    private void GoToNextLevel()
    {
        bool check = CheckIfLevelLast();
        if (check)
        {
            TryAgain();
            currentLevelIndex=0;
            currentStageIndex = 0;
            RestartGame();
        }
        else
        {
            currentLevelIndex++;
            currentStageIndex = 0;
            GameManager.instance.GetPlayerController().MoveToNextLevel(currentLevelIndex);
            
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
        GameManager.instance.OnNextLevel += GoToNextLevel;
        GameManager.instance.OnStageEnd += UnlockNextStage;
        GameManager.instance.OnTryAgain += TryAgain;
    }

    private void OnDisable()
    {
        GameManager.instance.OnNextLevel -= GoToNextLevel;
        GameManager.instance.OnStageEnd -= UnlockNextStage;
        GameManager.instance.OnTryAgain -= TryAgain;
    }
}
