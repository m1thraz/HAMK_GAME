using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthShop : MonoBehaviour
{
    PlayerLogic playerLogic;

    int healthCost = 10;
    int coins;

    Color colorYellow = new Color(0, 255, 255);
    Color colorRed = new Color(255, 0, 0);

    public GameObject healthPanel;

    public TextMeshProUGUI TextPrice;

    bool isShopOpenHealth = false;


    void Start()
    {
        playerLogic = GameObject.FindGameObjectWithTag("Player").GetComponent(typeof(PlayerLogic)) as PlayerLogic;
        healthPanel.SetActive(false);
    }

    void Update()
    {

        
        // Close Shop if not enough Coins
        if (Input.GetKeyDown(KeyCode.F) && isShopOpenHealth)
        {
            
            healthPanel.SetActive(false);
            isShopOpenHealth =  false;
            Time.timeScale = 1f;

        }

        // Buy Health
        if (Input.GetKeyDown(KeyCode.E) && isShopOpenHealth)
        {
            Debug.Log("INSIDE HEALTH KEY E");
            if (buyHealth())
            {

                Time.timeScale = 1f;
                healthPanel.SetActive(false);
            }

        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.name == "Player")
        { openHealthShop(); }

    }


    private bool buyHealth()
    {
        if (coins >= healthCost)
        {
            playerLogic.increasePlayerHealth();
            print($"<color=#00FF00> Fullheal Player success.</color>");
            healthPanel.SetActive(false);

            playerLogic.increaseCoin(-healthCost);
            healthCost *= 2;
            isShopOpenHealth = false;
            return true;
        }
        return false;


    }





    public void openHealthShop()
    {
        isShopOpenHealth = true;
        healthPanel.SetActive(true);
        Time.timeScale = 0.0f;
        coins = playerLogic.getCoin();

        if (coins >= healthCost)
        {
            TextPrice.color = Color.yellow;
            TextPrice.SetText(healthCost + " Coins");

        }
        else
        {
            TextPrice.color = Color.red;
            TextPrice.SetText(string.Format("<s> {0} Coins </s>", healthCost));
        }

    }
}