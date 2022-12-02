using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int health = 100;

    private int MAX_HEALTH = 100;

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {

        //teststatement to test healing
        if (Input.GetKeyDown(KeyCode.H))
        {
            Heal(10);
        }
        //teststatement to test damage
        if (Input.GetKeyDown(KeyCode.J))
        {
            Damage(10);
        }
    }
 
    public void SetHealth(int maxHealth, int health)
    {
        this.MAX_HEALTH = maxHealth;
        this.health = health;
    }
    public void Damage(int amount)
    {
        if (amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("Negative damage not possible");
        }
        this.health -= amount;
        if (health <=0 )
        {
            Die();
        }
    }
    public void Heal(int amount)
    {
        if (amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("negative healing not possible");
        }
        bool wouldBeOverMaxHealth = health + amount > MAX_HEALTH;
        if (wouldBeOverMaxHealth)
        {
            this.health = MAX_HEALTH;
        }
        else
        {
            this.health += amount;
        }
    }

    private void Die()
    {
        Debug.Log("you're dead");
        Destroy(gameObject);
    }    
}
