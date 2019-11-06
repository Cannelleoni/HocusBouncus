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
    // Maximum amount player is allowed to jump in a row.
    int jumpCountMax = 3;
    // The Spriterenderer component of the player object. 
    [SerializeField] SpriteRenderer sr;

    

    private void Start()
    {
        // Asign the original radius to checkRadius.
        checkRadius = checkRadiusValue;
    }

    void Update()
    {
        // OverlapCircle return true when the playerCharacter is close enough to a ground layer that the distance <= the radius checkRadius. 
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, layerGround);
        if (Physics2D.Raycast(transform.position, Vector2.down, checkRadiusValue, layerGround))
        {
            Debug.Log("grounded");
        }

        // If the player is touching the ground.
        if(isGrounded)
        {
            sr.color = Color.red;       // Change the player's colour to red.
        } else
        {
            sr.color = Color.white;     // Change the player's colour to white.
        }
        

        // The timer to check if new jump command has been issued.
        if (jumpTimeCounter > 0)
        {
            // If the timer is still active, the ground is being touched & a jump command has been issued.
            if(isGrounded && InputManager.aButtonDown())
            {
                // There have been less than 3 linked jumps so far.
                if(jumpCounter < jumpCountMax)
                {
                    print(jumpCounter);     // Same as Debug.Log();

                    // More jump force is applied to the player object.
                    rb.velocity = Vector2.up * jumpForce * jumpCounter;
                    // Add 0.02f allowance to make chaining jumps easier.
                    //checkRadius += 0.02f;
                    // The player has jumped once more so the counter keeping track of it goes up as well.
                    jumpCounter++;
                    
                } else
                {
                    // This is the third jump.
                    // More jump force is applied to the player object.
                    rb.velocity = Vector2.up * jumpForce * jumpCounter;
                    // The counter gets reset to 1 so the combo can begin anew.
                    jumpCounter = 1;
                    // The radius gets reset to the original value.
                    checkRadius = checkRadiusValue;
                }
                
            }
            // The timer is being reduced by a constant frame independent value
            jumpTimeCounter -= Time.deltaTime;
        }
        else if (isGrounded && (InputManager.aButtonDown()))    // The player is touching the ground and the command to jump was registered during a specific frame.
        {   // Jumping still has to be possible once the timer has reached 0 after all.
            if(jumpCounter > jumpCountMax)
            {
                // In case the jump counter hasn't been reset yet.
                jumpCounter = 1;
                // In case the radius hasn't been reset yet.
                checkRadius = checkRadiusValue;
            }
            // The player is now jumping.
            isJumping = true;
            // The force to simply jump is applied.
            rb.velocity = Vector2.up * jumpForce * jumpCounter;
            // The player has jumped once more so the counter keeping track of it goes up as well.
            jumpCounter++;
        }
        else
        {
            // If no new jump command is registered the player is not jumping anymore.
            isJumping = false;
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

    // Gets called evrytime the player comes into contact with a collider.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collider belongs to the layer "Wall"
        if(collision.collider.gameObject.layer == 8)
        {
            // If the player has jumped 3 times
            if(jumpCounter >= jumpCountMax)
            {
                jumpCounter = 1;        // Reset the counter to 1.
                checkRadius = checkRadiusValue;     // Reset the radius to the original value.
            }
            // If the third jump is next widen the radius to trigger the combo more easily.
            if(jumpCounter > 2)
            {
               // checkRadius += 0.18f;
            }

            // The timer for keeping the jump button pressed and therefore jumping higher is initialised.
            switch(jumpCounter)
            {
                case 3:
                    jumpTimeCounter = jumpTime + 0.15f;
                    break;
                case 2:
                    jumpTimeCounter = jumpTime + 0.05f;
                    break;
                case 1:
                    jumpTimeCounter = jumpTime;
                    break;
                default:
                    break;
            }

        }
    }
}
