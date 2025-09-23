using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    AudioSource audioSource;
   
   //BGM �������Դϴ�.
    public AudioClip clip; //�⺻ BGM
    public AudioClip hurryBGM; // ���ѽð� �� �ȳ������� BGM
    public AudioClip clearBGM; // �������� Ŭ���� BGM
    public AudioClip failBGM; // �������� ���� BGM
    public AudioClip hiddenBGM; // ���� �������� ���� BGM


    //ȿ���� �������Դϴ�.
    public AudioClip matchMiss; //ī�� Ʋ��
    public AudioClip Matched; //ī�� ����
    public AudioClip cardFlip; //ī�� ������ ȿ����
    public AudioClip GameStartSFX; //���� ���� ȿ����

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

        audioSource.clip = this.clip; //���ϱ�� �����̸��� ����Ʈ���� clip���� �س����ϴ�.
        audioSource.loop = true;
        audioSource.Play();
    }
    // ���ϴ� BGM ���� �ż���� �Դϴ� ========================
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

    public void StartsceneBGM() // ����ȭ�鿡���� BGM
    { 
        
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

    public void StartSFX()  // ���� ��ư ȿ����
    {
        audioSource.PlayOneShot(GameStartSFX);
    }

    public void Cardflip()  // ī�� ������ ȿ����
    {
        audioSource.PlayOneShot(cardFlip);
    }

}
