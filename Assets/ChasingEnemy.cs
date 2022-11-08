using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingEnemy : MonoBehaviour
{
    // Start is called before the first frame update

    float moveSpeed = 8;   
    [SerializeField] float maxHealth = 5, health;
    [SerializeField] float damage = 1;
    Rigidbody2D rb;
    Transform player;
    Vector2 moveDirection;

   


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        health = maxHealth;
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
            moveDirection = direction;
        }
    }
    private void FixedUpdate()
    {
        if (player)
        {
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * (moveSpeed * Time.deltaTime);
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerLogic playerObject = collision.gameObject.GetComponent(typeof(PlayerLogic)) as PlayerLogic;
        PlayerLogic playerObject2 = GameObject.Find("Player").GetComponent(typeof(PlayerLogic)) as PlayerLogic;

        if (collision.gameObject.name == "Player")
        {
            Debug.Log(string.Format("player HIT, taking {0} damage", damage));
            Destroy(gameObject);
            playerObject.ScoreUp();

            playerObject.takeDamage(damage);
            
        }

        if (collision.gameObject.tag == "bullet")
        {
            Debug.Log("Bullet hit enemy");

         
            Destroy(gameObject);


            playerObject2.ScoreUp();
        }




    }


}
