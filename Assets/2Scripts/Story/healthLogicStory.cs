using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthLogicStory : MonoBehaviour
{
    [SerializeField]
    float maxHealth;
    private float health;
    float currentMovespeed;
    [SerializeField]
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void takeDamage(float dmgAmount)
    {
        PlayerLogic playerObject2 = GameObject.Find("Player").GetComponent(typeof(PlayerLogic)) as PlayerLogic;
        Debug.Log(string.Format("got HIT, taking damage", dmgAmount));
        health -= dmgAmount;
        Debug.Log(string.Format("remaining health {0}  ", health  ));
        if (health <= 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, transform.position, 0);
            currentMovespeed = 0;
            //damage = 0;
            Destroy(GetComponent<Collider2D>());
            animator.Play("die");
            playerObject2.ScoreUp();
            Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
        }

    }
}
