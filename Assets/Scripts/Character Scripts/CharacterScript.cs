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

    // Use this for initialization
    void Start()
    {
        if (!isLocalPlayer)
        {
            GetComponentInChildren<Camera>().transform.gameObject.SetActive(false);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            rigby = GetComponent<Rigidbody>();
            collider = GetComponent<Collider>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {
            //Disables the annoying cursor lock
            if (Input.GetKeyDown("escape"))
                Cursor.lockState = CursorLockMode.None;
            Jump();
        }
    }

    void FixedUpdate()
    {
        if (isLocalPlayer)
        {
            //Executes all movements determined by active axis and currenty jump value 
            rigby.AddRelativeForce(new Vector3(
                   /* X */     Input.GetAxisRaw("Horizontal Movement") * movementSpeed,
                   /* Y */     Mathf.Max((curJumpPower), -maxFallSpeed),
               /* Z */     Input.GetAxisRaw("Vertical Movement") * movementSpeed)
                 /* All */   * 100 * Time.deltaTime, ForceMode.Force);
        }
    }

    void Jump()
    {

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