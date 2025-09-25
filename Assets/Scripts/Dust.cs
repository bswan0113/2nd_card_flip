using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dust : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.y < -30f)
        {
            Destroy(gameObject);
        }
    }
}
