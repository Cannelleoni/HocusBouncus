using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingOffWall : MonoBehaviour
{
    // The layer the player Object is supposed to interact with.
    [SerializeField] LayerMask layerToBounceOff;
    // The rigidbody2d component of the player object.
    [SerializeField] Rigidbody2D rb;
    //The force used to amplify the bounce.
    [SerializeField] float bounceForce;
    


    // Has the player Object come into contact with a collider.
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Is the object that has been collided with on the layer "Wall"?
        if(collision.collider.gameObject.layer == 9)
        {
            // Get the movement vector of the player object.
            Vector2 inDirection = new Vector2(-rb.velocity.x, rb.velocity.y);
            // Get the normal vector of the hit object.
            Vector2 inNormal = collision.GetContact(0).normal;
            // Use the normal vector to calculate the reflected movement vector.
            Vector2 newVelocity = Vector2.Reflect(inDirection, inNormal);
            // Change the player's facing direction since they're bounced in the other direction.
            PlayerMovement.setLastHorizontalMovement(PlayerMovement.getLastHorizontalMovement() * -1);
            //Apply the bounce force to the player, frame independent and multiplied with the bounceForce.
            // The force is instantely aplied by using the mass of the player object.
            rb.AddForce(new Vector2(newVelocity.x, 0) * Time.deltaTime * bounceForce, ForceMode2D.Impulse);
            
        }
        
    }



}
