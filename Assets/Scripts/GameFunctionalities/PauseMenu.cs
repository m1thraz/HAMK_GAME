using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // public GameObject settingsPanel;
    public GameObject pauseMenu;
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
        pauseMenu.SetActive(false);
    }
}
