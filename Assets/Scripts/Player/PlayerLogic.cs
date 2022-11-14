using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class PlayerLogic : MonoBehaviour
{
    public TextMeshProUGUI HPText;
    public TextMeshProUGUI ScoreText;
    public ProgressBar lvlbar;
    float DeathTime = 1;
    bool alive = true;
    // Start is called before the first frame update
    [SerializeField] 
    float maxHealth = 3, health, score;


    private void Awake()
    {
        lvlbar = GameObject.Find("Progress Bar").GetComponent<ProgressBar>();
    }

    void Start()
    {
        health = maxHealth;
        Physics.IgnoreLayerCollision(0, 6);
        HPText.text = health.ToString() + "x";
        ScoreText.text = "Score: " + score.ToString();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (health <= 0)
        {

            Death();

        }
    }

    public void takeDamage(float damageAmount)
    {
        
        health -= damageAmount;
        HPText.text = health.ToString() + "x";
        Debug.Log(string.Format("Player has now {0} health left", health));
        
        if(health > 0)
        {
            FindObjectOfType<AudoManager>().Play("player hit");
        }
        

    }

    public void ScoreUp()
    {
        score +=  10;
        ScoreText.text = "Score: " + score.ToString();
        lvlbar.increaseLevel(10);
    }

    private void Death()
    {
        DeathTime -= Time.deltaTime;
        if (DeathTime > 0 && alive)
        {
            
            FindObjectOfType<AudoManager>().Play("player death");
            FindObjectOfType<Animator>().SetLayerWeight(2, 2);
            Debug.Log("Player DIED");
            alive = false;
        }
        if(DeathTime<=0)
        {

            Destroy(gameObject);
            SceneManager.LoadScene(2);
        }
        
    }
}
