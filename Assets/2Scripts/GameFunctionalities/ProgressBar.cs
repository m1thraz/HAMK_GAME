using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class ProgressBar : MonoBehaviour
{
    private Slider slider;
    public TextMeshProUGUI levelText;
    private int level = 1;

    private float targetProgress  = 0;
    [SerializeField]
    private float fillSpeed = 10f;

    PowerUpMenu powerupMenu;

    private void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        powerupMenu = GameObject.FindGameObjectWithTag("GameManager").GetComponent(typeof(PowerUpMenu)) as PowerUpMenu;
    }

    // Update is called once per frame
    void Update()
    {
        // Debug function
        if (Input.GetKey(KeyCode.L))
        {
            increaseLevel(100);
            //Debug.Log("slider.value " + slider.value + " slidermax " + slider.maxValue);
        }

        if (slider.value < targetProgress)
        {
            slider.value += fillSpeed * Time.deltaTime;

         
        }
        
            

        if(slider.value == slider.maxValue)
        {
            Debug.Log("Level UP");
            slider.value = 0;
            slider.maxValue = slider.maxValue + 20;
            targetProgress = 0;
            levelUp();


        }
    }

    public void increaseLevel(float newProgress)
    {
        targetProgress =  slider.value + newProgress;

    }

    private void levelUp()
    {
        level++;
        levelText.text = "Level: " + level.ToString();
        powerupMenu.openPowerUP();
    }

}
