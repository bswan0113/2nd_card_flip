using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class RetryButton : MonoBehaviour
{
    public bool isInit = true;
    public void Retry()
    {
        try
        {
            if (isInit)
            {
                SceneManager.LoadScene("MainScene");
                isInit = false;
            }
            else
            {
                GameManager gameManager = GameManager.instance;
                UIManager uiManager = UIManager.instance;
                AudioManager.instance.ClickSFX();
                AudioManager.instance.NomalBGM();
                gameManager.selectStageContainer.gameObject.SetActive(true);
                gameManager.hiddenBackground.SetActive(false);
                gameManager.endTxt.SetActive(false);
                gameManager.timeTxT.gameObject.SetActive(true);
                uiManager.UnlockImage.SetActive(false);
                DeleteAllCards();
            }

        }
        catch (Exception e)
        {
            Debug.Log(e);
        }


    }

    public void DeleteAllCards()
    {
        GameObject[] cards = GameObject.FindGameObjectsWithTag("Card");
        foreach (GameObject go in cards)
        {
            Destroy(go);
        }
    }

}
