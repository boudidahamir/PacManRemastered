using System;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Coinspawner : MonoBehaviour
{
    public GameObject coinPrefab;
    public GameObject walls;
    public GameObject green;
    public GameObject red;
    public GameObject blue;
    public GameObject pink;
    public GameObject pwerup;
    public GameObject player;
    public GameObject cherry;
    private float timercherry;
    private Vector3 spawn;
    private int randomcherry;
    public bool started;
    private float starttimer;
    private bool isgame;

    void Start()
    {
        Time.timeScale = 1f;
        isgame= false;
        starttimer = 4f;
        started = true;   
        timercherry = Random.Range(10,30);
        Instantiate(walls, walls.transform.position,walls.transform.rotation );

        Instantiate(pwerup, new Vector3(-21.2f, 1.43f, 28.9f), Quaternion.identity);
        Instantiate(pwerup, new Vector3(33.3f, 1.43f, 28.9f), Quaternion.identity);
        Instantiate(pwerup, new Vector3(33.3f, 1.43f, -9.1f), Quaternion.identity);
        Instantiate(pwerup, new Vector3(-21.2f, 1.43f, -9.1f), Quaternion.identity);

        Instantiate(coinPrefab,coinPrefab.transform.position, Quaternion.identity);

        
    }
    private void Update()
    {
        if (started)
        {
            if (isgame)
            {
                if (!GameObject.Find("Red(Clone)"))
                {
                    Instantiate(red, new Vector3(5.8f, 0, 25.76f), Quaternion.identity);
                }

                if (!GameObject.Find("Green(Clone)"))
                {
                    Instantiate(green, new Vector3(0.7f, 0, 9f), Quaternion.identity);
                }
                if ((!GameObject.Find("Pink(Clone)") && SceneManager.GetActiveScene().buildIndex==2) || (!GameObject.Find("Pink(Clone)") && SceneManager.GetActiveScene().buildIndex == 3))
                {
                    Instantiate(pink, pink.transform.position , Quaternion.identity);
                }

                if (!GameObject.Find("Blue(Clone)") && SceneManager.GetActiveScene().buildIndex == 3)
                {
                    Instantiate(blue, blue.transform.position, Quaternion.identity);
                }

                if (!GameObject.Find("Sphere(Clone)"))
                {
                    Instantiate(player, player.transform.position, Quaternion.identity);
                }

                if (!GameObject.Find("Cherry(Clone)"))
                {
                    randomcherry = Random.Range(1, 5);
                    timercherry -= Time.deltaTime;
                    if (timercherry < 0)
                    {
                        timercherry = Random.Range(10, 30);
                        switch (randomcherry)
                        {
                            case 1:
                                spawn = new Vector3(6.4f, -0.4f, 25.3f);
                                break;
                            case 2:
                                spawn = new Vector3(5.6f, -0.4f, 2.15f);
                                break;
                            case 3:
                                spawn = new Vector3(20.8f, -0.4f, -20.6f);
                                break;
                            case 4:
                                spawn = new Vector3(-9.6f, -0.4f, -20.6f);
                                break;
                        }

                        Instantiate(cherry, spawn, cherry.transform.rotation);
                    }
                }
            }

            else if(!isgame)
            {
                GameObject.Find("Ready").GetComponent<Canvas>().enabled = true;
                starttimer -= Time.deltaTime;
                if (starttimer < 0)
                {
                    isgame = true;
                    GameObject.Find("Ready").GetComponent<Canvas>().enabled = false;
                    starttimer = 4f;
                    if (!GameObject.Find("Red(Clone)"))
                    {
                        Instantiate(red, new Vector3(5.8f, 0, 25.76f), Quaternion.identity);
                    }

                    if (!GameObject.Find("Green(Clone)"))
                    {
                        Instantiate(green, new Vector3(0.7f, 0, 9f), Quaternion.identity);
                    }

                    if (!GameObject.Find("Sphere(Clone)"))
                    {
                        Instantiate(player, player.transform.position, Quaternion.identity);
                    }

                    if (!GameObject.Find("Cherry(Clone)"))
                    {
                        randomcherry = Random.Range(1, 5);
                        timercherry -= Time.deltaTime;
                        if (timercherry < 0)
                        {
                            timercherry = Random.Range(10, 30);
                            switch (randomcherry)
                            {
                                case 1:
                                    spawn = new Vector3(6.4f, -0.4f, 25.3f);
                                    break;
                                case 2:
                                    spawn = new Vector3(5.6f, -0.4f, 2.15f);
                                    break;
                                case 3:
                                    spawn = new Vector3(20.8f, -0.4f, -20.6f);
                                    break;
                                case 4:
                                    spawn = new Vector3(-9.6f, -0.4f, -20.6f);
                                    break;
                            }

                            Instantiate(cherry, spawn, cherry.transform.rotation);
                        }
                    }
                }
            }
           
        }
    }
}
