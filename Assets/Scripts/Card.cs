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
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
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

        if (Gamemanager.instance.firstcard == null)
        {
            Gamemanager.instance.firstcard = this;
        }

        else
        { 
            Gamemanager.instance.secondcard = this;
            Gamemanager.instance.Matched();
        }
    }

    public void DestoryCard() 
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
