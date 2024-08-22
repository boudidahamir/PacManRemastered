using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CherryController : MonoBehaviour
{
    float timer;

    void Start()
    {
        GameObject.Find("IndicCherry").GetComponent<Renderer>().enabled = true;
        timer = 10f;  
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0 )
        {
            GameObject.Find("IndicCherry").GetComponent<Renderer>().enabled = false;
            Destroy( gameObject );
        }
    }
}
