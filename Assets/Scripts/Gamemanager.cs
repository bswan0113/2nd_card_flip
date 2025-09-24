using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private StageManager stageManager;
    [SerializeField] private BoardManager boardManager;

    [SerializeField] private Text timeTxT;
    [SerializeField] private GameObject endTxt;

    [SerializeField] public GameObject selectStageContainer;

    [SerializeField] private Button startBtn;

    public Card firstCard;
    public Card secondCard;
    [SerializeField] private int totalCardCount;

    private float currentTime = 0.0f;
    private float timeLimit;

    private bool isGameStarted = false;
    private int stageLevel = 1;
    private const int MaximumStageLevel = 3;

    private bool isHiddenStageActive = false;
    private int playerBestScore = 0;
    private KeyCode[] secretCommand = {
        KeyCode.UpArrow,
        KeyCode.DownArrow,
        KeyCode.UpArrow,
        KeyCode.DownArrow
    };
    private const float HiddenStageTimeDecrease = 30f;
    private const float HiddenStageMinimumTimeLimit = 30f;

    private int commandIndex = 0;
    public float commandTimeout = 2.0f;
    private float timeOfLastInput;
    private int hiddenStageClearCount = 0;

    AudioSource audioSource;
    public AudioClip matchClip;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        selectStageContainer.SetActive(true);
        // PlayerPrefs.SetInt("playerBestScore", 3);
        // isHiddenStageActive = true;
        playerBestScore = PlayerPrefs.GetInt("playerBestScore");
    }

    void Update()
    {
        CheckToOpenHiddenStage();
        if (!isGameStarted) return;

        currentTime += Time.deltaTime;

        if (currentTime < stageManager.stage.timeLimit)
        {
            
            timeTxT.text = (stageManager.stage.timeLimit - currentTime).ToString("N2");

            if (stageManager.stage.timeLimit - currentTime <= 10.0f && !EffectManager.instance.remain10Sec)
            {
                AudioManager.instance.ChangeTohurryBGM();
                StartCoroutine(EffectManager.instance.ShakeCamera(0.5f));
                StartCoroutine(EffectManager.instance.DropDusts(0.5f));
                EffectManager.instance.remain10Sec = true;
            }
        }
        else
        {
            timeTxT.text = "0.00";
            GameOver();
        }


    }

    public void GameStart()
    {
        selectStageContainer.SetActive(false);
        boardManager.ClearBoard();

        StageData currentStage = stageManager.stage;

        if (isHiddenStageActive)
        {
            float limit = currentStage.timeLimit;
            if (limit > HiddenStageMinimumTimeLimit)
            {
                currentStage.timeLimit -= HiddenStageTimeDecrease * hiddenStageClearCount;
            }

        }

        int[] cardDeck = stageManager.GenerateCardDeck(currentStage);
        totalCardCount = cardDeck.Length;
        boardManager.CreateBoard(cardDeck, currentStage.boardSize);

        currentTime = 0.0f;
        Time.timeScale = 1.0f;
        endTxt.SetActive(false);
        isGameStarted = true;
        EffectManager.instance.remain10Sec = false;
    }

    public void MatchCards()
    {

        if (firstCard.idx == secondCard.idx)
        {
            // audioSource.PlayOneShot(matchClip);
            firstCard.DestroyCard();
            secondCard.DestroyCard();
            totalCardCount -= 2;

            if (totalCardCount == 0)
            {
                GameClear();
            }
        }
        else
        {
            firstCard.CloseCard();
            secondCard.CloseCard();
        }
        firstCard = null;
        secondCard = null;
    }

    private void GameClear()
    {
        isGameStarted = false;
        endTxt.SetActive(true);
        Time.timeScale = 0.0f;
        if (isHiddenStageActive && stageLevel == MaximumStageLevel)
        {
            hiddenStageClearCount++;
        }
        CheckBestScore();
    }


    private void GameOver()
    {
        isGameStarted = false;
        Time.timeScale = 0.0f;
        endTxt.SetActive(true);
    }

    public void SelectStage(bool isNextStage)
    {
        if (isNextStage)
        {
            if (stageLevel + 1 > MaximumStageLevel || stageLevel > playerBestScore) return;

            stageLevel++;

            if (isHiddenStageActive)
            {
                stageLevel = MaximumStageLevel;
            }
        }
        else
        {
            if(stageLevel-1 <= 0) return;
            if (isHiddenStageActive)
            {
                isHiddenStageActive = false;
            }
            else
            {
                stageLevel--;
            }
        }
        stageManager.SetCurrentStage(stageLevel, isHiddenStageActive);
    }

    public void CheckBestScore()
    {
        if (stageLevel > playerBestScore)
        {
            playerBestScore = stageLevel;
            PlayerPrefs.SetInt("playerBestScore", playerBestScore);
        }
    }

    public void CheckToOpenHiddenStage()
    {
        if (playerBestScore < MaximumStageLevel) return;
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(secretCommand[commandIndex]))
            {
                commandIndex++;
                timeOfLastInput = Time.time;
            }
            else
            {
                commandIndex = 0;
            }
        }

        if (commandIndex == secretCommand.Length)
        {
            OpenHiddenStage();
            commandIndex = 0;
        }

        if (commandIndex > 0 && Time.time - timeOfLastInput > commandTimeout)
        {
            commandIndex = 0;
        }
    }

    void OpenHiddenStage()
    {
        isHiddenStageActive = true;
        stageLevel = MaximumStageLevel;
        stageManager.SetCurrentStage(stageLevel, isHiddenStageActive);
    }
}