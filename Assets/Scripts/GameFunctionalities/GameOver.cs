using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI Score;

    public void Start()
    {
        Score.text = "Score: " + PlayerPrefs.GetFloat("playScore"); ;
    }
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
