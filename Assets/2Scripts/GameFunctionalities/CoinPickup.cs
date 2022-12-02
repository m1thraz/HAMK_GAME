using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{

    PlayerLogic playerLogic;

    // Start is called before the first frame update
    void Start()
    {
        playerLogic = GameObject.FindGameObjectWithTag("Player").GetComponent(typeof(PlayerLogic)) as PlayerLogic;
    }

    // Update is called once per frame
    void Update()
    {

    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("took Coin");




            int amount = Random.Range(1, 5) * 10;
            playerLogic.increaseCoin(amount);
            Destroy(gameObject);



        }

    }
}