using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI Score;
    public TextMeshProUGUI HighScore;

    public void Start()
    {
        Score.text = "Score: " + PlayerPrefs.GetFloat("playScore"); 
        if (!PlayerPrefs.HasKey("highScore"))
        {
            HighScore.text = "Highscore: " + PlayerPrefs.GetFloat("playScore");
            PlayerPrefs.SetFloat("highScore", PlayerPrefs.GetFloat("playScore"));
        } else
        {
            if(PlayerPrefs.GetFloat("playScore") < PlayerPrefs.GetFloat("highScore"))
            {
                HighScore.text ="Highscore: " + PlayerPrefs.GetFloat("highScore");
            } else
            {
                PlayerPrefs.SetFloat("highScore", PlayerPrefs.GetFloat("playScore"));
                HighScore.text = "Highscore: " + PlayerPrefs.GetFloat("highScore");
            }
        }

    }
    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
