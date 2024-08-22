using TMPro;
using UnityEngine;
public class lives : MonoBehaviour
{
    public Material matOn, matOff;
    private GameObject player;
    private TextMeshProUGUI textMesh;
    private Material life1;
    private Material life2;
    private Material life3;
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
        if (player != null)
        {
            playerControl = player.GetComponent<PlayerControl>();
            /*textMesh.SetText("Lives : " + playerControl.lives.ToString()) ;*/
            switch(playerControl.lives) 
            { 
                case 0:
                    GameObject.Find("SphereLife").GetComponent<SkinnedMeshRenderer>().material = matOff;
                    GameObject.Find("SphereLife1").GetComponent<SkinnedMeshRenderer>().material = matOff;
                    GameObject.Find("SphereLife2").GetComponent<SkinnedMeshRenderer>().material = matOff;
                    break;
                case 1:
                    GameObject.Find("SphereLife").GetComponent<SkinnedMeshRenderer>().material = matOn;
                    GameObject.Find("SphereLife1").GetComponent<SkinnedMeshRenderer>().material = matOff;
                    GameObject.Find("SphereLife2").GetComponent<SkinnedMeshRenderer>().material = matOff;
                    break;
                case 2:
                    GameObject.Find("SphereLife").GetComponent<SkinnedMeshRenderer>().material = matOn;
                    GameObject.Find("SphereLife1").GetComponent<SkinnedMeshRenderer>().material = matOn;
                    GameObject.Find("SphereLife2").GetComponent<SkinnedMeshRenderer>().material = matOff;
                    break;    
                case 3:
                    GameObject.Find("SphereLife").GetComponent<SkinnedMeshRenderer>().material = matOn;
                    GameObject.Find("SphereLife1").GetComponent<SkinnedMeshRenderer>().material = matOn;
                    GameObject.Find("SphereLife2").GetComponent<SkinnedMeshRenderer>().material = matOn;
                    break;
            }
        }
    }
}
