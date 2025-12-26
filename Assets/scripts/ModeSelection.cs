using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ModeSelection : MonoBehaviour
{
    public Sprite on;
    public Sprite off;
    public string key;
    public GameObject s1;
    public GameObject s2;
    public GameObject s3;
    public int timekey;
    private void Start()
    {

        PlayerPrefs.SetInt(key, 0);
        PlayerPrefs.SetInt("time", 0);
    }
    public void onoffmods() {
        if (this.GetComponent<Image>().sprite == off)
        {
            this.GetComponent<Image>().sprite = on;
            PlayerPrefs.SetInt(key, 1);
        }
        else { 
            this.GetComponent<Image>().sprite = off;
            PlayerPrefs.SetInt(key, 0);
        }
    }
    void cancelsound() {
        if (PlayerPrefs.GetInt("time") == 30) {
            this.GetComponent<Image>().sprite = on;
            s2.gameObject.GetComponent<Image>().sprite = s2.gameObject.GetComponent<ModeSelection>().off;
            s3.gameObject.GetComponent<Image>().sprite = s3.gameObject.GetComponent<ModeSelection>().off;
        }
        if (PlayerPrefs.GetInt("time") == 60)
        {
            this.GetComponent<Image>().sprite = on;


            s1.gameObject.GetComponent<Image>().sprite = s1.gameObject.GetComponent<ModeSelection>().off;
            s3.gameObject.GetComponent<Image>().sprite = s3.gameObject.GetComponent<ModeSelection>().off;
        }
        if (PlayerPrefs.GetInt("time") == 120)
        {
            this.GetComponent<Image>().sprite = on;


            s2.gameObject.GetComponent<Image>().sprite = s2.gameObject.GetComponent<ModeSelection>().off;
            s1.gameObject.GetComponent<Image>().sprite = s1.gameObject.GetComponent<ModeSelection>().off;
        }

    }
    public void timeonoff() {
        PlayerPrefs.SetInt("time", timekey);
        cancelsound();
    }
}