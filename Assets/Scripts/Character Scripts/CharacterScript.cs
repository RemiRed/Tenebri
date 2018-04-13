using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CharacterScript : NetworkBehaviour
{
    [SerializeField]
    float movementSpeed = 32;
    [SerializeField]
    float jumpSpeed = 55;
    [SerializeField]
    float maxJumpHeight = 1.85f;
    [SerializeField]
    float gravity = 120;
    [SerializeField]
    float maxFallSpeed = 200;

    float jumpStartY;
    float curJumpPower;

    Rigidbody rigby;
    Collider collider;

    bool grounded;
    bool hitCeiling;
    bool jumping;

    public bool menu = false;
    GameObject pauseMenu;

    AudioSource footsteps;
    void Start()
    {
        if (!isLocalPlayer)
        {
            GetComponentInChildren<Camera>().transform.gameObject.SetActive(false);
        }
        else
        {
            footsteps = GetComponent<AudioSource>();
            Cursor.lockState = CursorLockMode.Locked;
            rigby = GetComponent<Rigidbody>();
            collider = GetComponent<Collider>();
            pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
            pauseMenu.SetActive(false);
        }

    }

    public void ToggleMenu()
    {
        menu = !menu;
        if (menu)
        {
            Cursor.lockState = CursorLockMode.None;
            pauseMenu.SetActive(true);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            pauseMenu.SetActive(false);
        }
    }

    void Update()
    {
      
        if (isLocalPlayer)
        {
            //Opens Menu and disables the clocked cursor
            if (Input.GetButtonDown("Menu"))
            {
                pauseMenu.GetComponent<PauseMenu>().character = this;
                ToggleMenu();
            }
            Jump();
        }
    }

    void FixedUpdate()
    {
        if (menu)
        {
            return;
        }
        if (isLocalPlayer)
        {
            //Executes all movements determined by active axis and currenty jump value 
            rigby.AddRelativeForce(new Vector3(
                   /* X */     Input.GetAxisRaw("Horizontal Movement") * movementSpeed,
                   /* Y */     Mathf.Max((curJumpPower), -maxFallSpeed),
                   /* Z */     Input.GetAxisRaw("Vertical Movement") * movementSpeed)
                  /* All */   * 100 * Time.deltaTime, ForceMode.Force);
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

    void Jump()
    {
      if (menu)
        {
            return;
        }
        if (IsGrounded())
        {

            if (Input.GetButtonDown("Jump"))
            {

                //Initiates player jump after jump button is pressed
                curJumpPower = jumpSpeed;
                jumping = true;

                //Sets base for calculating when jump reaches maximum jump height
                jumpStartY = transform.position.y;

            }
            else if (!jumping)
            {

                //Sets jump value to a neutral value as long as the player is grounded and not jumping
                curJumpPower = 0;
            }

        }
        else
        {

            //Increases fall speed while not grounded
            curJumpPower -= gravity * Time.deltaTime;
        }

        if (jumping)
        {

            //Cancels jump if jump is interupted or reaches jumping limit. 
            if (Input.GetButtonUp("Jump") || transform.position.y >= jumpStartY + maxJumpHeight || HitCeiling())
            {

                jumping = false;
                curJumpPower = Mathf.Min(curJumpPower, 0);
            }
        }
    }

    bool IsGrounded()
    {
        //Runs Raycasts to determine if grounded or not. Uses a triad of raycasts for smoother edge detection.
        if (Physics.Raycast(transform.position + new Vector3(0, 0, collider.bounds.extents.x / 1.5f), -Vector3.up, collider.bounds.extents.y + 0.05f))
        {
            grounded = true;
        }
        else if (Physics.Raycast(transform.position + new Vector3(0, 0, -collider.bounds.extents.x / 1.5f), -Vector3.up, collider.bounds.extents.y + 0.05f))
        {
            grounded = true;
        }
        else if (Physics.Raycast(transform.position + new Vector3(collider.bounds.extents.x / 1.5f, 0, 0), -Vector3.up, collider.bounds.extents.y + 0.05f))
        {
            grounded = true;
        }
        else if (Physics.Raycast(transform.position + new Vector3(-collider.bounds.extents.x / 1.5f, 0, 0), -Vector3.up, collider.bounds.extents.y + 0.05f))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
        return grounded;
    }

    bool HitCeiling()
    {
        //Runs Raycasts to determine if htting a ceiling or not. Uses a triad of raycasts for smoother edge detection.
        if (Physics.Raycast(transform.position + new Vector3(0, 0, collider.bounds.extents.x / 1.5f), Vector3.up, collider.bounds.extents.y + 0.05f))
        {
            hitCeiling = true;
        }
        else if (Physics.Raycast(transform.position + new Vector3(0, 0, -collider.bounds.extents.x / 1.5f), Vector3.up, collider.bounds.extents.y + 0.05f))
        {
            hitCeiling = true;
        }
        else if (Physics.Raycast(transform.position + new Vector3(collider.bounds.extents.x / 1.5f, 0, 0), Vector3.up, collider.bounds.extents.y + 0.05f))
        {
            hitCeiling = true;
        }
        else if (Physics.Raycast(transform.position + new Vector3(-collider.bounds.extents.x / 1.5f, 0, 0), Vector3.up, collider.bounds.extents.y + 0.05f))
        {
            hitCeiling = true;
        }
        else
        {
            hitCeiling = false;
        }
        return hitCeiling;
    }
}
