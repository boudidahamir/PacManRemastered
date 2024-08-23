using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerControl : MonoBehaviour
{
    public LayerMask mask;
    private float radius;
    RaycastHit raycast;
    public float castdistance;
    Vector3 directionWanted;

    private Rigidbody rb;
    private Collider col;
    public Vector3 h;
    public Vector3 v;
    [SerializeField]
    public int score;
    [SerializeField]
    public int lives;
    [SerializeField]
    public bool poweractive;
    private float countdownTime;
    private float movementqueue;
    private bool startqueue;

    void Start()
    {
        movementqueue = 0.5f;
        directionWanted = Vector3.right;
        startqueue = false;
        radius = 1.6f;
        Time.timeScale = 1f;
        h.x = 5f; ; v.z = 0f; 
        transform.position = new Vector3(5.6f, 1.39f, -8.91f);
        transform.rotation = Quaternion.Euler(0, 90, 0);
        rb = GetComponent<Rigidbody>();  
        col = GetComponent<Collider>();
        poweractive = false;
        countdownTime = 10.0f;
    }
    void Update()
    {
        if (rb != null)
        {
            rb.velocity = (new Vector3(h.x, 0, v.z)).normalized*5f; 
            col.isTrigger = false;
            
            if(startqueue)
            {
                movementqueue -= Time.deltaTime;
            }
            
            
            if(poweractive==true)
            {
                if (countdownTime > 0)
                {
                    countdownTime -= Time.deltaTime;
                    GameObject.Find("PwrUpCd").GetComponent<TextMeshProUGUI>().text = ((int)countdownTime).ToString("D") + "s";

                    GameObject.Find("PwrUpCd (1)").GetComponent<TextMeshProUGUI>().text ="Power Up:";
                }
                else
                {
                    GameObject.Find("pacmanchaseaudio").GetComponent<AudioSource>().Stop();
                    poweractive = false;
                    GameObject.Find("PwrUpCd").GetComponent<TextMeshProUGUI>().text = "";
                    GameObject.Find("PwrUpCd (1)").GetComponent<TextMeshProUGUI>().text = "";
                    countdownTime = 10.0f;
                }
            }

            if (Input.GetButtonDown("Horizontal"))
            {
                startqueue = true;
                if (Input.GetAxis("Horizontal") < 0)
                {
                    directionWanted = Vector3.left;
                }
                else
                {
                    directionWanted = Vector3.right;
                }

                if(cast()==false)
                {
                    if (directionWanted.x < 0)
                    {
                        transform.rotation = Quaternion.Euler(0, -90, 0);
                        h.x = -5f;
                    }
                    else
                    {
                        transform.rotation = Quaternion.Euler(0, 90, 0);
                        h.x = 5f;
                    }
                    v.z = 0f;
                }
            }
            if (Input.GetButtonDown("Vertical"))
            {
                startqueue = true;
                if (Input.GetAxis("Vertical") < 0)
                {
                    directionWanted = Vector3.back;
                }
                else
                {
                    directionWanted = Vector3.forward;
                }

                if (cast() == false)
                {
                    if (directionWanted.z < 0)
                    {
                        transform.rotation = Quaternion.Euler(0, 180, 0);
                        v.z = -5f;
                    }
                    else
                    {
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                        v.z = 5f;
                    }
                    h.x = 0f;
                }
                

            }
            if (movementqueue > 0)
            {
                if(!cast())
                {
                    if (directionWanted.z < 0)
                    {
                        transform.rotation = Quaternion.Euler(0, 180, 0);
                        v.z = -5f;
                        h.x = 0f;
                    }
                    else if (directionWanted.z > 0)
                    {
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                        v.z = 5f;
                        h.x = 0f;
                    }
                    if (directionWanted.x < 0)
                    {
                        transform.rotation = Quaternion.Euler(0, -90, 0);
                        h.x = -5f;
                        v.z = 0f;
                    }
                    else if (directionWanted.x > 0)
                    {
                        transform.rotation = Quaternion.Euler(0, 90, 0);
                        h.x = 5f;
                        v.z = 0f;
                    }
                    startqueue = false;
                    movementqueue = 0.5f ;
                }
            }else if (movementqueue < 0)
            {
                startqueue = false;
                movementqueue = 0.5f;
            }

            GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

            bool foundCoin = false;

            foreach (GameObject obj in allObjects)
            {
                if (obj.name.StartsWith("Coin") || obj.name.Equals("PowerUp(Clone)"))
                {
                    foundCoin = true;
                    break;
                }
            }

            if (foundCoin==false)
            {
                if (GameObject.Find("Sphere(Clone)") != null && GameObject.Find("Green(Clone)") != null && GameObject.Find("Red(Clone)") != null)
                {
                    Time.timeScale = 0f;
                    Destroy(GameObject.Find("spawner"));
                    Destroy(GameObject.Find("Green(Clone)"));
                    Destroy(GameObject.Find("Red(Clone)"));
                    if (GameObject.Find("Pink(Clone)"))
                        Destroy(GameObject.Find("Pink(Clone)"));
                    if (GameObject.Find("Blue(Clone)"))
                        Destroy(GameObject.Find("Blue(Clone)"));
                }
                GameObject.Find("WinLose").GetComponent<TextMeshProUGUI>().text = "YOU WIN!!!";
                GameObject.Find("CanvasWinLose").GetComponent<Canvas>().enabled = true;
                if(GameObject.Find("SaveGame"))
                {
                    GameObject.Find("SaveGame").GetComponent<Canvas>().enabled = true;
                }
                    
                if(GameObject.Find("pauseGame"))
                {
                    GameObject.Find("pauseGame").SetActive(false);
                }
                
                
            }

            if (lives==0)
            {
                if (GameObject.Find("Sphere(Clone)") != null && GameObject.Find("Green(Clone)") != null && GameObject.Find("Red(Clone)") != null)
                {
                    Destroy(GameObject.Find("spawner"));
                    Destroy(GameObject.Find("Green(Clone)"));
                    Destroy(GameObject.Find("Red(Clone)"));
                    if (GameObject.Find("Pink(Clone)"))
                        Destroy(GameObject.Find("Pink(Clone)"));
                    if (GameObject.Find("Blue(Clone)"))
                        Destroy(GameObject.Find("Blue(Clone)"));

                }

                GameObject.Find("WinLose").GetComponent<TextMeshProUGUI>().text = "GAME OVER!!!";
                GameObject.Find("CanvasWinLose").GetComponent<Canvas>().enabled = true;
                GameObject.Find("SaveGame").GetComponent<Canvas>().enabled = false;
                if (GameObject.Find("pauseGame"))
                {
                    GameObject.Find("pauseGame").SetActive(false);
                }
                Time.timeScale = 0f;
            }
        }
    } 

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.name.Equals("Wall1"))
        {
            transform.position = new Vector3(-25.15f, 0f, 8.27f);
            rb.velocity = new Vector3(h.x, 0, v.y);
        }
        else if (collision.gameObject.name.Equals("Wall2"))
        {
            transform.position = new Vector3(36.18f, 0, 8.27f);
            rb.velocity = new Vector3(h.x, 0, v.y);
        }

        if (collision.gameObject.name.Equals("Green(Clone)"))
        {
            if (!collision.gameObject.GetComponent<GreenController>().death)
            {
                if (poweractive == false)
                {
                    GameObject.Find("deathaudio").GetComponent<AudioSource>().Play();
                    lives -= 1;
                    GameObject.Find("spawner").GetComponent<Coinspawner>().started=true;
                    Start();
                }
                else if (poweractive == true)
                {
                    GameObject.Find("eatghost").GetComponent<AudioSource>().Play();
                    score += 100;
                }
            }
        }

        if (collision.gameObject.name.Equals("Red(Clone)"))
        {
            if (!collision.gameObject.GetComponent<Redcontroller>().death)
            {
                if (poweractive == false)
                {
                    GameObject.Find("deathaudio").GetComponent<AudioSource>().Play();
                    lives -= 1;
                    Start();
                }
                else if (poweractive == true)
                {
                    GameObject.Find("eatghost").GetComponent<AudioSource>().Play();
                    score += 100;
                }
            }

        }
        if (collision.gameObject.name.Equals("Pink(Clone)"))
        {
            if (!collision.gameObject.GetComponent<Redcontroller>().death)
            {
                if (poweractive == false)
                {
                    GameObject.Find("deathaudio").GetComponent<AudioSource>().Play();
                    lives -= 1;
                    Start();
                }
                else if (poweractive == true)
                {
                    GameObject.Find("eatghost").GetComponent<AudioSource>().Play();
                    score += 100;
                }
            }

        }
        if (collision.gameObject.name.Equals("Blue(Clone)"))
        {
            if (!collision.gameObject.GetComponent<Redcontroller>().death)
            {
                if (poweractive == false)
                {
                    GameObject.Find("deathaudio").GetComponent<AudioSource>().Play();
                    lives -= 1;
                    Start();
                }
                else if (poweractive == true)
                {
                    GameObject.Find("eatghost").GetComponent<AudioSource>().Play();
                    score += 100;
                }
            }

        }

        if (collision.gameObject.name.StartsWith("Coin"))
        {
            GameObject.Find("eatcoin").GetComponent<AudioSource>().Play();
            score += 1;
        }

        if (collision.gameObject.name.StartsWith("PowerUp"))
        {
            GameObject.Find("eatcoin").GetComponent<AudioSource>().Play();
            GameObject.Find("pacmanchaseaudio").GetComponent<AudioSource>().Play();
            poweractive = true;
            score += 50;
        }

        if (collision.gameObject.name.StartsWith("Cherry(Clone)"))
        {
            GameObject.Find("eatcherry").GetComponent<AudioSource>().Play();
            score += 200;
            Destroy(GameObject.Find("Cherry(Clone)"));
        }
    }

    bool cast()
    {
        if (Physics.SphereCast(transform.position, radius, directionWanted, out raycast,castdistance,mask))
        {
            return true; 
        }
        
        return false;
    }

/*    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position+directionWanted*castdistance, radius);
    }*/
}
