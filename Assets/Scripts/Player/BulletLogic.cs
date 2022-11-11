using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;


public class BulletLogic : MonoBehaviour
{

    float BulletTime = 2;
    private int damage = 3;


    // Update is called once per frame
    void Update()
    {
        DeleteBullet();
    }

    private void OnCollisionEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<Health>() != null)
        {
            Health health = collider.GetComponent<Health>();
            FindObjectOfType<AudoManager>().Play("explosion");
            health.Damage(damage);
            //Destroy(gameObject);         

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
