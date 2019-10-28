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
    // The player's maximum distance from the ground for jumping to be allowed.
    [SerializeField] float checkRadius;
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
    

    void Update()
    {
        // OverlapCircle return true when the playerCharacter is close enough to a ground layer that the distance <= the radius checkRadius. 
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, layerGround);

        // The player is touching the ground and the command to jump was registered during a specific frame.
        if(isGrounded && (InputManager.aButtonDown()))
        {
            // The player is now jumping.
            isJumping = true;
            // The timer for keeping the jump button pressed and therefore jumping higher is initialised.
            jumpTimeCounter = jumpTime;
            // The force to simply jump is applied.
            rb.velocity = Vector2.up * jumpForce;
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
        if(InputManager.aButtonUp())
        {
            isJumping = false;
        }
    }
}
