using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class healthLogic : MonoBehaviour
{
    [SerializeField]
    float maxHealth;
    private float health;
    float currentMovespeed;
    Animator animator;
    public GameObject droppedCoin;
    EnemySpawner enemySpawner;

    [SerializeField]
    GameObject endGameAdds;


    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        animator = GetComponent<Animator>();
        enemySpawner = GameObject.Find("EnemySpawner").GetComponent(typeof(EnemySpawner)) as EnemySpawner;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void takeDamage(float dmgAmount)
    {
        PlayerLogic playerObject2 = GameObject.Find("Player").GetComponent(typeof(PlayerLogic)) as PlayerLogic;
        health -= dmgAmount;
        if (health <= 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, transform.position, 0);

            if (!GetComponent<bossFireball>())
            {
                animator.Play("die");

                Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
            } else
            {
                Destroy(gameObject);
            }
            if (GetComponent<kingBoss>())
            {
                animator.Play("die");
                GameObject[] spiders = GameObject.FindGameObjectsWithTag("spider");
                if (spiders.Length > 0)
                {


                    foreach (GameObject spider in spiders)
                    {
                        Destroy(spider);
                    }
                }
                kingBoss boss = GetComponent(typeof(kingBoss)) as kingBoss;
                boss.onDeath();
            }
            if (GetComponent<smartChasing>())
            {
                GetComponent<smartChasing>().currentMovespeed = 0;
            }
            //damage = 0;
            Destroy(GetComponent<Collider2D>());
            playerObject2.ScoreUp();
            enemySpawner.enemyDied();


            // DROP COIN 20% Chance 10-50 coins
            if (!GetComponent<bossFireball>())
            {
                int coinsProb = Random.Range(1, 100);

                if (coinsProb > 69) // change %
                {
                    GameObject coin = Instantiate(droppedCoin, transform.position, Quaternion.identity);

                }
            }

            // maybe unkillable drop? mario star

        }

    }
}
