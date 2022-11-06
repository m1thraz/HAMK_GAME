using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float maxHealth = 3, health;
    void Start()
    {
        health = maxHealth;
        Physics.IgnoreLayerCollision(0, 6);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(float damageAmount)
    {
        health -= damageAmount;
        Debug.Log(string.Format("Player has now {0} health left", health));
        if (health <= 0)
        {
            Debug.Log("Player DIED");
            Destroy(gameObject);
        }
    }
}
