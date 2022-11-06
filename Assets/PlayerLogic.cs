using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerLogic : MonoBehaviour
{
    public TextMeshProUGUI HPText;
    public TextMeshProUGUI ScoreText;
    // Start is called before the first frame update
    [SerializeField] float maxHealth = 3, health, score;
    void Start()
    {
        health = maxHealth;
        Physics.IgnoreLayerCollision(0, 6);
        HPText.text = "Health: " + health.ToString();
        ScoreText.text = "Score: " + score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(float damageAmount)
    {
        health -= damageAmount;
        HPText.text = "Health: " +health.ToString();
        Debug.Log(string.Format("Player has now {0} health left", health));
        if (health <= 0)
        {
            Debug.Log("Player DIED");
            Destroy(gameObject);
            SceneManager.LoadScene(2);
        }
    }

    public void ScoreUp()
    {
        score +=  10;
        ScoreText.text = "Score: " + score.ToString();
    }
}
