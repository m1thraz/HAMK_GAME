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
    // Start is called before the first frame update
    void Start()
    {
        currentMovespeed = 3;
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
}
