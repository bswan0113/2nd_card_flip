using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject UnlockImage;
    public GameObject Ending;


    private void Awake ()
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
        if (UnlockImage != null)
        {
            UnlockImage.SetActive(true);
        }
   
    }
    public void Endings()
    {
        //3스테이지 클리어시 엔딩화면
        if (Ending != null)
        {
            Ending.SetActive(true);
        }
  
    }
}

