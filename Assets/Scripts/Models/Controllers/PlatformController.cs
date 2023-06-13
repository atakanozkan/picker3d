using System;
using System.Collections;
using System.Collections.Generic;
using Models.Builders;
using Models.Managers;
using UnityEngine;

    public class PlatformController : MonoBehaviour
    {
        public List<StageBehaviour> listStages;
        [SerializeField] private int currentIndex;
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
    
        public StageBehaviour GetWantedStage(int levelIndex,int stageIndex)
        {
            PlatformBuilder builder = GameManager.instance.GetPlatformBuilder();
            return listStages[levelIndex * builder.GetEachLevelCount()+stageIndex];
        }
        
        public void UpdateCurrentBehaviour(int stageIndex)
        {
            if (listStages.Count <= stageIndex)
            {
                return;
            }
    
            currentIndex = stageIndex;
            currentStageBehaviour = listStages[currentIndex];
        }
    
        public void SetCurrentIndex(int index)
        {
            currentIndex = index;
        }
    }


