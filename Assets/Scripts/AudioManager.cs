using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    AudioSource audioSource;
   
   //BGM 변수들입니다.
    public AudioClip clip; //기본 BGM, 꼬일까봐 기본 BGM의 변수이름은 디폴트였던 clip으로 해놨습니다.
    public AudioClip hurryBGM; // 제한시간 얼마 안남았을때 BGM
    public AudioClip clearBGM; // 스테이지 클리어 BGM
    public AudioClip failBGM; // 스테이지 실패 BGM
    public AudioClip hiddenBGM; // 히든 스테이지 진입 BGM
    public AudioClip StartBGM; // (StartScene) 전용 BGM


    //효과음 변수들입니다.
    public AudioClip matchMiss; //카드 틀림
    public AudioClip Matched; //카드 맞춤
    public AudioClip cardFlip; //카드 뒤집기 효과음
    public AudioClip buttonClickSFX; //버튼 클릭 효과음 (게임 시작 버튼에도 응용가능한 효과음)

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
    // 이하는 BGM 관련 매서드들 입니다 ========================

    public void PlayBGM(AudioClip clip) //같은 곡일 경우 교체 하지 않기 위한 매서드입니다.
    {
        if (audioSource.clip == clip) return;
        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void ChangeTohurryBGM() //일정 시간 이하일 경우 출력되는 BGM
    {
        if (audioSource.clip != hurryBGM)
        {
            audioSource.clip = hurryBGM;
            audioSource.loop = true;
            audioSource.Play();
        }
        
    }

    public void GameClearBGM() //게임이 클리어 됐을때 BGM
    {
        audioSource.clip = clearBGM;
        audioSource.loop = false;
        audioSource.Play();
    }

    public void FailedBGM() //시간내로 클리어를 못했을때 BGM
    {
        audioSource.clip = failBGM;
        audioSource.loop = false;
        audioSource.Play();
    }

    public void HiddenBGM() //히든스테이지로 돌입하는 스테이지 클리어시 BGM
    {
        // 스테이지 로직이 필요할거같음
        audioSource.clip = hiddenBGM;
        audioSource.loop = false;
        audioSource.Play();
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode) // 시작화면(StartScene)에서의 BGM과 (MainScene)에서의 BGM을 바꾸기 로직입니다(실험)
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

    // 이하는 효과음 관련 매서드들 입니다. ============================


    public void Cardmatched() // 카드 일치시 효과음
    {
        audioSource.PlayOneShot(Matched);
    }

    public void CardMiss() // 카드 불일치시 효과음
    {
        audioSource.PlayOneShot(matchMiss);
    }

    public void ClickSFX()  // 시작 버튼을 포함한 대부분의 버튼(다음스테이지라던가) 클릭 사운드로 활용하기 좋게 Click 이란 이름으로 지었습니다.
    {
        audioSource.PlayOneShot(buttonClickSFX);
    }

    public void Cardflip()  // 카드 뒤집기 효과음
    {
        audioSource.PlayOneShot(cardFlip);
    }

}
