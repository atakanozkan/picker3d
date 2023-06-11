using System;
using UnityEngine;

namespace Models.Builders
{
    public class PlatformBuilder : MonoBehaviour
    {
        public GameObject ballPrefab;
        public GameObject player;

        public void BuildLevel(Level level,int stageIndex)
        {
            Stage stage = level.stages[stageIndex];

            GameObject levelObject = Instantiate(stage.stageObject,transform.position,Quaternion.identity);
            StageBehaviour stageBehaviour = levelObject.GetComponent<StageBehaviour>();

            player.transform.position = stageBehaviour.startPoint.transform.position;

            foreach (var obj in stageBehaviour.ballPositionList)
            {
                Instantiate(ballPrefab, obj.transform.position,Quaternion.identity);
            }

        }
    }
}