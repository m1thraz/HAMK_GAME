using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PowerUpMenu : MonoBehaviour
{
    public GameObject powerUpMenu;
    public bool isPowerMenuOpen;


    public Button PowerupBtn1;
    public Button PowerupBtn2;
    public Button PowerupBtn3;


    public TextMeshProUGUI pw1Text;
    public TextMeshProUGUI pw2Text;
    public TextMeshProUGUI pw3Text;

    private static string PW0DESCRIPTION = "Decrease Spellcasttime "; // Castspeed WORKING
    private static string PW1DESCRIPTION = "Increase max HP"; // Health potion   WORKING
    private static string PW2DESCRIPTION = "Lower Dashtimer "; // Dash timer lower
    private static string PW3DESCRIPTION = "Increase Playerspeed"; // Feather speed WORKING
    private static string PW4DESCRIPTION = "Bigger Magicspell 10x  "; //bigger spells for 10 shoots
    private static string PW5DESCRIPTION = "Freezing Spell"; // increase playerdamage
    // bouncing bullets count collison 2times / 3 times
    // crit dmg ?
    // poison bullet ?
    // cone style shooting ?
    // bullet shooting through target
    // aoe bullets


    private string[] pwDescriptions = {PW0DESCRIPTION, PW1DESCRIPTION , PW2DESCRIPTION , PW3DESCRIPTION , PW4DESCRIPTION };
    // maybe later


    // Powerup Sprite Array

    public Sprite[] powerUpSpriteList;

    // Arry with Powerup Nr | available  PowerUp Number     Available Count
    public int[,] powerArray =          { {0,1,2,3,4}, {20,100,30,20,10} };


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

    PlayerLogic playerLogic;
    PlayerMovement playerMovement;

    CanvasGroup canvasGroup;


    [SerializeField] private Button Button1 = null;
    [SerializeField] private Button Button2 = null;
    [SerializeField] private Button Button3 = null;

    

        // Start is called before the first frame update
        void Start()
    {
        //powerUpMenu.SetActive(false);
        isPowerMenuOpen = false;
       // Debug.Log("Start powerupmenu script");


        playerLogic = GameObject.FindGameObjectWithTag("Player").GetComponent(typeof(PlayerLogic)) as PlayerLogic;
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent(typeof(PlayerMovement)) as PlayerMovement;


        pw1Image = PowerupBtn1.GetComponent<Image>(); 
        pw2Image = PowerupBtn2.GetComponent<Image>(); 
        pw3Image = PowerupBtn3.GetComponent<Image>();

        canvasGroup = powerUpMenu.GetComponent<CanvasGroup>();

        canvasGroup.alpha = 0.0f;
        canvasGroup.interactable = false;

       



    }

    // Update is called once per frame
    void Update()
    {
       
        
    }

    public void openPowerUP()
    {
        // powerUpMenu.SetActive(true);
        canvasGroup.alpha = 1.0f;
        canvasGroup.interactable = true;
        Time.timeScale = 0f;

        chooseRandomPowerUps();
        isPowerMenuOpen = true;
        Debug.Log("powerup script open");

    }


    public void closePowerUP()
    {
        // powerUpMenu.SetActive(false);
        canvasGroup.alpha = 0.0f;
        canvasGroup.interactable = false;
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
                    pw1Text.text = pwDescriptions[choosenPw];
                    Debug.Log("pw1 chosen");

                }

                if(choosenPw != 99)
                {
                    pw1search = false;
                }
            }


            if (pw2search)
            {
                int random2 = Random.Range(1, 100);
                int choosenPw2 = checkProb(random2);

                if (checkAvaiable(choosenPw2))
                {
                    chosenPower2 = choosenPw2;
                    renderPowerUpImage(pw2Image, powerUpSpriteList[choosenPw2]);
                    pw2Text.text = pwDescriptions[choosenPw2];
                    Debug.Log("pw2 chosen");

                }

                if (choosenPw2 != 99)
                {
                    pw2search = false;
                }
            }

            if (pw3search)
            {
                int random3 = Random.Range(1, 100);
                int choosenPw3 = checkProb(random3);

                if (checkAvaiable(choosenPw3))
                {
                    chosenPower3 = choosenPw3;
                    renderPowerUpImage(pw3Image, powerUpSpriteList[choosenPw3]);
                    pw3Text.text = pwDescriptions[choosenPw3];
                    Debug.Log("pw3 chosen");

                }

                if (choosenPw3 != 99)
                {
                    pw3search = false;
                }
            }


        }

    }

    public bool checkAvaiable(int powerUpNumber)
    {
        int available = powerArray[1, powerUpNumber];

        if (available > 0)
        {
            Debug.Log("powerupnumber : " + powerUpNumber + " true");
            available--;
            powerArray[1, powerUpNumber] = available;
            Debug.Log("Only " + available + " times possible now");
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

        return 99;
    }


    public void Buttonclicked(int buttonNumber)
    {
        switch (buttonNumber)
        {
            case 1:
                activatePowerUp(chosenPower1);
                Debug.Log("button1 clicked");
                break;

            case 2:
                activatePowerUp(chosenPower2);
                Debug.Log("button2 clicked");
                break;

            case 3:
                activatePowerUp(chosenPower3);
                Debug.Log("button3 clicked");
                break;

        }
    }

    public void activatePowerUp(int pwnumber)
    {
        Debug.Log("activatepowerUP nr. " + pwnumber );
        switch(pwnumber)
        {
            case 0:
                    Debug.Log("pw0 activated");
                    playerMovement.increaseShootSpeed();
                    break;
            case 1:
                    Debug.Log("pw1 activated max hp");
                    playerLogic.increaseMaxHP();
                    closePowerUP();
                    break;

            case 2:
                    Debug.Log("pw2 activated");
                    // Dashtimer lower
                    break;

            case 3:
                    Debug.Log("pw3 activated");
                    playerMovement.increaseSpeed();
                    closePowerUP();
                    break;

            case 4:
                    Debug.Log("pw4 activated");

                    playerMovement.activateBigSpell();
                    break;

            case 5:
                    Debug.Log("pw5 activated");
                    playerMovement.activateBigSpellFreeze();
                    break;




        }

        closePowerUP();
    }

}
