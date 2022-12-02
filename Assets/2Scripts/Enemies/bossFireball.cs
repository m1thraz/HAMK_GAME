using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossFireball : MonoBehaviour
{
    [SerializeField]
    float damage;
    [SerializeField]
    float projectileSpeed;
    Rigidbody2D rb;
    float angle = 0;
    Transform player;

    public Vector2 moveDirection;

    private bool follow = false;
    // Start is called before the first frame update
    void Start()
    {
    
        Physics.IgnoreLayerCollision(7, 7);
        Physics.IgnoreLayerCollision(8, 7);
        Physics.IgnoreLayerCollision(7, 8);
        Physics.IgnoreLayerCollision(8, 10);
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").transform;
        int rand = Random.Range(1, 11);
        if(rand >= 8)
        {
            follow = true;
        }
        else
        {
            follow = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction;
        if (follow )
        {
            direction = (player.position - transform.position).normalized;
            moveDirection = direction;

        } else
        {
            direction = moveDirection;

        }
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle + 180;



    }
    private void FixedUpdate()
    {
        //Debug.Log(string.Format("shot direction = {0}", moveDirection));
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * (projectileSpeed * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerLogic playerObject = collision.gameObject.GetComponent(typeof(PlayerLogic)) as PlayerLogic;

            //Debug.Log(string.Format("player HIT, taking {0} damage", damage));
            Destroy(gameObject);

            playerObject.takeDamage(damage);

        }

    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
