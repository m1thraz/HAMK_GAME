using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;


public class BulletLogic : MonoBehaviour
{

    float BulletTime = 2;
    private float damage = 1;


    // Update is called once per frame
    void Update()
    {
        DeleteBullet();
    }

    //replace this with decreasing health - Daan
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            collision.gameObject.GetComponent<ChasingEnemy>().takeDamage(damage);
            
            Destroy(gameObject);

        }
    }

    //CODE FOR DECREASING HEALTH DONT DELETE
    //might be osbelete with above lines

    //private void OnTriggerEnter2D(Collider2D collider)
    //{
    //    if (collider.CompareTag("enemy"))
    //    {
    //        collider.GetComponent<Health>().Damage(2);
    //        throw new System.ArgumentOutOfRangeException("Impact");

    //    }
    //}

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
    public void setDamage(float dmgAmount)
    {
        damage = dmgAmount;
    }
}
