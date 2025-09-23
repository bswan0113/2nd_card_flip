using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    AudioSource audioSource;
   
   //BGM �������Դϴ�.
    public AudioClip clip; //�⺻ BGM, ���ϱ�� �⺻ BGM�� �����̸��� ����Ʈ���� clip���� �س����ϴ�.
    public AudioClip hurryBGM; // ���ѽð� �� �ȳ������� BGM
    public AudioClip clearBGM; // �������� Ŭ���� BGM
    public AudioClip failBGM; // �������� ���� BGM
    public AudioClip hiddenBGM; // ���� �������� ���� BGM
    public AudioClip StartBGM; // (StartScene) ���� BGM


    //ȿ���� �������Դϴ�.
    public AudioClip matchMiss; //ī�� Ʋ��
    public AudioClip Matched; //ī�� ����
    public AudioClip cardFlip; //ī�� ������ ȿ����
    public AudioClip buttonClickSFX; //��ư Ŭ�� ȿ���� (���� ���� ��ư���� ���밡���� ȿ����)

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
    // ���ϴ� BGM ���� �ż���� �Դϴ� ========================

    public void PlayBGM(AudioClip clip) //���� ���� ��� ��ü ���� �ʱ� ���� �ż����Դϴ�.
    {
        if (audioSource.clip == clip) return;
        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void ChangeTohurryBGM() //���� �ð� ������ ��� ��µǴ� BGM
    {
        if (audioSource.clip != hurryBGM)
        {
            audioSource.clip = hurryBGM;
            audioSource.loop = true;
            audioSource.Play();
        }
        
    }

    public void GameClearBGM() //������ Ŭ���� ������ BGM
    {
        audioSource.clip = clearBGM;
        audioSource.loop = false;
        audioSource.Play();
    }

    public void FailedBGM() //�ð����� Ŭ��� �������� BGM
    {
        audioSource.clip = failBGM;
        audioSource.loop = false;
        audioSource.Play();
    }

    public void HiddenBGM() //���罺�������� �����ϴ� �������� Ŭ����� BGM
    {
        // �������� ������ �ʿ��ҰŰ���
        audioSource.clip = hiddenBGM;
        audioSource.loop = false;
        audioSource.Play();
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode) // ����ȭ��(StartScene)������ BGM�� (MainScene)������ BGM�� �ٲٱ� �����Դϴ�(����)
    {
        if (scene.name == "StartScene")
        {
            PlayBGM(StartBGM);
        }
        else if (scene.name == "MainScene")
        {
            PlayBGM(clip);
        }
    }

    // ���ϴ� ȿ���� ���� �ż���� �Դϴ�. ============================


    public void Cardmatched() // ī�� ��ġ�� ȿ����
    {
        audioSource.PlayOneShot(Matched);
    }

    public void CardMiss() // ī�� ����ġ�� ȿ����
    {
        audioSource.PlayOneShot(matchMiss);
    }

    public void ClickSFX()  // ���� ��ư�� ������ ��κ��� ��ư(�����������������) Ŭ�� ����� Ȱ���ϱ� ���� Click �̶� �̸����� �������ϴ�.
    {
        audioSource.PlayOneShot(buttonClickSFX);
    }

    public void Cardflip()  // ī�� ������ ȿ����
    {
        audioSource.PlayOneShot(cardFlip);
    }

}
