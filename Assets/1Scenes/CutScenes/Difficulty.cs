using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Difficulty : MonoBehaviour
{
    public GameObject pauseMenu;
    public void EasyMode()
    {
        SceneManager.LoadScene(1);
    }

    public void NormalMode()
    {
        SceneManager.LoadScene(1);
    }
    public void HardMode()
    {
        SceneManager.LoadScene(1);
    }
    public void EndPause()
    {
        pauseMenu.SetActive(false);
    }

}
