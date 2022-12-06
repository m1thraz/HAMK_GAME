using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject settingsPanel, GameSelectPanel;

    public void StartWaves()
    {
        SceneManager.LoadScene(2);
    }

    public void StartStory()
    {
        SceneManager.LoadScene(4);
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }
    public void OpenGameSelect()
    {
        GameSelectPanel.SetActive(true);
    }

    public void CloseGameSelect()
    {
        GameSelectPanel.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
