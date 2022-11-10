using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private Slider slider;


    private float targetProgress  = 0;
    [SerializeField]
    private float fillSpeed = 10f;



    private void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Debug function
        if (Input.GetKey(KeyCode.L))
        {
            increaseLevel(50);
            Debug.Log("slider.value " + slider.value + " slidermax " + slider.maxValue);
        }

        if (slider.value < targetProgress)
        {
            slider.value += fillSpeed * Time.deltaTime;

         
        }
        
            

        if(slider.value == slider.maxValue)
        {
            Debug.Log("Level UP");
            slider.value = 0;
            slider.maxValue = slider.maxValue + 50;
            targetProgress = 0;

        }
    }

    public void increaseLevel(float newProgress)
    {
        targetProgress =  slider.value + newProgress;

    }


}
