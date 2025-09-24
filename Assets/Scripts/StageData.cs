using System;
using UnityEngine;

[Serializable]
public class StageData
{
    public int boardSize;
    // [HideInInspector]
    public float timeLimit = 120f;
    [HideInInspector]
    public bool isHiddenStage = false;
}