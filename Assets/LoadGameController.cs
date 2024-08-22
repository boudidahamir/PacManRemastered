using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadGameController : MonoBehaviour
{
    private Canvas loadcanvas;
    List<int> stages;
    List<string> savesNames;

    // Start is called before the first frame update
    void Start()
    {
        loadcanvas = GetComponent<Canvas>();
        if(PlayerPrefs.GetInt("stage")>0)
        {
            GameObject.Find("title").GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString("savename") + " " + PlayerPrefs.GetInt("stage").ToString();
            GameObject.Find("Load").GetComponent<Button>().interactable = true;
            GameObject.Find("Load").GetComponentInChildren<MenuButton>().enabled = true;
        }
    }
           

    // Update is called once per frame
    void Update()
    {

    }
}
