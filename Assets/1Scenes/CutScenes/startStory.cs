using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startStory : MonoBehaviour
{
    [SerializeField]
    GameObject difficultyMenu;
    public GameObject camObject;
    Camera cam;
    public GameObject Boss;
    public GameObject Son;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void startEnum()
    {
        StartCoroutine(beginStory());
    }


    IEnumerator beginStory()
    {
        
        yield return new WaitForSeconds(3);
        StartCoroutine(vanish());
        

    }



    IEnumerator vanish()
    {
        Boss.SetActive(false);
        Son.SetActive(false);
        yield return new WaitForSeconds(5);
        difficultyMenu.SetActive(true);
    }


    public void bigCam()
    {
        Boss.SetActive(true);
        cam = camObject.GetComponent<Camera>();
        cam.orthographicSize = 11;
    }
}
