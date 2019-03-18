using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Stats")]
    public float moveSpeed = 10f;
    public float jumpStrength = 10f;
    public float punchStrength = 10f;

    // Movement Related Stuff.
    private float moveHori;
    private float moveVert;
    private Vector3 movement;

    // Components
    private Rigidbody rb;
    private Animator anim;

    // Boolean flags that represent the current state of the player.
    //    - isGrounded: Should be true whenever the player is on the ground.
    //    - isJumping: Should be true whenever the player is jumping. Note,
    //         this should only be true while the player is ASCENDING.
    //    - isFalling: Should be true whenever the player begins to fall
    //         after reaching the apex of their jump.
    //    - isPunching: Should be true whenever the player presses the punch
    //         button (either B on an Xbox Con. or LMB)
    private bool isGrounded;
    private bool isJumping;
    private bool isFalling;
    private bool isPunching;

    // Misc. Stuff.
    private float distToGround;
    private CapsuleCollider playerCollider;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        distToGround = GetComponent<CapsuleCollider>().bounds.extents.y;
        playerCollider = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log(anim.GetCurrentAnimatorClipInfo(0)[0].clip.name);

        checkGrounded();
        checkFalling();
        Move();
        Jump();
        Punch();
    }

    // Private Methods
    private void Move() {
        if (!isPunching)
        {
            moveHori = Input.GetAxis("Horizontal");
            moveVert = Input.GetAxis("Vertical");
            movement = new Vector3(moveHori, 0f, moveVert);

            if (movement.magnitude > 1f) { movement.Normalize(); }

            if (moveHori != 0f || moveVert != 0f)
            {
                Quaternion rotation = Quaternion.LookRotation(movement);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 10f * Time.deltaTime);
            }

            // This line of code makes the player slowly slide off platforms if they are on the edge of one.
            if (isFalling) { movement += -(Vector3.up * .1f); }
            rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);

            anim.SetFloat("blendSpeed", movement.magnitude);
        }
    }

    private void Jump() {
        if (!isJumping && isGrounded && Input.GetAxis("Jump") == 1)
        {
            isJumping = true;
            isGrounded = false;
            rb.velocity += Vector3.up * jumpStrength;

            anim.SetBool("animJump", true);
            anim.SetBool("animFalling", false);
        }
    }

    private void Punch() {
        if (isGrounded && Input.GetAxis("Fire1") == 1)
        {
            isPunching = true;
            anim.SetBool("animPunch", true);
        }
        if (isPunching && anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Armature|Punch")
        {
            isPunching = false;
            anim.SetBool("animPunch", false);
        }
    }

    private void checkGrounded() {
        if (Physics.Raycast(playerCollider.bounds.center, -Vector3.up, distToGround + 0.01f))
        {
            isGrounded = true;
            isJumping = false;
            isFalling = false;

            anim.SetBool("animJump", false);
            anim.SetBool("animFalling", false);
        }
        else
            isGrounded = false; 
    }
    private void checkFalling() {
        if (rb.velocity.y < -.01f)
        {
            isFalling = true;
            isJumping = false;

            anim.SetBool("animFalling", true);
            anim.SetBool("animJump", false);
        }
    }
}
