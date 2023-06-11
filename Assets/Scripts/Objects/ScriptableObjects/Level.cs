using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level",  menuName = "ScriptableObjects/Levels")]
public class Level : ScriptableObject
{
    public int LevelIndex;
    public List<Stage> stages;
}
