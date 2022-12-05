using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
//We might change the language in this script, so let's use the localization
using RPGTALK.Localization;

public class MovePlayer : MonoBehaviour {
	//The speed that our 'hero' will run
	public float speed = 10f;

	
	Rigidbody2D rigid;
	Animator animator;
	SpriteRenderer render;
    private Vector2 direction;
    private Vector2 directionHistory = Vector2.right;

   
    public bool controls;

	//We will sometimes initialize the talk by script, so let's keep a instance of the current RPGTalk
	public RPGTalk rpgTalk;

	//A canvas that will be shown asking for the player's name
	public GameObject askWho;
	//The input that the player should write its name
	public InputField myName;

	//A wall to desappear and a particle to play when that happens
	public GameObject wall;
	public GameObject particle;

    //We want to specify callbacks to different parts of the conversation
    public UnityEvent OnIKnowYou, OnByWall;



	// Get the right references...
	void Start () {
		rigid = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
		render = GetComponent<SpriteRenderer> ();
		//In the tagsDemo scene, we want to do something when we make a choice...
		//rpgTalk.OnMadeChoice += OnMadeChoice;
	}

	// Update is called once per frame
	void Update () {

		//skip the Talk to the end if the player hit Return
		if(Input.GetKeyDown(KeyCode.Return)){
			rpgTalk.EndTalk ();
		}


		TakeInput();
		Move();


    }

    private void SetAnimatorMovement(Vector2 direction)
    {
        animator.SetLayerWeight(1, 1);
        animator.SetFloat("xDir", directionHistory.x);
        animator.SetFloat("yDir", directionHistory.y);

    }


    private void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        if (direction.x != 0 || direction.y != 0)
        {

            SetAnimatorMovement(direction);

        }
        else
        {
            animator.SetLayerWeight(1, 0);
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

            //Pause();
        }

        if (direction != Vector2.zero)
        {
            directionHistory = direction;
            //Debug.Log(directionHistory);
        }

    }



    //the player cant move
    public void CancelControls(){
		controls = false;
	}

	//give back the controls to player
	public void GiveBackControls(){
		controls = true;
	}

	//Open the screen to enter Player's name
	public void WhoAreYou(){
		askWho.SetActive(true);
		myName.Select ();
	}

	//This callback will be called by RPGTalk after the first talk ends with the "FunnyGuy"
	//Here, we will change the value of a variable in RPGTalk to be the name of the player
	//And then, we will start a new talk =D
	public void IKnowYouNow(){
		askWho.SetActive (false);
		rpgTalk.variables [0].variableValue = myName.text;
		rpgTalk.NewTalk ("17", "25", rpgTalk.txtToParse, OnIKnowYou);
	}

	//Let's get rid of that wall. This function was called by RPGTalk bacause the function above
	//setted it to be its callback.
	public void ByeWall(){
		wall.SetActive (false);
		particle.SetActive (true);
		Invoke ("FunnyGuyEnd", 2f);
	}

	//After the wall exploded, let the Funny Guy end his talking
	void FunnyGuyEnd(){
		rpgTalk.NewTalk ("26", "29", rpgTalk.txtToParse, OnByWall);
	}

	
	
}
