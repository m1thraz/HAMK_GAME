using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingEnemy : MonoBehaviour
{
    // Start is called before the first frame update

    Rigidbody2D rb;
    Transform player;
    Animator animator;
    Vector2 moveDirection;

    [SerializeField]
    float moveSpeed = 1;
    float currentMovespeed;
    [SerializeField] float maxHealth = 5;

    private float health;
    [SerializeField] float damage = 1;

    [SerializeField]
    float distance;

    public GameObject droppedCoin;


    private float slowTimer = 0;
    private bool slow;
    private float slowDuration = 2;
    private float normalSpeed;
    private void Awake()
    {
    }
    void Start()
    {
        currentMovespeed = moveSpeed;
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        health = maxHealth;
        player = GameObject.Find("Player").transform;
        animator = GetComponent<Animator>();
        Physics.IgnoreLayerCollision(8, 7);

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
        // This shoudl probalby taken out of this script and create a movement script
        // or move all the damage logic and shit out of this script
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
            distance = Mathf.Abs(Vector2.Distance(transform.position, player.transform.position));
            if(distance <= 0.4)
            {
                animator.SetLayerWeight(1, 1);
                animator.SetLayerWeight(0, 0);
            }
            else
            {
                animator.SetLayerWeight(1, 0);
                animator.SetLayerWeight(0, 1);
            }
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerLogic playerObject = collision.gameObject.GetComponent(typeof(PlayerLogic)) as PlayerLogic;

        if (collision.gameObject.CompareTag("Player") && health > 0)
        {
            Debug.Log(string.Format("player HIT, taking {0} damage", damage));
            Destroy(gameObject);

            playerObject.takeDamage(damage);

        }

        //edit this so it decreases health instead of destroying the gameobject
        //if (collision.gameObject.tag == "bullet")
        //{

        //    Debug.Log("Bullet hit enemy");


        //    Destroy(gameObject);


        //    playerObject2.ScoreUp();
        //}
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
