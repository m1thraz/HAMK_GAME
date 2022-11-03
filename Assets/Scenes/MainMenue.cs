using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenue : MonoBehaviour
{


    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenSettings()
    {

    }

    public void CloseSettings()
    {

    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
