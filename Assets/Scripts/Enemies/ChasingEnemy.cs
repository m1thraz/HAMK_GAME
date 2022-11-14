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
    float moveSpeed = 8;
    [SerializeField] float maxHealth = 5;

    private float health;
    [SerializeField] float damage = 1;


    private void Awake()
    {
    }
    void Start()
    {
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
    private void FixedUpdate()
    {
        if (player)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);

        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerLogic playerObject = collision.gameObject.GetComponent(typeof(PlayerLogic)) as PlayerLogic;

        if (collision.gameObject.name == "Player" && health > 0)
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
            moveSpeed = 0;
            //damage = 0;
            Destroy(GetComponent<Collider2D>());
            animator.Play("die");
            playerObject2.ScoreUp();
            Debug.Log(animator.GetCurrentAnimatorStateInfo(0).length);
            Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
        }

    }
}
