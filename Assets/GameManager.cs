using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class GameManager : MonoBehaviour
{

    private GameObject BulletLogic;
    private int enemyKillCount;
    PowerUpMenu powerMenu;


    // Start is called before the first frame update
    void Start()
    {
        PowerUpMenu powerMenu = GameObject.Find("GameManager").GetComponent(typeof(PowerUpMenu)) as PowerUpMenu;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Input func");

        


    }


}


