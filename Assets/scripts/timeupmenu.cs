using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timeupmenu : MonoBehaviour
{
    public Text score;
    public Text bestscore;

    private void OnEnable()
    {
        score.text = PlayerPrefs.GetInt("score").ToString();
        bestscore.text = PlayerPrefs.GetInt("bestscore").ToString();
    }
}
