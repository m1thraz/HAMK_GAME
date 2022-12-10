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


    [SerializeField]
    private bool inCutScene = false;

    private bool isShooting, isShootingBig;


    public GameObject gameManager;
    bool isPowerMenuOpen;
    PowerUpMenu powerMenu;
    public GameObject pauseMenu;
    bool gamePaused = false;
    public GameObject settingsMenu;

    public GameObject bigBullet;
    public GameObject bigBulletFreeze;

    public bool isBiggerSpell = false;
    public int biggerSpellCount = 0;

    public bool isBiggerSpellFreeze = false;
    public int biggerSpellFreezeCount = 0;


    public float dashSpeed;
    private float dashCount;
    public float startDashCount;
    public float dashTimer;
    public bool isPlayerDashing;
    public Rigidbody2D rb;




    private float slowTimer = 0;
    private bool slow;
    private float slowDuration = 2;
    private float normalSpeed;




    private void Start()
    {
        isShooting = false;
        animator = GetComponent<Animator>();
        powerMenu = gameManager.GetComponent(typeof(PowerUpMenu)) as PowerUpMenu;
        isPowerMenuOpen = false;
        rb = GetComponent<Rigidbody2D>() as Rigidbody2D;
        dashCount = startDashCount;

    }
    // Update is called once per frame
    void Update()
    {
        TakeInput();
        Move();

        if (Input.GetKeyDown(KeyCode.B))
        {
            activateBigSpell();
            Debug.Log("Bigspell++ " + biggerSpellCount);
        }
        if(slow)
        {
            slowTimer += Time.deltaTime;
            if(slowTimer >= slowDuration)
            {
                slowTimer = 0;
                slow = false;
                speed = normalSpeed;
            }
        }

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
    public void slowMovement()
    {
        if (slow)
        {
            slowTimer = 0;
        } else
        {
            normalSpeed = speed;
            float slowedSpeed = 0.5f * speed;

            slow = true;
            speed = slowedSpeed;
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


        if (!inCutScene) { 
        // Shooting
        if (!isShooting && Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (!gamePaused)
            {

                StartCoroutine(AlternativShot());
            }
        }


        if (!isShootingBig && Input.GetKeyDown(KeyCode.Mouse1) && biggerSpellFreezeCount > 0)
        {
            if (!gamePaused)
            {

                StartCoroutine(playerShoot(1));
                biggerSpellFreezeCount--;
            }


        } 

        else if (!isShootingBig && Input.GetKeyDown(KeyCode.Mouse1) && biggerSpellCount > 0)
        {
            if (!gamePaused)
            {

                StartCoroutine(playerShoot(0));
                biggerSpellCount--;
            }


        }


    }

        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(1);
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


        if (Input.GetKeyDown(KeyCode.Space) && !isPlayerDashing)
        {
            isPlayerDashing = true;
            StartCoroutine(playerDash());
            
        }



    }

    IEnumerator playerDash()
    {

        // Dashing

        gameObject.layer = LayerMask.NameToLayer("Dash");
       
            if (directionHistory == Vector2.up)
            {
                rb.velocity = new Vector2(0f, 1.0f * dashSpeed);

                yield return new WaitForSeconds(0.25f);
                rb.velocity = Vector2.zero;
                gameObject.layer = LayerMask.NameToLayer("Player");


        

        }
            if (directionHistory == Vector2.right)
            {

            
            rb.velocity = new Vector2(1.0f * dashSpeed, 0f);

                yield return new WaitForSeconds(0.25f);
                rb.velocity = Vector2.zero;
                gameObject.layer = LayerMask.NameToLayer("Player");

           

        }

            if (directionHistory == Vector2.left)
            {

                rb.velocity = new Vector2(-1.0f * dashSpeed, 0f);

                yield return new WaitForSeconds(0.25f);
                rb.velocity = Vector2.zero;
                gameObject.layer = LayerMask.NameToLayer("Player");

        }


            if (directionHistory == Vector2.down)
            {
            rb.velocity = new Vector2(0f, -1.0f * dashSpeed);

            yield return new WaitForSeconds(0.25f);
            rb.velocity = Vector2.zero;
            gameObject.layer = LayerMask.NameToLayer("Player");




    
        }

        yield return new WaitForSeconds(dashTimer);
        isPlayerDashing = false;
    }


    private void SetAnimatorMovement(Vector2 direction)
    {
        animator.SetLayerWeight(1, 1);
        animator.SetFloat("xDir", directionHistory.x);
        animator.SetFloat("yDir", directionHistory.y);
        
    }

    // Big Bullet 0 / freeze Bullet 1
    IEnumerator playerShoot(int mode)
    {
        isShootingBig = true;
        GameObject newBullet;

        //directions
        Vector2 rightUp = new Vector2(1.0f, 1.0f);
        Vector2 rightDown = new Vector2(1.0f, -1.0f);
        Vector2 leftUp = new Vector2(-1.0f, 1.0f);
        Vector2 leftDown = new Vector2(-1.0f, -1.0f);


        if(mode == 0)
        {
            if (directionHistory == Vector2.up)
            {
                newBullet = Instantiate(bigBullet, shootPosNorth.position, Quaternion.identity);
            }
            else if (directionHistory == Vector2.right)
            {
                newBullet = Instantiate(bigBullet, shootPosEast.position, Quaternion.identity);
            }
            else if (directionHistory == Vector2.left)
            {
                newBullet = Instantiate(bigBullet, shootPosWest.position, Quaternion.identity);
            }
            else
            {
                newBullet = Instantiate(bigBullet, shootPosSouth.position, Quaternion.identity);
            }
        } else { // add else if 1 after adding new poweredbullets

            if (directionHistory == Vector2.up)
            {
                newBullet = Instantiate(bigBulletFreeze, shootPosNorth.position, Quaternion.identity);
            }
            else if (directionHistory == Vector2.right)
            {
                newBullet = Instantiate(bigBulletFreeze, shootPosEast.position, Quaternion.identity);
            }
            else if (directionHistory == Vector2.left)
            {
                newBullet = Instantiate(bigBulletFreeze, shootPosWest.position, Quaternion.identity);
            }
            else
            {
                newBullet = Instantiate(bigBulletFreeze, shootPosSouth.position, Quaternion.identity);
            }
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
        isShootingBig = false;
    }
    public Vector2 getDirection()
    {
        return direction.normalized;
    }


    public void increaseSpeed()
    {
        speed *= 1.1f;
    }

    public void increaseShootSpeed()
    {
        shootTimer *= 0.9f;
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
    IEnumerator AlternativShot()
    {
        isShooting = true;
        // GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
        // GameObject spell = Instantiate(bullet, shootPosNorth.position, Quaternion.identity);
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);


            
            Vector2 myPos = transform.position;
            Vector2 direct = (mousePos - myPos).normalized;

            GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);

           // Debug.Log(direct);
            newBullet.GetComponent<Rigidbody2D>().velocity = direct * 5;

            yield return new WaitForSeconds(shootTimer);
            isShooting = false;




    }

    public void activateBigSpell()
    {
         biggerSpellCount += 10;
        

    }

    public void activateBigSpellFreeze()
    {
        biggerSpellFreezeCount += 5;


    }

    public void lowerDashTimer()
    {
        dashTimer *= 0.9f;
    }

}
