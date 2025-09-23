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
} 

