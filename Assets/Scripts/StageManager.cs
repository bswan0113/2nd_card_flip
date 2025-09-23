using System;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class StageManager : MonoBehaviour
{
    public const int TotalImageCount = 8;


    [SerializeField] private Text selectedStageNum;
    [SerializeField] private StageData[] allStages;
    private StageData _currentStage;

    public StageData stage => _currentStage;

    void Awake()
    {
        if (allStages != null && allStages.Length > 0)
        {
            _currentStage = allStages[0];
        }
        else
        {
            _currentStage = new StageData();
        }
    }

    public void SetCurrentStage(int stageLevel, bool isHiddenStageActive)
    {
        _currentStage = allStages[stageLevel - 1];
        if (isHiddenStageActive)
        {
            selectedStageNum.text = "HIDDEN";
        }
        else
        {
            selectedStageNum.text = stageLevel.ToString();
        }
    }

       public int[] GenerateCardDeck(StageData stageData)
    {
        int totalCards = stageData.boardSize * stageData.boardSize;
        int cardPairs = totalCards / 2;

        List<int> chosenCardTypes = new List<int>();

        List<int> availableImageIndices = Enumerable.Range(0, TotalImageCount).ToList();

        availableImageIndices = availableImageIndices.OrderBy(x => Random.Range(0f, 1f)).ToList();

        int initialFillCount = Math.Min(cardPairs, TotalImageCount);
        for (int i = 0; i < initialFillCount; i++)
        {
            chosenCardTypes.Add(availableImageIndices[i]);
        }

        int remainingPairs = cardPairs - chosenCardTypes.Count;
        if (remainingPairs > 0)
        {
            availableImageIndices = availableImageIndices.OrderBy(x => Random.Range(0f, 1f)).ToList();

            for (int i = 0; i < remainingPairs; i++)
            {
                chosenCardTypes.Add(availableImageIndices[i % TotalImageCount]);
            }
        }

        List<int> finalCardDeck = new List<int>();
        foreach (int type in chosenCardTypes)
        {
            finalCardDeck.Add(type);
            finalCardDeck.Add(type);
        }
        return finalCardDeck.OrderBy(x => Random.Range(0f, 1f)).ToArray();
    }
}