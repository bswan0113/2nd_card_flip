using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
     public int idx = 0;

    public GameObject front;
    public GameObject back;

    public Animator anim;

    AudioSource audioSource;
    public AudioClip clip;

    public SpriteRenderer frontImage;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Setting(int number)
    {
        idx = number;
        frontImage.sprite = Resources.Load<Sprite>($"rtan{idx}");
    }

    public void OpenCard() 
    {
        audioSource.PlayOneShot(clip);
        anim.SetBool("isOpen", true);
        front.SetActive(true);
        //back.SetActive(false);

        if (GameManager.instance.firstCard == null)
        {
            GameManager.instance.firstCard = this;
        }

        else
        { 
            GameManager.instance.secondCard = this;
            GameManager.instance.MatchCards();
        }
    }

    public void DestroyCard()
    {
        Invoke("DestroyCardInvoke", 1.0f);
    }

    void DestroyCardInvoke() 
    {
        Destroy(gameObject);
    }
    public void CloseCard() 
    {
        Invoke("CloseCardInvoke", 1.0f);
    }

    void CloseCardInvoke()
    {
        anim.SetBool("isOpen", false);
        front.SetActive(false);
        back.SetActive(true);
        back.transform.localScale = new Vector3(1f, 1f, 1f);
    }
}
