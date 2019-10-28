using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script manages the different input types such as keyboard and controller.
// To set up individual input listeners go in unity to Edit > Project Settings > Input and then look at the examples or a tutorial.
public static class InputManager 
{
    // -- This section deals with axis inputs.

    public static float mainHorizontal()
    {
        // This variable takes all horizontal input information.
        float r = 0.0f;
        r += Input.GetAxis("Horizontal");
        r += Input.GetAxis("JHorizontal");
        // Only the maximum or minimum gets returned to prevent wonky results from multiple inputs like pressing a key or button a the same time.
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    public static float mainVertical()
    {
        // This variable takes all vertical input information.
        float r = 0.0f;
        r += Input.GetAxis("Vertical");
        r += Input.GetAxis("JVertical");
        // Only the maximum or minimum gets returned to prevent wonky results from multiple inputs like pressing a key or button a the same time.
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    // This method joins the returned values of mainHorizontal() and mainVertical() into one Vector3.
    public static Vector3 mainJoyStick()
    {
        return new Vector3(mainHorizontal(), 0, 0);
       // return new Vector3(mainHorizontal(), mainVertical(), 0.0f);
    }

    // -- This section deals with button inputs.
    // The reason we don't simply ask for the Input itself is so we can decide how a positive input shall be used in each situation.

    // Returns wether the A Button has been pressed during a specific frame.
    public static bool aButtonDown()
    {
        return Input.GetButtonDown("A Button");
    }

    // Returns wether the A Button has been pressed.
    public static bool aButton()
    {
        return Input.GetButton("A Button");
    }

    // Returns wether the AButton has been released.
    public static bool aButtonUp()
    {
        return Input.GetButtonUp("A Button");
    }

    // Returns wether the B Button has been pressed during a specific frame.
    public static bool bButtonDown()
    {
        return Input.GetButtonDown("B Button");
    }

    // Return wether the B Button has been pressed.
    public static bool bButton()
    {
        return Input.GetButton("B Button");
    }


    // Returns wether the X Button has been pressed.
    public static bool xButton()
    {
        return Input.GetButtonDown("X Button");
    }

    // Returns wether the Y Button has been pressed.
    public static bool yButton()
    {
        return Input.GetButtonDown("Y Button");
    }

    public static bool leftBumper()
    {
        return Input.GetButtonDown("Left Bumper");
    }

    public static bool rightBumper()
    {
        return Input.GetButtonDown("Right Bumper");
    }

}
