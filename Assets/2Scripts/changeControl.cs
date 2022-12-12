using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class changeControl : MonoBehaviour
{
    int status;
    public Toggle toggle;
    public GameObject Controls1Text;
    public GameObject Controls2Text;

    private void Awake()
    {
     
        if (PlayerPrefs.HasKey("shootcontrol"))
        {
            status = PlayerPrefs.GetInt("shootcontrol");


            if (status == 0)
            {
                toggle.isOn = false;  
            } else
            {
                toggle.isOn = true;
            }
        }
        else
        {
            PlayerPrefs.SetInt("shootcontrol", 0);
        }
       
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void toggleControl()
    {
        if (PlayerPrefs.HasKey("shootcontrol"))
        {
            status = PlayerPrefs.GetInt("shootcontrol");


            if (status == 0)
            {
                PlayerPrefs.SetInt("shootcontrol",1);
                Controls1Text.SetActive(false);
                Controls2Text.SetActive(true);
            }
            else
            {
                PlayerPrefs.SetInt("shootcontrol",0);
                Controls1Text.SetActive(true);
                Controls2Text.SetActive(false);
            }
        }

    }
}
