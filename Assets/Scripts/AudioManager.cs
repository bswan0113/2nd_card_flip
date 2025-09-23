using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //시험
    public static AudioManager ins;

    AudioSource audioSource;
    public AudioClip clip;
    public AudioClip hidden;


    private void Awake()
    {
        if (ins == null)
        {
            ins = this;
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
        audioSource.Play();
    }

    //시험
    public void HiddenBg()
    {
        if (audioSource.clip = this.clip)
        {
            Destroy(gameObject);
            audioSource.clip = this.hidden;
        }
    }

}
