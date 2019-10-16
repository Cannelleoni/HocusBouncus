using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    // How fast is the player moving. Set to 0, as it is increased/decreased when player is moved.
    [SerializeField] float speed = 0f;
    // Acceleration for increasing / decreasing the speed variable.
    [SerializeField] int acceleration = 2;
    // Helping variable that contains last horizontal movement variable used to keep player moving while slowing down.
    [SerializeField] float lastHorizontalMovement;
    // Contains the Vector3 of the last movement, used to save the last movement direction.
    [SerializeField] Vector3 lastMovement;
    // Able to dash.
    bool dashPermit;
    // Counts frame rate until dash is available again. (2 seconds)
    int dashWait;

    // Initializing the helping variables.
    void Start() {

        lastHorizontalMovement = 0;
        dashPermit = true;
        dashWait = 0;
    }

    void Update() {
        
        normalMovement();

        // Counts up frames until dash is available again. (2 seconds)
        if (!dashPermit) {
            setDashWait(getDashWait() + 1);
        }

        // Dash function is called. Tests direction of dash.
        if (Input.GetButton("Left Bumper") && InputManager.mainHorizontal() > 0) {

            // Latency between dashes.
            if (dashPermit) {
                StartCoroutine(noDashies(1));
            }
        }
        else if (Input.GetButton("Left Bumper") && InputManager.mainHorizontal() < 0) {

            // Latency between dashes.
            if (dashPermit) {
                StartCoroutine(noDashies(-1));
            }
        }

        if (Input.GetKeyDown(KeyCode.B)) {
            stop();
        }


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

        bool aButton = Input.GetButton("A Button");

        if (aButton) {
            Debug.Log("a button pressed");
        }
    }


    // Normal movement with slow acceleration + smooth slow down.
    public void normalMovement() {

        // Shift in direction makes player's speed drop down to one. The remaining speed makes the change of direction smoother.
        if (lastHorizontalMovement != 0 && lastHorizontalMovement != InputManager.mainHorizontal()) {
            speed = 1;
        }
       
        // If horizontal movement is detected, the speed ist increased depending on the acceleration variable and deltaTime.
        if (InputManager.mainHorizontal() != 0) {
            
            /* The last horizontal movement variable is saved in lastHorizontalMovement
               This variable is used to keep the player moving for some time when keys are released.
            */
            if (InputManager.mainHorizontal() < 0) {
                lastHorizontalMovement = -1;
            }
            else {
                lastHorizontalMovement = 1;
            }

            // The vector lastMovement is initialized.
            lastMovement = new Vector3(lastHorizontalMovement, 0f, 0f);

            // Speed is increased, until it reaches the value 5.
            if (speed < 5) {
                speed += acceleration * Time.deltaTime;
            }

            // Decrease speed back to "normal" after dashing.
            else if (speed > 5) {
                speed--;
            }

            // Elude the problem of constant speed change due to float type.
            if (speed <= 6 && speed >= 5) {
                speed = 5f;
            }

            /* For moving the vector lastMovement is used, not the vector InputManager.mainJoyStick().
               The other vector was problematic each time the key was released. (value ca. 0,04 -> no visual movement)
             */
            transform.position += lastMovement * speed * Time.deltaTime;
        }

        // If no movement is detected, the speed is decreased depending on the acceleration variable and deltaTime.
        else {
            if (speed > 0) {
                speed -= acceleration * 2 * Time.deltaTime;

                if (speed < 0) {
                    speed = 0;
                }

            }

            transform.position += lastMovement * speed * Time.deltaTime;
        }

        // Instant stop.
        if (speed > 0 && Input.GetButton("Right Bumper")) {
            stop();
        }
    }

    // Get method for dashWait.
    public int getDashWait() {
        return this.dashWait;
    }

    // Set method for dashWait.
    public void setDashWait(int dashWait) {
        this.dashWait = dashWait;
    }

    // Dash function (push player forward fast + set speed to max).
    public void dash(int direction) {
        speed = 15f;
    }

    // Makes dash unavailable for certain time.
    public IEnumerator noDashies(int direction) {
        
        // Not able to dash right after dashing.
        dashPermit = false;

        // Dash forward.
        dash(direction);

        // Suppress dashing for 2 seconds.
        yield return new WaitUntil(() => getDashWait() >= 120);
        
        // Make dashing possible again.
        dashPermit = true;

        // Set wait-variable back to default.
        setDashWait(0);
    }

    // Instantly stops player's movement.
    public void stop() {
        speed = 0f;
    }
}
