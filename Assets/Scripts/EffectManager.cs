using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectManager : MonoBehaviour
{
    public static EffectManager instance;
    public Camera mc;
    public Text startCountdown;

    public GameObject dust;

    public bool remain10Sec = false;
    public bool isTimerStart = false;

    // Update is called once per frame
    private void Awake()
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

    void Update()
    {

    }


    // Do not start same couroutine until stop 
    public IEnumerator ShakeCamera(float st)
    {
        Vector3 originPos = mc.transform.localPosition;
        float timeCount = 0f;

        while (timeCount < st)
        {
            float offsetX = Random.Range(-0.25f, 0.25f) * mc.orthographicSize * 0.1f;

            mc.transform.localPosition = new Vector3(originPos.x + offsetX, originPos.y, originPos.z);

            timeCount += Time.deltaTime;
            yield return null;
        }
        mc.transform.localPosition = originPos;
    }

    public IEnumerator DropDusts(float st)
    {
        float timeCount = 0f;

        while (timeCount < st)
        {
            //Vector3 ranPos = new Vector3(Random.Range(-2.6f, +2.6f), 5.0f, 1.5f);
            float x = Screen.width * Random.Range(0f, 1f);
            float y = Screen.height;
            Vector3 ranPos = mc.ScreenToWorldPoint(new Vector3(x, y, 1f));

            GameObject go = Instantiate(dust, this.transform);
            go.transform.position = ranPos;
            go.transform.localScale *= Random.Range(0.2f, 0.4f) * mc.orthographicSize * 0.5f;
            timeCount += 0.05f;
            yield return new WaitForSeconds(0.05f);
        }

    }

    // move all cards to right positions
    public IEnumerator PositionCards()
    {
        GameObject[] cards = GameObject.FindGameObjectsWithTag("Card");
        for (int i = 0; i < cards.Length; i++)
        {
            float x = (i % Mathf.Sqrt(cards.Length)) * 1.4f - 2.1f;
            float y = (int)(i / Mathf.Sqrt(cards.Length)) * 1.4f - 3.0f;
            Vector3 tp = new Vector3(x, y, 0);
            //cards[i].transform.position = new Vector3(x, y, 0f);
            StartCoroutine(MoveCard(cards[i], tp));
        }
        yield return null;
    }

    // move a card to right position
    public IEnumerator MoveCard(GameObject go, Vector3 pos)
    {
        yield return new WaitForSeconds(0.1f);
        float t = 0;
        while (t < 0.5f)
        {
            t += Time.deltaTime;
            go.transform.localPosition = Vector3.Lerp(go.transform.localPosition, pos, t);
            yield return null;
        }
    }

    public IEnumerator GameStartCountdown(float sec)
    {
        while (sec > 0)
        {
            sec -= Time.deltaTime;
            startCountdown.text = ((int)sec + 1).ToString();

            if (sec < 0.5f)
            {
                startCountdown.text = "Start";
            }
            yield return null;
        }
        startCountdown.text = "";
        GameManager.instance.GameStart();
    }

    public void GameStartRoutine()
    {
        StartCoroutine(GameStartCountdown(3.0f));
        GameManager.instance.selectStageContainer.SetActive(false);
        Time.timeScale = 1.0f;
    } 
}
