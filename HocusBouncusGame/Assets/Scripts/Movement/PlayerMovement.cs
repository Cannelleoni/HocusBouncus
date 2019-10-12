using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // How fast is the player moving.
    [SerializeField] float speed = 5f;

    void Update()
    {
        /*
        if (InputManager.aButton())
        {
            Debug.Log("A was pressed");
        }
        if (InputManager.bButton())
        {
            Debug.Log("B was pressed");
        }
        if (InputManager.xButton())
        {
            Debug.Log("X was pressed");
        }
        if (InputManager.yButton())
        {
            Debug.Log("Y was pressed");
        }
        */
        transform.position += InputManager.mainJoyStick() * speed * Time.deltaTime;

        bool aButton = Input.GetButton("A Button");
        
        if(aButton)
        {
            Debug.Log("a button pressed");
        }
    }
}
