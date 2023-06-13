using System.Collections.Generic;
using Models.Managers;
using UnityEngine;
using Objects.Poolings;

namespace Models.Builders
{
    public class PlatformBuilder : MonoBehaviour
    {
        [SerializeField] private GameObject ballPrefab;
        [SerializeField] private GameObject ballsParent;
        [SerializeField] private GameObject player;
        [SerializeField] private int countEachLevelStages = 4;
        [SerializeField] private PlatformController platformController;
        
        private void Start()
        {
            GenerateBallsForPool();
        }

        private void GenerateBallsForPool()
        {
            for (int i = 0; i < 200; i++)
            {
                GameObject ball = Instantiate(ballPrefab);
                PoolManager.instance.AddToAvailable(ball.GetComponent<PoolItem>());
            }
        }

        public void BuildAllLevels(List<Level> levelList,int stageIndex,int levelIndex)
        {
            foreach (Level level in levelList)
            {
                BuildLevel(level,stageIndex,levelIndex);
            }
        }
        

        private void BuildLevel(Level level,int stageIndex,int levelIndex)
        {
            for (int index = 0; index < level.stages.Count; index++)
            {
                Stage stage = level.stages[index];
                
                GameObject stageObj = Instantiate(stage.stageObject);
                
                StageBehaviour stageBehaviour = stageObj.GetComponent<StageBehaviour>();
                
                
                if (index == 0&& level.LevelIndex == 0)
                {
                    stageObj.gameObject.transform.position = transform.position;
                }
                else
                {
                    stageObj.gameObject.transform.position =
                        platformController.listStages[level.LevelIndex*countEachLevelStages+index - 1].endPoint.transform.position;
                }

                if (index == 0 && levelIndex == level.LevelIndex)
                {
                    player.transform.position = stageBehaviour.startPoint.transform.position;
                }
                
            
                platformController.AddStageToList(stageBehaviour);
                
                    
                foreach (var obj in stageBehaviour.ballPositionList)
                {
                    PoolItem item = PoolManager.instance.GetFromPool(PoolItemType.Ball,ballsParent.transform);
                    item.transform.position = obj.transform.position;
                }

            }
        }

        public int GetEachLevelCount()
        {
            return countEachLevelStages;
        }
    }
}