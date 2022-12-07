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


    public GameObject scroll1;
    public GameObject scrollOpen;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void bossSpawn()
    {
        bigCam();
        Boss.SetActive(true);

    }

    public void startStoryline()
    {
        StartCoroutine(beginStory());
    }


    IEnumerator beginStory()
    {
        
        yield return new WaitForSeconds(2);

        StartCoroutine(vanish());
        

    }


    public void openScroll()
    {
        scroll1.SetActive(false);
        scrollOpen.SetActive(true);
    }


    IEnumerator vanish()
    {
        Boss.SetActive(false);
        Son.SetActive(false);
        yield return new WaitForSeconds(2);
        difficultyMenu.SetActive(true);
    }


    public void bigCam()
    {
        cam = camObject.GetComponent<Camera>();
        cam.orthographicSize = 11;
    }
}
