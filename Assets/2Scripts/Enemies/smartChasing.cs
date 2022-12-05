using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smartChasing : MonoBehaviour
{
    GameObject player;
    Animator animator;
    PlayerMovement playerMovement;
    [SerializeField]
    float currentMovespeed;
    [SerializeField] float damage;
    // Start is called before the first frame update

    private float slowTimer = 0;
    private bool slow;
    private float slowDuration = 2;
    private float normalSpeed;
    void Start()
    {
        player = GameObject.Find("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
        Physics.IgnoreLayerCollision(8, 7, true);

    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {


            Vector2 movingLocation;
            Vector2 playerPosition = player.transform.position;
            Vector2 playerDirection = playerMovement.getDirection();
            float distance = Vector2.Distance(transform.position, player.transform.position);
            if (playerDirection != Vector2.zero && Mathf.Abs(distance) >= 1)
            {
                movingLocation = playerPosition + 1 * playerDirection;
            }
            else
            {
                movingLocation = playerPosition;
            }

            transform.position = Vector2.MoveTowards(transform.position, movingLocation, currentMovespeed * Time.deltaTime);
            Vector2 movingDirection = (movingLocation - (Vector2)transform.position).normalized;

            animator.SetFloat("xDir", movingDirection.x);
            animator.SetFloat("yDir", movingDirection.y);
        }


    }
    public void FixedUpdate()
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerLogic playerObject = collision.gameObject.GetComponent(typeof(PlayerLogic)) as PlayerLogic;

        if (collision.gameObject.CompareTag("Player"))
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

}
