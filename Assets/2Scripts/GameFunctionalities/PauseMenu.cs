using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public GameObject controlsMenu;
    PlayerMovement controlls;
    public GameObject Player;
    private bool muted = false;
    [SerializeField] Slider volumeSlider;


    public void Start()
    {
        controlls = Player.GetComponent(typeof(PlayerMovement)) as PlayerMovement;
        if (!PlayerPrefs.HasKey("musicVolume")){


            PlayerPrefs.SetFloat("musicVolume", 1);
        } else
        {
            LoadVolume();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }


    public void ExitGame()
    {
        Application.Quit();
    }
    public void Return()
    {

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
    public void SetVolume()
    {
        AudioListener.volume = volumeSlider.value;
    }

    public void OpenSettings()
    {
        settingsMenu.SetActive(true);
    }
    public void CloseSettings()
    {
        settingsMenu.SetActive(false);
        SaveVolume();
    }

    public void OpenControls()
    {
        controlsMenu.SetActive(true);
    }
    public void CloseControls()
    {
        controlsMenu.SetActive(false);
    }


    private void LoadVolume()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }
    private void SaveVolume()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }

    public void toggleControls()
    {
        if (PlayerPrefs.GetInt("shootcontrol") == 0)
        {
            PlayerPrefs.SetInt("shootcontrol", 1);
            
        }
        else
        {
            PlayerPrefs.SetInt("shootcontrol", 0);
            
        }
    }
}
