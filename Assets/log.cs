using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class log : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerLogic playerObject = collision.gameObject.GetComponent(typeof(PlayerLogic)) as PlayerLogic;

        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Obstacle Hit by Enemy");

        }

        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Obstacle Hit by Player");

        }





    }

    public void giveMessageInConsole()
    {
        Debug.Log("log working");
    }
}
