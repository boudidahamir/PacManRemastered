using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;
using Random = UnityEngine.Random;

public class Greencontroller : MonoBehaviour
{
    private Rigidbody rb;
    private Transform tr;
    private NavMeshAgent agent;
    public Material normalmat;
    public Material fearmat;
    public Material deathmat;
    private GameObject player;
    private PlayerControl playerControl;
    private string state;
    private float randomX;
    private float randomZ;
    private float statetimer;
    public bool death;
    private Collider col;
    private float deathtimer;
    private float wondertimer;
    private int randomdirection;
    public Transform[] positions;


    // Start is called before the first frame update
    void Start()
    {
        randomdirection = Random.Range(1, 5);
        wondertimer = 5f;
        statetimer = 20f;
        deathtimer = 20f;
        state = "wonder";
        player = GameObject.Find("Sphere(Clone)");
        death = false;
        col = GetComponent<Collider>();
        if (player != null)
        {
            tr = player.transform;
            playerControl = player.GetComponent<PlayerControl>();
        }


        rb = GetComponent<Rigidbody>();

        agent = GetComponent<NavMeshAgent>();
        randomdirection = Random.Range(0, positions.Length);
    }

    // Update is called once per frame
    void Update()
    {
        positions = GameObject.Find("Points").GetComponentsInChildren<Transform>();
        statetimer -= Time.deltaTime;
        if (statetimer < 0)
        {
            state = "chase";
        }
        if (player != null)
            tr = player.transform;

        if (!death)
        {
            if (playerControl != null && playerControl.poweractive == true)
            {
                agent.speed = 4f;
                gameObject.GetComponent<SkinnedMeshRenderer>().material = fearmat;
                Vector3 dest = new Vector3(0, 0, 0);
                float maxdist = Vector3.Distance(positions[0].position, player.GetComponent<Transform>().position);
                foreach (Transform t in positions)
                {
                    float distance = Vector3.Distance(t.position, player.GetComponent<Transform>().position);
                    if (distance > maxdist)
                    {
                        maxdist = distance;
                        dest = t.position;
                    }
                }
                agent.destination = dest;
            }

            if (playerControl != null && playerControl.poweractive == false)
            {
                agent.speed = 3.5f;
                agent.destination = positions[randomdirection].position;
                gameObject.GetComponent<SkinnedMeshRenderer>().material = normalmat;
                if (player != null && state == "chase")
                {
                    agent.speed = 5f;
                    agent.destination = tr.position;
                }
                else if (player != null && state == "wonder")
                {
                    wondertimer -= Time.deltaTime;
                    if (wondertimer < 0)
                    {
                        wondertimer = 5f;
                        agent.speed = 3.5f;
                        randomdirection = Random.Range(0, positions.Length);
                    }
                }
            }
        }

        if (death)
        {
            agent.speed = 10f;
            agent.destination = new Vector3(9.69f, 0, 8.26f);
            col.isTrigger = true;
            gameObject.GetComponent<SkinnedMeshRenderer>().material = deathmat;
            if (transform.position.x == 9.69f && transform.position.z == 8.26f)
            {
                deathtimer -= Time.deltaTime;

                if (deathtimer < 0)
                {
                    deathtimer = 20;
                    death = false;
                }
                col.isTrigger = false;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.name.Equals("Wall1"))
        {
            transform.position = new Vector3(7.5f, 0f, -19.6f);
        }
        else if (collision.gameObject.name.Equals("Wall2"))
        {
            transform.position = new Vector3(6.9f, 0f, 19.7f);
        }
        else if (collision.gameObject.name.Equals("Sphere(Clone)"))
        {
            if (playerControl.poweractive == true)
            {
                death = true;
            }else if (playerControl.poweractive == false)
            {
                Destroy(gameObject);
                Destroy(GameObject.Find("Red(Clone)"));
                if (GameObject.Find("Pink(Clone)"))
                    Destroy(GameObject.Find("Pink(Clone)"));
                if (GameObject.Find("Blue(Clone)"))
                    Destroy(GameObject.Find("Blue(Clone)"));
            }
        }
        else if (collision.gameObject.name.Equals("Red(Clone)"))
        {
            col.isTrigger = true;
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name.Equals("Red(Clone)"))
        {
            col.isTrigger = false;
        }
    }
}
