using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Debug Stuff")]
    [Tooltip("Want to control when you see certain debug logs? Check this option here.")]
    public bool enableDebugLog = false;
    [Tooltip("Want to see the exact hitbox of the player's punch? Check this option here.")]
    public bool visualizeHitbox = false;

    [Header("Player Stats")]
    public float moveSpeed = 10f;
    public float jumpStrength = 10f;
    public float punchStrength = 10f;

    [Header("Punch Hitbox Info")]
    public SphereCollider hitbox;
    public MeshRenderer hbRend;

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
    private bool canJump;

    private Vector3 startPosition;

    // Misc. Stuff.
    private float distToGround;
    private CapsuleCollider playerCollider;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        distToGround = GetComponent<CapsuleCollider>().bounds.extents.y;
        playerCollider = GetComponent<CapsuleCollider>();
        if (hbRend.enabled == true) { hbRend.enabled = false; }
        hitbox.enabled = false;
        startPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        checkCanJump();
        checkFalling();
        checkGrounded();
        checkDeath();

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

            Vector3 camForward = Camera.main.transform.forward;
            Vector3 camRight = Camera.main.transform.right;
            camForward.y = 0f;
            camRight.y = 0f;
            camForward.Normalize();
            camRight.Normalize();

            movement = camForward * moveVert + camRight * moveHori;
            movement.Normalize();
            

            if (movement.magnitude != 0f)
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


    private void checkDeath()
    {
        if(transform.position.y < -20f)
        {
            transform.position = startPosition;
        }
    }

    private void Jump() {
        if (!isJumping && canJump && isGrounded && !isPunching && Input.GetAxis("Jump") == 1)
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
            if (visualizeHitbox) { hbRend.enabled = true; }
            hitbox.enabled = true;
            anim.SetBool("animPunch", true);
        }
        if (isPunching) {
            AnimatorClipInfo[] ci_Array = anim.GetCurrentAnimatorClipInfo(0);
            if(ci_Array.Length != 0 && ci_Array[0].clip.name == "Armature|Punch")
            {
                isPunching = false;
                hbRend.enabled = false;
                hitbox.isTrigger = true;
                hitbox.enabled = false;
                anim.SetBool("animPunch", false);
            }
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

    private void checkCanJump()
    {
        if (Input.GetAxis("Jump") == 1 && isJumping){ canJump = false; }
        else if (Input.GetAxis("Jump") == 0 && isGrounded) { canJump = true; }
    }
}
