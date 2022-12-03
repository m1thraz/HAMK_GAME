using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovementCut : MonoBehaviour
{
    public float speed;
    private Vector2 direction;
    private Vector2 directionHistory = Vector2.right;
    private Animator animator;

    

    [SerializeField]
    private bool inCutScene = false;

    public GameObject pauseMenu;
    bool gamePaused = false;
    public GameObject settingsMenu;

    public RPGTalk rpgTalk;

    public GameObject askWho;
    public GameObject askWhoSon;

    public InputField myName;
    public InputField sonName;



    private void Start()
    {
      
        animator = GetComponent<Animator>();

    }
    // Update is called once per frame
    void Update()
    {
        TakeInput();
        Move();
    }


    public void WhoAreYou()
    {
        askWho.SetActive(true);
        myName.Select();
    }

    public void WhoIsSon()
    {
        askWhoSon.SetActive(true);
        sonName.Select();
    }

    public void IKnowYourName()
    {
        askWho.SetActive(false);
        rpgTalk.variables[0].variableValue = myName.text;
        rpgTalk.variables[0].variableValue = myName.text;

        //rpgTalk.NewTalk("17", "25", rpgTalk.txtToParse, OnIKnowYou);
    }




    private void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        if(direction.x != 0 || direction.y != 0) { 
      
        SetAnimatorMovement(direction);

        } else
        {
            animator.SetLayerWeight( 1, 0);
        }
    }
    private void TakeInput()
    {
        direction = Vector2.zero;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            direction += Vector2.up;

        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            direction += Vector2.left;

        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            direction += Vector2.down;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            direction += Vector2.right;

        }



        if (Input.GetKeyDown(KeyCode.Escape))
        {

            Pause();
        }

        if (direction != Vector2.zero)
        {
            directionHistory = direction;
            //Debug.Log(directionHistory);
        }
        
    }

  

    private void SetAnimatorMovement(Vector2 direction)
    {
        animator.SetLayerWeight(1, 1);
        animator.SetFloat("xDir", directionHistory.x);
        animator.SetFloat("yDir", directionHistory.y);
        
    }

  

      
   
    public Vector2 getDirection()
    {
        return direction.normalized;
    }



    public void Pause()
    {
        if (!gamePaused)
        {
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
            gamePaused = true;
        }
        else
        {
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
            gamePaused = false;
            settingsMenu.SetActive(false);
        }
    }

}
