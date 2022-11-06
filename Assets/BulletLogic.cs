using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;

public class BulletLogic : MonoBehaviour
{
    private GameObject scoreboard;

    // Start is called before the first frame update
    void Start()
    {
        scoreboard = GameObject.Find("Score");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            Destroy(gameObject);
            scoreboard.GetComponent<Text>().text = "test";
        }




    }
}
