using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingOffWall : MonoBehaviour
{
    [SerializeField] LayerMask layerToBounceOff;
    [SerializeField] Rigidbody2D rb;

    float rot = 0;

    bool wallBounce = false;
    Vector2 destination;
    

    // Update is called once per frame
    void FixedUpdate()
    {
        /*
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, Time.deltaTime * PlayerMovement.speed - .1f, layerToBounceOff))
        {
            Debug.Log("wall");
            
            
        }
        
        if(wallBounce)
        {
            Vector2.MoveTowards(transform.position, destination, 10f * Time.deltaTime);

        }
        if (transform.position.Equals(destination)) {
            wallBounce = false;
        }
        */
    }
    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the detected collisio is with a wall (nr 9):
        if(collision.collider.gameObject.layer == 9)
        {



            /*
            print("wall : " + rb.velocity);
            Vector2 reflectDir2D = Vector2.Reflect(new Vector2(-rb.velocity.x, rb.velocity.y), collision.GetContact(0).normal);
            print(reflectDir2D);
           // rot = 90 - Mathf.Atan2(reflectDir2D.y, reflectDir2D.x) * Mathf.Rad2Deg;
            //transform.Translate(new Vector2(-reflectDir2D.x, reflectDir2D.y) * PlayerMovement.speed * Time.deltaTime);
            PlayerMovement.setLastHorizontalMovement(-PlayerMovement.getLastHorizontalMovement());
            //rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
            print("reflect force: " + reflectDir2D * PlayerMovement.speed * 100 * Time.deltaTime);
            //rb.AddForce(reflectDir2D * PlayerMovement.speed * 100 * Time.deltaTime);
            rb.MovePosition((Vector2)transform.position + (reflectDir2D * PlayerMovement.speed * 10 * Time.deltaTime));
            Vector2.MoveTowards(transform.position, reflectDir2D * PlayerMovement.speed * 10 * Time.deltaTime, 2000f);
        
        }
        
    }*/

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.layer == 9)
        {

            Vector2 inDirection = new Vector2(-rb.velocity.x, rb.velocity.y);
            Vector2 inNormal = collision.GetContact(0).normal;
            Vector2 newVelocity = Vector2.Reflect(inDirection, inNormal);
            //print(newVelocity);
            PlayerMovement.setLastHorizontalMovement(PlayerMovement.getLastHorizontalMovement() * -1);
            //rb.velocity = newVelocity;
            //wallBounce = true;
            //destination = new Vector2(transform.position.x - newVelocity.x, transform.position.y + newVelocity.y);
            //Vector2.MoveTowards(transform.position, destination, 10f * Time.deltaTime);
            rb.AddForce(new Vector2(newVelocity.x, 0) * Time.deltaTime * 500f, ForceMode2D.Impulse);
            
        }
        
    }



}
