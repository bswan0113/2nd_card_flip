using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    AudioSource audioSource;
    public AudioClip clip;
    public AudioClip hurryBGM;



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
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = this.clip;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void ChangeTohurryBGM()
    {
        if (audioSource.clip != hurryBGM)
        {
            audioSource.clip = hurryBGM;
            audioSource.loop = true;
            audioSource.Play();
        }
        
    }
}
