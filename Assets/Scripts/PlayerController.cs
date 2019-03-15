﻿using System.Collections;
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
    private bool isGrounded;
    private bool isJumping;
    private bool isFalling;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    // Private Methods
    private void Move() {
        moveHori = Input.GetAxis("Horizontal");
        moveVert = Input.GetAxis("Vertical");

        movement = new Vector3(moveHori, 0f, moveVert);
        if (movement.magnitude > 1f) { movement.Normalize(); }

        if (moveHori != 0f || moveVert != 0f)
        {

            Quaternion rotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 10f * Time.deltaTime);
        }

        anim.SetFloat("blendSpeed", movement.magnitude);
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
    }
}
