using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class score : MonoBehaviour
{
    private GameObject player;
    private GameObject go;
    private TextMeshProUGUI textMesh;
    private PlayerControl playerControl;
    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("Sphere(Clone)");
        if (player != null )
        {
            playerControl = player.GetComponent<PlayerControl>();
            textMesh.text = playerControl.score.ToString();
        }
        

    }
}
