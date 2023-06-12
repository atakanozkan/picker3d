using System;
using System.Collections.Generic;
using Models.Managers;
using UnityEngine;
using Objects.Poolings;

namespace Models.Builders
{
    public class PlatformBuilder : MonoBehaviour
    {
        public GameObject ballPrefab;
        public GameObject ballsParent;
        public GameObject player;
        public List<GameObject> stageList;

        private PlatformController platformController;
        
        private void Start()
        {
            platformController = GameManager.instance.GetPlatformController();
            GenerateBallsForPool();
            GenerateStages();
        }

        private void GenerateBallsForPool()
        {
            for (int i = 0; i < 100; i++)
            {
                GameObject ball = Instantiate(ballPrefab);
                PoolManager.instance.AddToAvailable(ball.GetComponent<PoolItem>());
            }
        }

        private void GenerateStages()
        {
            foreach (var stage in stageList)
            {
                for (int i = 0; i < 2; i++)
                {
                    GameObject stageObj = Instantiate(stage);
                    PoolManager.instance.AddToAvailable(stageObj.GetComponent<PoolItem>());
                }
            }
        }
        
        public void BuildLevel(Level level,int stageIndex)
        {
            foreach (Stage stage in level.stages)
            {
                Debug.Log(stage);
            }

            for (int index = 0; index < level.stages.Count; index++)
            {
                
                Stage stage = level.stages[index];
                
                PoolItem stageItem = PoolManager.instance.GetFromPool(stage.ItemType,null);
                StageBehaviour stageBehaviour = stageItem.GetComponent<StageBehaviour>();
                
                
                if (index == 0)
                {
                    stageItem.gameObject.transform.position = transform.position;
                }
                else
                {
                    stageItem.gameObject.transform.position =
                        platformController.listStages[index - 1].endPoint.transform.position;
                }

                if (index == stageIndex)
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
    }
}