using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;


public class BulletLogic : MonoBehaviour
{

    float BulletTime = 2;


    // Update is called once per frame
    void Update()
    {
        DeleteBullet();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            Destroy(gameObject);         

        }
    }

    private void DeleteBullet()
    {
        if (BulletTime > 0)
        {
            BulletTime -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
