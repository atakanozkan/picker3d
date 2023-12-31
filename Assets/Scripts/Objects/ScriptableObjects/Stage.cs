using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stage",  menuName = "ScriptableObjects/Stages")]
public class Stage : ScriptableObject
{
    public String stageName;
    public int stageIndex;
    public GameObject stageObject;
    public PoolItemType ItemType;
    public int ballNeeded;
    public bool isEndStage;
}
