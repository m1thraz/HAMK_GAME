using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ArmorShop : MonoBehaviour
{
    PlayerLogic playerLogic;

    int armorCost = 10;
    int coins;

    Color colorYellow = new Color(0, 255, 255);
    Color colorRed = new Color(255, 0, 0);

    public GameObject armorPanel;

    public TextMeshProUGUI TextPrice;

    bool isShopOpen = false;


    void Start()
    {
        playerLogic = GameObject.FindGameObjectWithTag("Player").GetComponent(typeof(PlayerLogic)) as PlayerLogic;
        armorPanel.SetActive(false);
    }

    void Update()
    {

        // DEBUG FOR COIN INCREASE
        if (Input.GetKeyDown(KeyCode.C))
        {
            playerLogic.increaseCoin(10);
            Debug.Log("coins= " + playerLogic.getCoin());
            

        }



        // Close Shop if not enough Coins
        if (Input.GetKeyDown(KeyCode.F) && isShopOpen)
        {
            armorPanel.SetActive(false);
            Time.timeScale = 1f;
            isShopOpen = false;

        }

        // Buy Armor
        if (Input.GetKeyDown(KeyCode.E) && isShopOpen)
        {
            if (buyArmor())
            {
                Time.timeScale = 1f;
                armorPanel.SetActive(false);
            }
            
        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.name == "Player")
        { openArmorShop(); }
        
    }


    private bool buyArmor()
    {
        if(coins >= armorCost)
        {
            playerLogic.increaseArmor(5);
            print($"<color=#00FF00> Armorbuy success.</color>");
            armorPanel.SetActive(false);

            playerLogic.increaseCoin(-armorCost);
            armorCost *= 2;
            isShopOpen = false;
            return true;
        }
        return false;

        
    }





    public void openArmorShop()
    {
        isShopOpen = true;
        armorPanel.SetActive(true);
        Time.timeScale = 0.0f;
        coins = playerLogic.getCoin();

        if (coins >= armorCost)
        {
            TextPrice.color = Color.yellow;
            TextPrice.SetText(armorCost + " Coins");

        }
        else
        {
            TextPrice.color = Color.red;
            TextPrice.SetText(string.Format("<s> {0} Coins </s>", armorCost));
        }

    }
}