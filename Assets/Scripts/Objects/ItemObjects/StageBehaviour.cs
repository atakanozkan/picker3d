using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageBehaviour : MonoBehaviour
{
    public List<GameObject> ballPositionList;
    public BoxCollider dropPoint;
    public GameObject startPoint;
    public GameObject endPoint;
    public bool stageDropDone = false;
    public TextMeshPro TextMeshPro;
}
