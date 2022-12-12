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
    float shotTimer;
    [SerializeField]
    float projectileSpeed;

    public GameObject droppedCoin;

    private float slowTimer = 0;
    private bool slow;
    private float slowDuration = 2;
    private float normalSpeed;
    [SerializeField]
    private bool hardMode = false;
    EnemySpawner enemySpawner;


    // Start is called before the first frame update
    void Start()
    {
        enemySpawner = GameObject.Find("EnemySpawner").GetComponent(typeof(EnemySpawner)) as EnemySpawner;

        Physics.IgnoreLayerCollision(8, 7);
        Physics.IgnoreLayerCollision(7,8);
        shotTimer = fireDelay;
        currentMovespeed = moveSpeed;
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        health = maxHealth;
        player = GameObject.Find("Player").transform;
        animator = GetComponent<Animator>();

    }
    public void init(float difficultyMultiplier)
    {
        health *= difficultyMultiplier;
        damage *= difficultyMultiplier;
        hardMode = true;
        projectileSpeed *= 2;


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
            shotTimer += Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, currentMovespeed * Time.deltaTime);
            float distance = Mathf.Abs(Vector2.Distance(transform.position, player.transform.position));
            if (distance <= shotDistance && shotTimer >= fireDelay)
            {
                moveDirection = (player.position - transform.position).normalized;
                shootPlayer(moveDirection);
                shotTimer = 0;
            }

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player") && health > 0)
        {
            PlayerLogic playerObject = collision.gameObject.GetComponent(typeof(PlayerLogic)) as PlayerLogic;
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

        //hardMode spider
        if (projectile.name == "cobWeb" && hardMode)
        {
            int numberOfShots = 3;
            float maxAngle = 90;
            float angleDeviation = maxAngle / (numberOfShots + 1);
            for (int i = 1; i <= numberOfShots; i++)
            {
                float rotation = -1 * (maxAngle/2) + i * angleDeviation;
                GameObject shot = Instantiate(projectile, transform.position, Quaternion.identity);
                Vector3 shotDir = Quaternion.Euler(0, 0, rotation) * shotDirection;
                shot.GetComponent<Rigidbody2D>().velocity = new Vector2(shotDir.x, shotDir.y).normalized * projectileSpeed;

            }

        }
        else if (projectile.name == "fireballPrefab" && hardMode)
        {
            int numberOfShots = 4;
            float maxAngle = 120;
            float angleDeviation = maxAngle / (numberOfShots + 1);
            for (int i = 1; i <= numberOfShots; i++)
            {
                float rotation = -1 * (maxAngle / 2) + i * angleDeviation;
                GameObject shot = Instantiate(projectile, transform.position, Quaternion.identity);
                Vector3 shotDir = Quaternion.Euler(0, 0, rotation) * shotDirection;
                shot.GetComponent<Rigidbody2D>().velocity = new Vector2(shotDir.x, shotDir.y).normalized * projectileSpeed;

            }

        }
        else
        {
            GameObject newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
            newProjectile.GetComponent<Rigidbody2D>().velocity = shotDirection;
            Vector2 dir = new Vector2(player.transform.position.x, player.transform.position.y);
            animator.SetLayerWeight(1, 0);
            animator.SetLayerWeight(0, 1);
        }
        currentMovespeed = speedBefore;

    }

    public void takeDamage(float dmgAmount)
    {
        PlayerLogic playerObject2 = GameObject.Find("Player").GetComponent(typeof(PlayerLogic)) as PlayerLogic;

        health -= dmgAmount;
        if (health <= 0)
        {
            currentMovespeed = 0;
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
