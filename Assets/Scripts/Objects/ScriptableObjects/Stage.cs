using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stage",  menuName = "ScriptableObjects/Stages")]
public class Stage : ScriptableObject
{
    public String stageName;
    public GameObject stageObject;
    public int stageIndex;
    public PoolItemType ItemType;
    public int ballNeeded;
    public bool isEndStage;
}
