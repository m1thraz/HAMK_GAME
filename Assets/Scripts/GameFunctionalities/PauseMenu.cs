using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // public GameObject settingsPanel;
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    PlayerMovement controlls;
    public GameObject Player;
    private bool muted = false;


    public void Start()
    {
        controlls = Player.GetComponent(typeof(PlayerMovement)) as PlayerMovement;

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

/*    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }*/

/*    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }*/
    public void ExitGame()
    {
        Application.Quit();
    }
    public void Return()
    {
        //   pauseMenu.SetActive(false);
        // Time.timeScale = 1f;
        controlls.Pause();
    }
    public void Mute()
    {
        if(muted)
        {
            AudioListener.pause = true;
            muted = false;
        }

        else
        {
            AudioListener.pause = false;
            muted = true;
        }

    }

    public void OpenSettings()
    {
        settingsMenu.SetActive(true);
    }
    public void CloseSettings()
    {
        settingsMenu.SetActive(false);
    }
}
