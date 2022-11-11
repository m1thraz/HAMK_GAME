using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpMenu : MonoBehaviour
{
    public GameObject powerUpMenu;
    public bool isPowerMenuOpen;


    public Button PowerupBtn1;
    public Button PowerupBtn2;
    public Button PowerupBtn3;


    // Powerup Sprites
    
    public Sprite PowerUp1;
    public Sprite PowerUp2;
    public Sprite PowerUp3;
    public Sprite PowerUp4;
    public Sprite PowerUp5;
    public Sprite PowerUp6;
    public Sprite PowerUp7;

    public Sprite[] powerUpSpriteList;

    //                            PowerUp Number     Available Count
    public int[,] powerArray = { {1,2,3,4,5}, {2,3,1,4,6} };

    public int[] minProb = { 1, 11, 41, 61, 91 };
    public int[] maxProb = { 10, 40, 60, 90, 100 };

    //never used
    public int[] pw1Prob = { 1, 10 }; // 10%
    public int[] pw2Prob = { 11, 40 }; // 30%
    public int[] pw3Prob = { 41, 60 }; // 20%
    public int[] pw4Prob = { 61, 90 }; // 30%
    public int[] pw5Prob = { 91, 100 }; // 10%



    // Image of the Buttons
    private Image pw1Image;
    private Image pw2Image;
    private Image pw3Image;

    // Chosen Powerups
    private int chosenPower1;
    private int chosenPower2;
    private int chosenPower3;

    private int choosenPW = 0;

    // Start is called before the first frame update
    void Start()
    {
        powerUpMenu.SetActive(false);
        isPowerMenuOpen = false;
        Debug.Log("Start powerupmenu script");

        pw1Image = PowerupBtn1.GetComponent<Image>(); 
        pw2Image = PowerupBtn2.GetComponent<Image>(); 
        pw3Image = PowerupBtn3.GetComponent<Image>();
     


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.C))
        {
            Debug.Log("change sprite");
            
            renderPowerUpImage(pw1Image, powerUpSpriteList[choosenPW]);
            choosenPW++;
        } 

    }

    public void openPowerUP()
    {
        powerUpMenu.SetActive(true);
        Time.timeScale = 0f;


        isPowerMenuOpen = true;
        Debug.Log("powerup script open");

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

    public void pauselog()
    {
        Debug.Log("powermenu workingggggg !!!!!!");
    }

    public void renderPowerUpImage(Image buttonSource , Sprite newPower )
    {
        buttonSource.sprite = newPower;

    }

    public void chooseRandomPowerUps()
    {
        bool pw1search = true;
        bool pw2search = true;
        bool pw3search = true;

        // random function

    while (pw1search || pw2search || pw3search)
        {

            if (pw1search)
            {
                int random = Random.Range(1, 100);
                int choosenPw = checkProb(random);

                if (checkAvaiable(choosenPw))
                {
                    chosenPower1 = choosenPw;
                    renderPowerUpImage(pw1Image, powerUpSpriteList[choosenPw]);

                }

                if(choosenPw != 0)
                {
                    pw1search = false;
                }
            }


            if (pw2search)
            {

            }

            if (pw3search)
            {

            }


        }
        





    }

    public bool checkAvaiable(int powerUpNumber)
    {
        int available = powerArray[1, powerUpNumber];

        if (available > 0)
        {
            Debug.Log("powerupnumber : " + powerUpNumber + " true");
            powerArray[1, powerUpNumber] = available--;
            Debug.Log("Only " + available + " times possible");
            return true;
        }
        else
        {
            return false;
        }
    }

    public int checkProb(int number)
    {
        for(int i = 0; i< 5; i++)
        {
            if (minProb[i] <= number && maxProb[i] >= number)
            {
                return i;
            }
        }

        return 0;
    }

}
