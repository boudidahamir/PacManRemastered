using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    float x, z;
    public TMP_InputField input;
    private Canvas canvas;
    private AudioSource clicksound;
    private void Start()
    {
        clicksound = GameObject.Find("clicksound").GetComponent<AudioSource>();
        x =5f; 
        z=0;
        canvas = GetComponent<Canvas>();
        if (canvas.name.Equals("CanvasPause") )
        {
            canvas.enabled = false;
        }
        if (canvas.name.Equals("CanvasWinLose"))
        {
            canvas.enabled = false;
        }
        if (canvas.name.Equals("LoadGameCanvas"))
        {
            canvas.enabled = false;
        }
        if (canvas.name.Equals("Highscore"))
        {
            canvas.enabled = false;
        }
        if (PlayerPrefs.GetInt("stage")==1)
        {
            GameObject.Find("Continue").SetActive(false);
        }

        if(GameObject.Find("Hs"))
            GameObject.Find("Hs").GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString("savename") +" "+ PlayerPrefs.GetInt("score");
    }

    private void Update()
    {
        if (canvas.name.Equals("CanvasWinLose"))
        {
            GameObject.Find("Hs").GetComponent<TextMeshProUGUI>().text=PlayerPrefs.GetString("lastprofile") +" "+ PlayerPrefs.GetInt("lastscore");
        }
    }

    public void ToggleGame()
    {
        clicksound.Play();
        string text = input.text;
        Debug.Log(text.Equals(""));
        if (text.Equals(""))
        {
            
            GameObject.Find("errormsg").GetComponent<TextMeshProUGUI>().text = "please enter a name!";
        }
        else
        {
            PlayerPrefs.SetInt("stage", SceneManager.GetActiveScene().buildIndex);
            PlayerPrefs.SetString("savename", GameObject.Find("TextName").GetComponent<TextMeshProUGUI>().text);
            PlayerPrefs.SetInt("score", 0);
            SceneManager.LoadScene("game");
        }

    }

    public void gostart()
    {
        GameObject.Find("Canvas").GetComponent<Canvas>().enabled = false;
        GameObject.Find("PutName").GetComponent<Canvas>().enabled = true;
    }
    public void MainMenu()
    {
        clicksound.Play();
        SceneManager.LoadScene("StartMenu");
    }
    public void Quit()
    {
        clicksound.Play();
        Application.Quit();
    }
    public void nextStage()
    {
        clicksound.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PauseGame()
    {
        clicksound.Play();
        GameObject.Find("pauseGame").GetComponent<Image>().enabled = false;
        GameObject.Find("CanvasPause").GetComponent<Canvas>().enabled = true;
        Time.timeScale = 0f;
    }
    public void ResumeGame()
    {
        clicksound.Play();
        GameObject.Find("pauseGame").GetComponent<Image>().enabled=true;
        GameObject.Find("CanvasPause").GetComponent<Canvas>().enabled = false;
        Time.timeScale = 1f;
    }

    public void LoadGame()
    {
        clicksound.Play();
        SceneManager.LoadScene(PlayerPrefs.GetInt("stage"));
    }

    public void Games()
    {
        clicksound.Play();
        GameObject.Find("LoadGameCanvas").GetComponent<Canvas>().enabled= true;
        GameObject.Find("Canvas").GetComponent<Canvas>().enabled = false;
    }

    public void ReturnfromLoad()
    {
        clicksound.Play();
        GameObject.Find("LoadGameCanvas").GetComponent<Canvas>().enabled = false;
        GameObject.Find("Canvas").GetComponent<Canvas>().enabled = true;
    }

    public void ReturnfromHS()
    {
        clicksound.Play();
        GameObject.Find("Highscore").GetComponent<Canvas>().enabled = false;
        GameObject.Find("Canvas").GetComponent<Canvas>().enabled = true;
    }
    public void ReturnfromStart()
    {
        clicksound.Play();
        GameObject.Find("PutName").GetComponent<Canvas>().enabled = false;
        GameObject.Find("Canvas").GetComponent<Canvas>().enabled = true;
    }

    public void SaveHS()
    {
        clicksound.Play();
        PlayerPrefs.SetInt("score",GameObject.Find("Sphere(Clone)").GetComponent<PlayerControl>().score+PlayerPrefs.GetInt("score"));
        PlayerPrefs.Save();
        PlayerPrefs.SetString("lastprofile", PlayerPrefs.GetString("savename"));
        PlayerPrefs.SetInt("lastscore", PlayerPrefs.GetInt("score"));
        GameObject.Find("savehighscore").GetComponent<Image>().enabled = false;
        GameObject.Find("savehighscore").GetComponent<Button>().enabled = false;
        GameObject.Find("savehstext").GetComponent<TextMeshProUGUI>().enabled = false;
    }

    public void SaveGame()
    {
        clicksound.Play();
        PlayerPrefs.SetInt("stage", SceneManager.GetActiveScene().buildIndex+1);
        PlayerPrefs.Save();
        GameObject.Find("saveGameButton").GetComponent<Image>().enabled = false;
        GameObject.Find("saveGameButton").GetComponent<Button>().enabled = false;
        GameObject.Find("savegametext").GetComponent<TextMeshProUGUI>().enabled = false;
    }

    public void Highscore()
    {
        clicksound.Play();
        GameObject.Find("Highscore").GetComponent<Canvas>().enabled = true;
        GameObject.Find("Canvas").GetComponent<Canvas>().enabled = false;

    }

    public void Right()
    {
        x = 5f;
        z = 0;
    }
    public void Left()
    {
        x = -5f;
        z = 0;
    }
    public void Up()
    {
        z = 5f;
        x = 0;
    }
    public void Down()
    {
        z = -5f;
        x = 0;
    }
}
