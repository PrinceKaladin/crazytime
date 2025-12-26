using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelecting : MonoBehaviour
{
    public Text bestscore;
    public Button playbutton;
    public GameObject SELECTING;
    public GameObject GAMEPLAY;
    public GameObject TIMEUP;
    public void selectlevel(int level) {
    SceneManager.LoadScene(level);
    }
    private void Start()
    {
        if (!PlayerPrefs.HasKey("sound")) {
            PlayerPrefs.SetInt("sound", 1);
        }
        if (PlayerPrefs.HasKey("bestscore"))
        {
            if(bestscore)
            bestscore.text = PlayerPrefs.GetInt("bestscore").ToString();
        }
        else {
            PlayerPrefs.SetInt("bestscore",0);
        }
       
    }
    private void Update()
    {
        if (playbutton)
        {
            if (PlayerPrefs.GetInt("time") == 0 ||
                (PlayerPrefs.GetInt("color") == 0 &&
                PlayerPrefs.GetInt("size") == 0 &&
                PlayerPrefs.GetInt("speed") == 0 &&
                PlayerPrefs.GetInt("logic") == 0
                ))
            {
                playbutton.interactable = false;

            }
            else
            {
                playbutton.interactable = true;
            }
        }
    }
    public void startgame() { 
    GAMEPLAY.SetActive(true);
    SELECTING.SetActive(false);

    }
}
