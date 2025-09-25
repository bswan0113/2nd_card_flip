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
 
    public void ShowUnlockImage()
    {
        if (UnlockImage != null)
        {
            UnlockImage.SetActive(true);
        }
    }

    public void ShowEnding()
    {
        if (Ending != null)
        {
            Ending.SetActive(true);
        }

    }

}

