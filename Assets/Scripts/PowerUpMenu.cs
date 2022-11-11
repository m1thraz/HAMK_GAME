using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpMenu : MonoBehaviour
{
    public GameObject powerUpMenu;
    public bool isPowerMenuOpen;
    // Start is called before the first frame update
    void Start()
    {
        powerUpMenu.SetActive(false);
        isPowerMenuOpen = false;
        Debug.Log("Start powerupmenu script");
    }

    // Update is called once per frame
    void Update()
    {
        // For Debugging

       
    }

    public void openPowerUP()
    {
        powerUpMenu.SetActive(true);
        Time.timeScale = 0f;
        isPowerMenuOpen = true;
    }


    public void closePowerUP()
    {
        powerUpMenu.SetActive(false);
        Time.timeScale = 1f;
        isPowerMenuOpen = false;
    }

    public bool isPaused()
    {
        if (isPowerMenuOpen)
        {
            return true;
        }
        else
        {
            return false;
        }
    } 
}
