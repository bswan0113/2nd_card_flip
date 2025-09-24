using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void OpenStage()
    {
        //스테이지1,2 클리어시 작동
        if (stagePn != null)
        {
            stagePn.SetActive(true);
        }
        else
        {
            Debug.LogWarning("아래스테이지가 클리어되지 않았습니다");
        }
    }
    public void Ending()
    {
        //3스테이지 클리어시 엔딩화면
        if (end != null)
        {
            end.SetActive(true);
        }
        else
        {
            Debug.LogWarning("아래스테이지가 클리어되지 않았습니다");
        }
    }
}

