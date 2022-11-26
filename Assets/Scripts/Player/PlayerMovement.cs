using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Vector2 direction;
    private Vector2 directionHistory = Vector2.right;
    private Animator animator;

    public float shootSpeed, shootTimer;
    public Transform shootPosNorth;
    public Transform shootPosEast;
    public Transform shootPosWest;
    public Transform shootPosSouth;



    public GameObject bullet;
    private bool isShooting;
    public GameObject gameManager;
    bool isPowerMenuOpen;
    PowerUpMenu powerMenu;
    public GameObject pauseMenu;
    bool gamePaused = false;

    public bool isDoubleCast = false;



    private void Start()
    {
        isShooting = false;
        animator = GetComponent<Animator>();
        powerMenu = gameManager.GetComponent(typeof(PowerUpMenu)) as PowerUpMenu;
        isPowerMenuOpen = false;
    }
    // Update is called once per frame
    void Update()
    {
        TakeInput();
        Move();
       
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
        if (Input.GetKey(KeyCode.Space) && !isShooting || Input.GetKey(KeyCode.Mouse0) && !isShooting)
        {
           // if (!gamePaused)
          //  {
                //maybe adjust duration between?
                if (isDoubleCast)
                {
                    StartCoroutine(playerShoot());
                    StartCoroutine(playerShoot());
                }
                else
                {
                    StartCoroutine(playerShoot());
                }

          //  }
        }
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(1);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!gamePaused)
            {
                Time.timeScale = 0f;
                pauseMenu.SetActive(true);
                gamePaused = true;
            } else
            {
                Time.timeScale = 1f;
                pauseMenu.SetActive(false);
                gamePaused = false;
            }

        }

        if (direction != Vector2.zero)
        {
            directionHistory = direction;
            //Debug.Log(directionHistory);
        }
        

        // For Debugging POWER UP MENU
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("key p pressed");

            if (isPowerMenuOpen)
            {
                Debug.Log("resume game and close powerup");
                powerMenu.closePowerUP();
                //powerMenu.pauselog();
                isPowerMenuOpen = false;
            }
            else
            {
                Debug.Log("pause game and open powerup");
                powerMenu.openPowerUP();
                isPowerMenuOpen = true;
                //powerMenu.pauselog();


            }
        }

    }
    private void SetAnimatorMovement(Vector2 direction)
    {
        animator.SetLayerWeight(1, 1);
        animator.SetFloat("xDir", directionHistory.x);
        animator.SetFloat("yDir", directionHistory.y);
        
    }

    IEnumerator playerShoot()
    {
        isShooting = true;
        GameObject newBullet;

        //directions
        Vector2 rightUp = new Vector2(1.0f, 1.0f);
        Vector2 rightDown = new Vector2(1.0f, -1.0f);
        Vector2 leftUp = new Vector2(-1.0f, 1.0f);
        Vector2 leftDown = new Vector2(-1.0f, -1.0f);



        if (directionHistory == Vector2.up)
        {
             newBullet = Instantiate(bullet, shootPosNorth.position, Quaternion.identity);
        }
        else if (directionHistory == Vector2.right)
        {
             newBullet = Instantiate(bullet, shootPosEast.position, Quaternion.identity);
        }
        else if (directionHistory == Vector2.left)
        {
            newBullet = Instantiate(bullet, shootPosWest.position, Quaternion.identity);
        }
        else
        {
            newBullet = Instantiate(bullet, shootPosSouth.position, Quaternion.identity);
        }



        if (directionHistory == rightUp)
        {
            newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, shootSpeed * Time.fixedDeltaTime);
        }
        else if (directionHistory == rightDown)
        {
            newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -shootSpeed * Time.fixedDeltaTime);
        }
        else if (directionHistory == leftUp)
        {
            newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, shootSpeed * Time.fixedDeltaTime);
        }
        else if (directionHistory == leftDown)
        {
            newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -shootSpeed * Time.fixedDeltaTime);
        }

        else if (directionHistory == Vector2.up)
        {
            newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, shootSpeed * Time.fixedDeltaTime);
        }

        else if (directionHistory == Vector2.down)
        {
            newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -shootSpeed * Time.fixedDeltaTime);
        }

        else if (directionHistory == Vector2.right)
        {
            newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(shootSpeed * Time.fixedDeltaTime, 0f);
        }

        else if (directionHistory == Vector2.left)
        {
            newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-shootSpeed * Time.fixedDeltaTime, 0f);
        }


        yield return new WaitForSeconds(shootTimer);
        isShooting = false;
    }


    public void increaseSpeed()
    {
        speed *= 1.1f;
    }

    public void increaseShootSpeed()
    {
        shootTimer *= 0.9f;
    }

    public void activateDoubleCast()
    {
        isDoubleCast = true;
    }
}
