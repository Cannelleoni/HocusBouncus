using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumping : MonoBehaviour
{
    // The rigidbody2D component of the playr object.
    [SerializeField] Rigidbody2D rb;
    // Boolean to determine whether the player is touching the ground or not.
    private bool isGrounded = true;
    // The position of the player's "feet" to check if they're touching the ground.
    [SerializeField] Transform feetPos;
    // The player's maximum distance from the ground for jumping to be allowed - original.
    [SerializeField] float checkRadiusValue;
    // The player's maximum distance from the ground for jumping to be allowed - current.
    float checkRadius;
    // The layer to check whether the player object is close enough of not. Therefore can differentiate between water and other environments.
    [SerializeField] LayerMask layerGround;
    // The force influencing the jump.
    [SerializeField] float jumpForce;
    // While the counter is active the player can jump higher by keeping the jump button pressed.
    float jumpTimeCounter;
    // The maximum amount of the jumpTimeCounter.
    [SerializeField] float jumpTime;
    //Determines wether the player is currently jumping ot not.
    bool isJumping = false;

    //How often has the player jumped already?
    int jumpCounter = 1;
    int jumpCountMax = 3;

    [SerializeField] SpriteRenderer sr;

    private void Start()
    {
        checkRadius = checkRadiusValue;
    }

    void Update()
    {
        // OverlapCircle return true when the playerCharacter is close enough to a ground layer that the distance <= the radius checkRadius. 
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, layerGround);

        if(isGrounded)
        {
            sr.color = Color.red;
        } else
        {
            sr.color = Color.white;
        }
        

        // The timer to check if new jump command has been issued.
        if (jumpTimeCounter > 0)
        {
            if(isGrounded && InputManager.aButtonDown())
            {
                if(jumpCounter < jumpCountMax)
                {
                    

                    print(jumpCounter);

                    // More jump force is applied to the player object.
                    rb.velocity = Vector2.up * jumpForce * jumpCounter;
                    checkRadius += 0.02f;
                    jumpCounter++;
                    

                } else
                {
                    jumpCounter = 1;
                    checkRadius = checkRadiusValue;
                    // More jump force is applied to the player object.
                    rb.velocity = Vector2.up * jumpForce * jumpCounter;
                }
                
            }

            
            // The timer is being reduced by a constant frame independent value
            jumpTimeCounter -= Time.deltaTime;
        }
        else if (isGrounded && (InputManager.aButtonDown()))    // The player is touching the ground and the command to jump was registered during a specific frame.
        {
            if(jumpCounter > jumpCountMax)
            {
                jumpCounter = 1;
                
                checkRadius = checkRadiusValue;
            }
            // The player is now jumping.
            isJumping = true;
            // The force to simply jump is applied.
            rb.velocity = Vector2.up * jumpForce * jumpCounter;
            jumpCounter++;
        }
        else
        {
            // If no new jump command is registered the player is not jumping anymore.
            isJumping = false;
        }

        if(jumpTimeCounter <= 0)
        {
            //jumpCounter = 1;
        }

        /* Press to jump higher, Method A
        // While the command to jump has been registered once listen for continuous jump input.
        if(InputManager.aButton() && isJumping)
        {
            // The timer is checked so the player can't jump higher infintely.
            if(jumpTimeCounter > 0)
            {
                // More jump force is applied to the player object.
                rb.velocity = Vector2.up * jumpForce;
                // The timer is being reduced by a constant frame independent value
                jumpTimeCounter -= Time.deltaTime;
            } else
            {
                // If no new jump command is registered the player player is not jumping anymore.
                isJumping = false;
            }
        }
        */

        // Jumping is over when player releases the jump button.
        if (InputManager.aButtonUp())
        {
            isJumping = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.layer == 8)
        {
            if(jumpCounter == jumpCountMax)
            {
                jumpCounter = 1;
                checkRadius = checkRadiusValue;
            }
            // The timer for keeping the jump button pressed and therefore jumping higher is initialised.
            if(jumpCounter > 2)
            {
                checkRadius += 0.18f;
            }
            jumpTimeCounter = jumpTime + 0.3f;

        }
    }
}
