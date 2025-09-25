using System;
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
            try
            {
                Vector3 cameraPos = Camera.main.transform.position;
                UnlockImage.transform.position = new Vector3(cameraPos.x, cameraPos.y - 0.4f, 2.0f);
                //UnlockImage.transform.localScale *= 0.5f;
            }
            catch (Exception ignore)
            {
                Console.WriteLine(ignore);
            }
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

