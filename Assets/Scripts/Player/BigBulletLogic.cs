using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;


public class BigBulletLogic : MonoBehaviour
{

    float BulletTime = 2;
    private float damage = 2;
    private Animator animator;
    public PlayerMovement playerMovement;


    private void Start()
    {
        animator = GetComponent<Animator>();
       

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        DeleteBullet();
    }

    //replace this with decreasing health - Daan
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {

            if (collision.gameObject.GetComponent<ChasingEnemy>() != null)
            {
                collision.gameObject.GetComponent<ChasingEnemy>().takeDamage(damage);
            }
            else
            {
                collision.gameObject.GetComponent<shootingEnemy>().takeDamage(damage);
            }

            FindObjectOfType<AudoManager>().Play("explosion");
            animator.Play("explosion");


            Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length);

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
