using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class kingBoss : MonoBehaviour
{
    // Start is called before the first frame update
    // Start is called before the first frame update

    Rigidbody2D rb;
    Transform player;
    Animator animator;
    Vector2 moveDirection;

    [SerializeField]
    float moveSpeed = 8;
    float currentMovespeed;

    [SerializeField] 
    float damage;

    [SerializeField]
    float fireIntervall;
    private float fireTimer;
    [SerializeField]
    float projectileSpeed;
    [SerializeField]
    bossFireball projectile;

    [SerializeField]
    int winScene;

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
        player = GameObject.Find("Player").transform;
        animator = GetComponent<Animator>();
        Physics.IgnoreLayerCollision(8,10);

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
    public void onDeath()
    {
        float score = player.GetComponent<PlayerLogic>().getScore();
        PlayerPrefs.SetFloat("playScore", score);
        SceneManager.LoadScene(winScene);
        
    }
    private void FixedUpdate()
    {
        // This shoudl probalby taken out of this script and create a movement script
        // or move all the damage logic and shit out of this script
        if (player)
        {
            fireTimer += Time.deltaTime;
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
            if (fireTimer >= fireIntervall)
            {
                shootPlayer();
                fireTimer = 0;
            }
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
    private void shootPlayer() {

        int rand = Random.Range(3, 8);

        float angleDeviation = 180 / (rand +1);
        Vector3 playerDirection = (player.position - transform.position).normalized;

        for (int i = 1; i <= rand; i++)

        {
            
            float rotation = -90 + i * angleDeviation;
            bossFireball newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
            //Vector3 shotDirection = RotateZ(playerDirection, rotation);
            Vector3 shotDirection = Quaternion.Euler(0, 0, rotation) * playerDirection;
            newProjectile.moveDirection = new Vector2(shotDirection.x, shotDirection.y).normalized;

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerLogic playerObject = collision.gameObject.GetComponent(typeof(PlayerLogic)) as PlayerLogic;

        if (collision.gameObject.CompareTag("Player"))
        {
            playerObject.takeDamage(damage);
        }
    }
}

