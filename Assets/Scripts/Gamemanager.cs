using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;

    public Card firstcard;
    public Card secondcard;

    public Text timeTxT;
    public GameObject endTxt;

    public int CardCount = 0;
    float time = 0.0f;



    AudioSource audioSource;
    public AudioClip clip;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        Time.timeScale = 1.0f;
        audioSource = GetComponent<AudioSource>();


        //시험
        Invoke(nameof(GoHidden), 5f);
        Debug.Log("[Gamemanager] Start OK");
    }
    void Update()
    {
        if (time < 30.0f)
        {
            time += Time.deltaTime;
            timeTxT.text = time.ToString("N2");
        }
        else
        {
            endTxt.SetActive(true);
            Time.timeScale = 0.0f;
        }

    }

    public void Matched()
    {
        if (firstcard.idx == secondcard.idx)
        {
            audioSource.PlayOneShot(clip);
            firstcard.DestoryCard();
            secondcard.DestoryCard();
            CardCount -= 2;
            if (CardCount == 0)
            {
                endTxt.SetActive(true);
                Time.timeScale = 0.0f;
                
            }

        }

        {
            firstcard.CloseCard();
            secondcard.CloseCard();
        }

        firstcard = null;
        secondcard = null;

    }

    //시험용
    void GoHidden()
    {
        Debug.Log("[Gamemanager] GoHidden() called");
        if (StageManager.stageManager != null)
        {
            StageManager.stageManager.forTest();
            AudioManager.ins.HiddenBg();
        }
        else
        {
            Debug.LogWarning("[Gamemanager] StageManager is NULL");
        }
    }


} 

