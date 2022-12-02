using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    [SerializeField]
    bool slow;
    [SerializeField]
    float damage;
    Vector2 shotDirection;
    Rigidbody2D rb;
    [SerializeField]
    float angle = 0;

    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreLayerCollision(7, 7);
        Physics.IgnoreLayerCollision(8, 7);
        Physics.IgnoreLayerCollision(7,8);
        rb = GetComponent<Rigidbody2D>();
        shotDirection = rb.velocity;
    }

    // Update is called once per frame
    void Update()
    {
        angle = Mathf.Atan2(shotDirection.y, shotDirection.x) * Mathf.Rad2Deg;
        rb.rotation = angle + 180;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerLogic playerObject = collision.gameObject.GetComponent(typeof(PlayerLogic)) as PlayerLogic;

            if (slow)
            {
                Debug.Log("trying to slow");
            }
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
