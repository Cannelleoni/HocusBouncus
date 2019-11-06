using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpingNew : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;

    bool isGrounded = false;

    [SerializeField] Transform feetPos;

    [SerializeField] float checkFeetRadius;

    [SerializeField] float checkRayRadius;

    [SerializeField] LayerMask layerGround;

    [SerializeField] float jumpForce;

    [SerializeField] float jumpTimeCounterMax;

    bool jumpCommandRegistered = false;

    float jumpTimeCounter;

    bool countUp = false;

    bool isJumping = false;

    int jumpCounter = 1;

    [SerializeField] SpriteRenderer sr;

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkFeetRadius, layerGround);
        if(isGrounded)
        {
            sr.color = Color.yellow;
        } else
        {
            sr.color = Color.white;
        }

        if(Physics2D.Raycast(transform.position, Vector2.down, checkRayRadius, layerGround))
        {
            print("grounded");
            // register jump command
            if(InputManager.aButtonDown())
            {
                jumpCommandRegistered = true;
            }
            
        }


        if (jumpCommandRegistered && isGrounded)
        {
            //jump
            rb.velocity = Vector2.up * jumpCounter * jumpForce;
            jumpCommandRegistered = false;
            jumpCounter++;
        }

        if (countUp)
        {
            jumpTimeCounter++;
        } 
        if(jumpTimeCounter >= jumpTimeCounterMax)
        {
            countUp = false;
            jumpCounter = 1;
            jumpTimeCounter = 0;
        }
    }

    private void FixedUpdate()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.layer == 8)
        {
            countUp = true;
        }
    }

}
