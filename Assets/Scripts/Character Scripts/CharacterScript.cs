using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

//Handles all player character movement, as well as some related functions, such as walking audio and the pause menu

//These required components are automatically added to the gameObject when the scripped is added, and can't be removed as long as the script is still attached
[RequireComponent(typeof(Rigidbody))]				//Required for force-bsed movement	(Drag still needs to be manually adjusted from the Default 0 to 10)	
[RequireComponent(typeof(DirectionalCollision))]	//Required for directional collision detection (Such as 'isGrounded')
[RequireComponent(typeof(AudioSource))]				//Required to play walking audio. (Needs to manually add the sound clip)

public class CharacterScript : NetworkBehaviour
{
    [SerializeField]
    float movementSpeed = 32;		//The force applied when walking.	
    [SerializeField]
    float jumpSpeed = 32;           //The force applied at the start of a jump. 
    float curJumpPower;             //Temporary variable that holds the current Y-axis movement

    bool jumping;                   //Bool if character is actively jumping

    float jumpStartY;               //Temporary variable that holds the characters local Y-coordinate at the start of each jump instance 
    [SerializeField]
    float maxJumpHeight = 1.75f;	//A hard restriction how high the player character can jump. If a jump exceededs this height it will be adjusted down to this height. 

    [SerializeField]
    float gravity = 120;			//The downward force applied while the charter isn't grounded
    //[SerializeField]
    float maxFallSpeed = 200;       //The hard-cap how fast the character can fall



    //Character physics
    Rigidbody rigby;
    DirectionalCollision proximityDetection;
    //Walking Audio
    AudioSource footsteps;
    //Menu objects
    GameObject pauseMenu;
    public GameObject gameOverMenu; 

    public bool menu = false, gameOver = false; //Bools that determines if the player can act.
    bool moved = false;
    void Start()
    {
        if (!isLocalPlayer)
        {
            GetComponentInChildren<Camera>().transform.gameObject.SetActive(false);
        }
        else
        {
            //Finds nessesary components 
            rigby = GetComponent<Rigidbody>();
            proximityDetection = GetComponent<DirectionalCollision>();
            footsteps = GetComponent<AudioSource>();
            //Sets the cursor
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;						//<-- Remember to impelment this everywhere we need to fix with the cursor
            //Finds and sets 'pauseMenu' object
            pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
            pauseMenu.SetActive(false);
            //Finds and sets GameOverMenu' Object
            gameOverMenu = GameObject.FindGameObjectWithTag("GameOverMenu");
            gameOverMenu.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {
            if (Input.anyKey)
            {
                moved = true;
            }
            //Toggles menu and the locked cursor
            if (Input.GetButtonDown("Menu") && !gameOver)
            {
                pauseMenu.GetComponent<PauseMenu>().character = this;
                ToggleMenu();
            }
            Jump();            
        }
    }
    //Handles all jumping actions
    void Jump()
    {
        if (menu) return; //Deactivates jumping when a menu is active

        //Checks if player is on the ground
        if (proximityDetection.IsGrounded())
        {
            if (Input.GetButtonDown("Jump"))
            {
                //Initiates player jump after jump button is pressed
                curJumpPower = jumpSpeed;
                jumping = true;

                //Sets base for calculating when jump reaches maximum jump height
                jumpStartY = transform.position.y;
            }
            else if (curJumpPower < 0)
            {
                //Sets jump value to a neutral value as long as the player is grounded and not jumping
                curJumpPower = 0;
                jumping = false;
            }
        }
        else
        {
            //Increases fall speed while not grounded
            curJumpPower -= gravity * Time.deltaTime;
        }

        if (jumping)
        {
            //Cancels the jump if it's interupted or reaches a jumping limit. 
            if (!Input.GetButton("Jump") || proximityDetection.DetectDirectionalCollision(Vector3.up) || transform.position.y >= jumpStartY + maxJumpHeight)
            {
                //Adjusts player position and velocity if exceeding the jump height limit
                if (transform.position.y >= jumpStartY + maxJumpHeight)
                {

                    transform.position = new Vector3(transform.position.x, jumpStartY + maxJumpHeight, transform.position.z);
                    rigby.velocity = Vector3.zero;
                }
                curJumpPower = Mathf.Min(curJumpPower, 0);  //Mathf function to not halt the fall when 'curJumpPower' is already a negative value
                jumping = false;
            }
        }
    }
    //Handles all physics based actions, such as movement, and actions that depend on movement
    void FixedUpdate()
    {
        if (!moved)
        {
            return;
        }
        if (isLocalPlayer && !menu)
        {
            //Executes all movements determined by active axis and current jump value 
            rigby.AddRelativeForce(new Vector3(
                /* X */     Input.GetAxisRaw("Horizontal Movement") * movementSpeed,
                /* Y */     Mathf.Max(curJumpPower, -maxFallSpeed),
                /* Z */     Input.GetAxisRaw("Vertical Movement") * movementSpeed)
                /* All */   * 100 * Time.deltaTime, ForceMode.Force);

            //Handles walking audio
            if (curJumpPower == 0 && (Input.GetAxisRaw("Horizontal Movement") != 0 || Input.GetAxisRaw("Vertical Movement") != 0))
            {
                footsteps.mute = false;
            }
            else
            {
                footsteps.mute = true;
            }
        }
    }
    // Toggles the pause menu and sets the cursor
    public void ToggleMenu()
    {
        menu = !menu;
        if (menu)
        {
            Cursor.lockState = CursorLockMode.None;
            pauseMenu.SetActive(true);
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            pauseMenu.SetActive(false);
            Cursor.visible = false;
        }
    }

    public void GameOver()
    {
        gameOver = true;
        menu = true;
        gameOverMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}