using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager stageManager;

    public GameObject hiddenBg;

    void Awake()
    {
        if (stageManager == null) stageManager = this;
        else if (stageManager != this) { Destroy(gameObject); return; }

        Debug.Log("[StageManager] Awake OK");
    }

    public void forTest()
    {
        hiddenBg.SetActive(true);
    }
}