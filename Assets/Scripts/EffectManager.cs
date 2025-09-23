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
    // Update is called once per frame

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

    void Update()
    {
        if (Input.GetKeyDown("c"))
        {
            StartCoroutine(ShakeCamera(0.5f));
            StartCoroutine(DropDusts(0.5f));
        }
    }


    // Do not start same couroutine until stop 
    public IEnumerator ShakeCamera(float st)
    {
        Vector3 originPos = mc.transform.localPosition;
        float timeCount = 0f;

        while (timeCount < st)
        {
            float offsetX = Random.Range(-0.25f, 0.25f);

            mc.transform.localPosition = new Vector3(originPos.x + offsetX, originPos.y, originPos.z);

            timeCount += Time.deltaTime;
            yield return null;
        }
        mc.transform.localPosition = originPos;
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
    }

    public IEnumerator DropDusts(float st)
    {
        float timeCount = 0f;

        while (timeCount < st)
        {
            Vector3 ranPos = new Vector3(Random.Range(-2.6f, +2.6f), 5.0f, 0f);
            GameObject go = Instantiate(dust, this.transform);
            go.transform.position = ranPos;
            go.transform.localScale *= Random.Range(0.5f, 1.0f);
            timeCount += 0.05f;
            yield return new WaitForSeconds(0.05f);
        }

    }
}
