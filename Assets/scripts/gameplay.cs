using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SocialPlatforms.Impl;

enum Category
{
    Color,
    Size,
    Speed,
    Logic
}
public class Gameplay : MonoBehaviour
{
    [Header("UI")]
    public Text questionText;
    public Text timerText;
    public Text scoretext;
    public GameObject errorSprite;
    public GameObject winSprite;
    public GameObject gameplaymenu;
    public GameObject timeupmenu;
    [Header("Color buttons")]
    public Button blue, red, white, yellow;

    [Header("Size buttons")]
    public Button circle, bigCube, smallCube, rotatedCube;

    [Header("Logic buttons")]
    public Button checkedBtn, notCheckedBtn, withoutCheckBtn;

    [Header("Movement")]
    public GameObject movementPanel;
    [Header("Speed")]
    public Button speedButton;

    int targetClicks;
    float targetTime;
    int clickCount;
    float speedTimer;
    bool speedActive;

    float timer;
    int correctAnswerId;
    Category currentCategory;
    List<Category> enabledCategories = new List<Category>();

    void Start()
    {
        PlayerPrefs.SetInt("score", 0);
        timer = PlayerPrefs.GetInt("time", 30);
        InitCategories();
        NextQuestion();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0) timer = 0;

        UpdateTimerText();

        if (timer <= 0)
        {
            EndGame();
        }
        if (speedActive)
        {
            speedTimer -= Time.deltaTime;

            if (speedTimer <= 0)
            {
                speedActive = false;
                FailSpeed();
            }
        }
    }
    public void SpeedClick()
    {
        if (!speedActive) return;

        clickCount++;

        if (clickCount >= targetClicks)
        {
            speedActive = false;
            WinSpeed();
        }
    }

    void WinSpeed()
    {
        int score = PlayerPrefs.GetInt("score", 0);
        PlayerPrefs.SetInt("score", score + 5);

        if (PlayerPrefs.GetInt("sound") == 1)
        {
            GameObject.Find("correct").GetComponent<AudioSource>().Play();
        }
        scoretext.text = PlayerPrefs.GetInt("score", 0).ToString();
        winSprite.SetActive(true);
        Invoke(nameof(NextQuestion), 0.5f);
    }

    void FailSpeed()
    {
        int score = PlayerPrefs.GetInt("score", 0);
        if (PlayerPrefs.GetInt("score", 0) > 0)
        {
            PlayerPrefs.SetInt("score", score - 5);
        }
        if (PlayerPrefs.GetInt("sound") == 1)
        {
            GameObject.Find("error").GetComponent<AudioSource>().Play();
        }
        errorSprite.SetActive(true);
        Invoke(nameof(NextQuestion), 0.5f);
    }
    void UpdateTimerText()
    {
        int seconds = Mathf.CeilToInt(timer);
        int minutes = seconds / 60;
        int sec = seconds % 60;

        timerText.text = minutes.ToString("00") + ":" + sec.ToString("00");
    }
    void InitCategories()
    {
        if (PlayerPrefs.GetInt("color") == 1) enabledCategories.Add(Category.Color);
        if (PlayerPrefs.GetInt("size") == 1) enabledCategories.Add(Category.Size);
        if (PlayerPrefs.GetInt("speed") == 1) enabledCategories.Add(Category.Speed);
        if (PlayerPrefs.GetInt("logic") == 1) enabledCategories.Add(Category.Logic);
    }

    void NextQuestion()
    {
        HideAll();

        currentCategory = enabledCategories[Random.Range(0, enabledCategories.Count)];

        switch (currentCategory)
        {
            case Category.Color:
                ColorQuestion();
                break;
            case Category.Size:
                SizeQuestion();
                break;
            case Category.Logic:
                LogicQuestion();
                break;
            case Category.Speed:
                SpeedQuestion();
                break;
        }
    }

    void HideAll()
    {
        blue.gameObject.SetActive(false);
        red.gameObject.SetActive(false);
        white.gameObject.SetActive(false);
        yellow.gameObject.SetActive(false);

        circle.gameObject.SetActive(false);
        bigCube.gameObject.SetActive(false);
        smallCube.gameObject.SetActive(false);
        rotatedCube.gameObject.SetActive(false);

        checkedBtn.gameObject.SetActive(false);
        notCheckedBtn.gameObject.SetActive(false);
        withoutCheckBtn.gameObject.SetActive(false);
        errorSprite.gameObject.SetActive(false);
        winSprite.gameObject.SetActive(false);
        movementPanel.SetActive(false);
    }

    // -------- QUESTIONS --------

    void ColorQuestion()
    {
        Button[] colors = { blue, red, white, yellow };
        string[] names = { "blue", "red", "white", "yellow" };

        int correctIndex = Random.Range(0, colors.Length);
        correctAnswerId = correctIndex;

        questionText.text = "Select " + names[correctIndex] + " color";

        for (int i = 0; i < colors.Length; i++)
        {
            colors[i].gameObject.SetActive(true);
        }
    }


    void SizeQuestion()
    {
        Button[] shapes = { circle, bigCube, smallCube, rotatedCube };
        string[] names = { "circle", "big cube", "small cube", "rotated cube" };

        int correctIndex = Random.Range(0, shapes.Length);
        correctAnswerId = correctIndex;

        questionText.text = "Select " + names[correctIndex];

        for (int i = 0; i < shapes.Length; i++)
        {
            shapes[i].gameObject.SetActive(true);
        }
    }

    void LogicQuestion()
    {
        Button[] logicButtons = { checkedBtn, notCheckedBtn, withoutCheckBtn };
        string[] names = { "checked", "not checked", "without check" };

        int correctIndex = Random.Range(0, logicButtons.Length);
        correctAnswerId = correctIndex;

        questionText.text = "Select " + names[correctIndex];

        for (int i = 0; i < logicButtons.Length; i++)
        {
            logicButtons[i].gameObject.SetActive(true);
        }
    }


    void SpeedQuestion()
    {
        movementPanel.SetActive(true);

        // примеры заданий
        if (Random.Range(0, 2) == 0)
        {
            targetClicks = 2;
            targetTime = 1f;
        }
        else
        {
            targetClicks = 5;
            targetTime = 3f;
        }

        clickCount = 0;
        speedTimer = targetTime;
        speedActive = true;

        questionText.text = "Click " + targetClicks + " times in " + targetTime + " seconds";
    }


    // -------- ANSWER --------

    public void Answer(int id)
    {
        if (id == correctAnswerId)
        {
            int score = PlayerPrefs.GetInt("score", 0);
            PlayerPrefs.SetInt("score", score + 5);
            if (PlayerPrefs.GetInt("bestscore") < PlayerPrefs.GetInt("score")) {
                PlayerPrefs.SetInt("bestscore", score + 5);
            }
            scoretext.text = (score+5).ToString();
            if (PlayerPrefs.GetInt("sound") == 1)
            {
                GameObject.Find("correct").GetComponent<AudioSource>().Play();
            }
            winSprite.SetActive(true);
        }
        else
        {
            int score = PlayerPrefs.GetInt("score", 0);
            if (score > 0)
            {
                PlayerPrefs.SetInt("score", score - 5);
            }

            if (PlayerPrefs.GetInt("sound") == 1)
            {
                GameObject.Find("error").GetComponent<AudioSource>().Play();
            }
            scoretext.text = PlayerPrefs.GetInt("score", 0).ToString();
            errorSprite.SetActive(true);
        }

        Invoke(nameof(NextQuestion), 0.5f);
    }

    void EndGame()
    {
        timeupmenu.SetActive(true);
        gameplaymenu.SetActive(false);

       
    }
}
