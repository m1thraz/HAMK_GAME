using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerLogic : MonoBehaviour
{
    public TextMeshProUGUI HPText;
    public TextMeshProUGUI ScoreText;
    public ProgressBar lvlbar;
    // Start is called before the first frame update
    [SerializeField] float maxHealth = 3, health, score;

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
    void Update()
    {
        
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
        
        if (health <= 0)
        {

            FindObjectOfType<AudoManager>().Play("player death");
           
            Debug.Log("Player DIED");
            Destroy(gameObject);
            SceneManager.LoadScene(2);
        }
    }

    public void ScoreUp()
    {
        
        score +=  10;
        ScoreText.text = "Score: " + score.ToString();
        lvlbar.increaseLevel(10);
    }
}
