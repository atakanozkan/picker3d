using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stage",  menuName = "ScriptableObjects/Stages")]
public class Stage : ScriptableObject
{
    public String stageName;
    public GameObject stageObject;
    public List<GameObject> ballPositionsList;
    public int stageIndex;
    public int ballNeeded;
    public bool isEndStage;
}
