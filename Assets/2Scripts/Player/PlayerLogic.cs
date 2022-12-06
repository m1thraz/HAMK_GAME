using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class PlayerLogic : MonoBehaviour
{
    public TextMeshProUGUI HPText;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI ArmorText;
    public TextMeshProUGUI CoinText;
    public ProgressBar lvlbar;


    float DeathTime = 1;
    bool alive = true;
    // Start is called before the first frame update
    [SerializeField] 
    float maxHealth = 3, health, score;
    [SerializeField]
    int armor = 0, coins = 0;

    private void Awake()
    {
        lvlbar = GameObject.Find("Progress Bar").GetComponent<ProgressBar>();
    }

    void Start()
    {
        health = maxHealth;
       // Physics.IgnoreLayerCollision(0, 6);
        HPText.text = health.ToString() + "/" + maxHealth.ToString();
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

        while(armor > 0 && damageAmount != 0)
        {
            armor--;
            damageAmount--;
            ArmorText.text = armor.ToString();
        }

        if(damageAmount == 0)
        {
            //play block sound?
            return;
        }

        health -= damageAmount;
        

        HPText.text = health.ToString() + "/" + maxHealth.ToString();

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
            PlayerPrefs.SetFloat("playScore", score);
            Destroy(gameObject);
            SceneManager.LoadScene(2);
        }
        
    }

    public void increaseMaxHP()
    {
        maxHealth++;
        HPText.text = health.ToString() + "/" + maxHealth.ToString();
    }

    public void increaseArmor(int amount)
    {
        armor += amount;
        ArmorText.text = armor.ToString(); 

    }

    public void increasePlayerHealth()
    {
        health = maxHealth;
        HPText.text = health.ToString() + "/" + maxHealth.ToString();
    }

    public void increaseCoin(int amount)
    {
        coins += amount;
        CoinText.text = coins.ToString();
    }

    public int getCoin()
    {
        return coins;
    }

    public int getArmor()
    {
        return armor;
    }

    public float getScore()
    {
        return score;
    }
}