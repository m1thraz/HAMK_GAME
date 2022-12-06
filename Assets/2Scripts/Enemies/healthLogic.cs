using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthLogic : MonoBehaviour
{
    [SerializeField]
    float maxHealth;
    private float health;
    float currentMovespeed;
    Animator animator;
    public GameObject droppedCoin;


    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void takeDamage(float dmgAmount)
    {
        PlayerLogic playerObject2 = GameObject.Find("Player").GetComponent(typeof(PlayerLogic)) as PlayerLogic;
        Debug.Log(string.Format("got HIT, taking damage", dmgAmount));
        health -= dmgAmount;
        Debug.Log(string.Format("remaining health {0}  ", health  ));
        if (health <= 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, transform.position, 0);
            
            if (GetComponent<smartChasing>())
            {
                GetComponent<smartChasing>().currentMovespeed = 0;
            }
            //damage = 0;
            Destroy(GetComponent<Collider2D>());
            animator.Play("die");
            playerObject2.ScoreUp();
            Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length);

            // DROP COIN 20% Chance 10-50 coins
            int coinsProb = Random.Range(1, 100);

            if (coinsProb > 1) // change %
            {
                GameObject coin = Instantiate(droppedCoin, transform.position, Quaternion.identity);

            }

            // maybe unkillable drop? mario star

        }

    }
}
