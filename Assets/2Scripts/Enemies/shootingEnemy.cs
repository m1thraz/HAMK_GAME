using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootingEnemy : MonoBehaviour
{
    Rigidbody2D rb;
    Transform player;
    Animator animator;
    Vector2 moveDirection;
    [SerializeField]
    GameObject projectile;

    [SerializeField]
    float moveSpeed;
    float currentMovespeed;
    [SerializeField] float maxHealth;

    private float health;
    [SerializeField] float damage;

    [SerializeField]
    float shotDistance;

    [SerializeField]
    float fireDelay;
    float nextShot = 0.15f;
    [SerializeField]
    float projectileSpeed;

    public GameObject droppedCoin;

    private float slowTimer = 0;
    private bool slow;
    private float slowDuration = 2;
    private float normalSpeed;
    EnemySpawner enemySpawner;


    // Start is called before the first frame update
    void Start()
    {
        enemySpawner = GameObject.Find("EnemySpawner").GetComponent(typeof(EnemySpawner)) as EnemySpawner;

        Physics.IgnoreLayerCollision(8, 7);
        Physics.IgnoreLayerCollision(7,8);

        currentMovespeed = moveSpeed;
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        health = maxHealth;
        player = GameObject.Find("Player").transform;
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            //Vector2 direction = (player.transform.position - transform.position).normalized;
            moveDirection = (player.position - transform.position).normalized;
            animator.SetFloat("xDir", moveDirection.x);
            animator.SetFloat("yDir", moveDirection.y);
        }
    }
    public void slowMovement()
    {
        if (slow)
        {
            slowTimer = 0;
        }
        else
        {
            normalSpeed = currentMovespeed;
            float slowedSpeed = 0.5f * currentMovespeed;

            slow = true;
            currentMovespeed = slowedSpeed;
        }

    }
    private void FixedUpdate()
    {
        if (player)
        {
            if (slow)
            {
                slowTimer += Time.deltaTime;
                if (slowTimer >= slowDuration)
                {
                    slowTimer = 0;
                    slow = false;
                    currentMovespeed = normalSpeed;
                }
            }
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, currentMovespeed * Time.deltaTime);
            moveDirection = (player.position - transform.position).normalized;
            float distance = Mathf.Abs(Vector2.Distance(transform.position, player.transform.position));
            Debug.Log("distance from player ");
            Debug.Log(distance);
            if (distance <= shotDistance && Time.time > fireDelay)
            {
                Debug.Log("trying to shoot");
                shootPlayer(moveDirection);
                fireDelay += Time.time + nextShot;
            }

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player") && health > 0)
        {
            PlayerLogic playerObject = collision.gameObject.GetComponent(typeof(PlayerLogic)) as PlayerLogic;


            Debug.Log(string.Format("player HIT, taking {0} damage", damage));
            Destroy(gameObject);
            enemySpawner.enemyDied();

            playerObject.takeDamage(damage);

        }

    }

    private void shootPlayer(Vector2 shotDirection)
    {

        float speedBefore = currentMovespeed;
        currentMovespeed = 0;
        animator.SetLayerWeight(1, 1);
        animator.SetLayerWeight(0, 0);

        GameObject newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
        //newProjectile.GetComponent<Rigidbody2D>().velocity = shotDirection * projectileSpeed;
        newProjectile.GetComponent<Rigidbody2D>().velocity = shotDirection;
        //transform.rotation = Quaternion.LookRotation(Vector3.forward, shootDirection);
        Vector2 dir = new Vector2(player.transform.position.x, player.transform.position.y);
        //newProjectile.GetComponent<Rigidbody2D>().transform.rotation = Quaternion.LookRotation(Vector3.forward, shotDirection);


        animator.SetLayerWeight(1, 0);
        animator.SetLayerWeight(0, 1);
        currentMovespeed = speedBefore;
    }
    
    public void takeDamage(float dmgAmount)
    {
        PlayerLogic playerObject2 = GameObject.Find("Player").GetComponent(typeof(PlayerLogic)) as PlayerLogic;

        health -= dmgAmount;
        if (health <= 0)
        {
            currentMovespeed = 0;
            //damage = 0;
            Destroy(GetComponent<Collider2D>());
            animator.Play("die");
            playerObject2.ScoreUp();
            Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
            enemySpawner.enemyDied();


            // DROP COIN 30% Chance 10-50 coins
            int coinsProb = Random.Range(1, 100);

            if (coinsProb > 69) // change %
            {
                GameObject coin = Instantiate(droppedCoin, transform.position, Quaternion.identity);

            }


        }

    }

}
