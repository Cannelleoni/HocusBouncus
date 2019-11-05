using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    // How fast is the player moving. Set to 0, as it is increased/decreased when player is moved.
    [SerializeField] float speed = 0f;
    // The average speed.
    float speedLimit = 8f;
    // Acceleration for increasing / decreasing the speed variable.
    [SerializeField] int acceleration = 2;
    // Helping variable that contains last horizontal movement variable used to keep player moving while slowing down.
    public static int lastHorizontalMovement;
    // Contains the Vector3 of the last movement, used to save the last movement direction.
    [SerializeField] Vector3 lastMovement;
    // Able to dash.
    bool dashPermit;
    // The speed added to the movement when dashing.
    [SerializeField] float dashBoost = 13f;
    // Counts frame rate until dash is available again. (2 seconds)
    int dashWait;
    // The amount of time dash is not usabel.
    float dashWaitTime = 120f;
    //Get access to the player's RigidBody2D component.
    [SerializeField] Rigidbody2D rb;

    // Initializing the helping variables.
    void Start() {

        // Make dash possible right from the start.
        setLastHorizontalMovement(1);
        lastMovement = new Vector3((float)getLastHorizontalMovement(), 0f, 0f);
        dashPermit = true;
        dashWait = 0;
        
    }

    void Update() {

        // Counts up frames until dash is available again. (2 seconds)
        if (!dashPermit) {
            setDashWait(getDashWait() + 1);
        }  
    }

    private void FixedUpdate()
    {
        normalMovement();

        // Dash function is called. Tests direction of dash.
        if (InputManager.leftBumper() && dashPermit)
        {
            StartCoroutine(noDashies(getLastHorizontalMovement()));
        }

        // Instant stop.
        if (speed > 0 && InputManager.rightBumper())
        {
            stop();
        }

        // - - - -


    }

    // Get method for lastHorizontalMovement.
    public static int getLastHorizontalMovement() {
        return lastHorizontalMovement;
    }

    // Set method for lastHorizontalMovement.
    public static void setLastHorizontalMovement(int lastMove) {
        lastHorizontalMovement = lastMove;
    }

    // Normal movement with slow acceleration + smooth slow down.
    public void normalMovement() {
        // The vector lastMovement is initialized.
        lastMovement = new Vector3((float)getLastHorizontalMovement(), 0f, 0f);

        // If horizontal movement is detected, the speed ist increased depending on the acceleration variable and deltaTime.
        if (InputManager.mainHorizontal() != 0) {

            // Shift in direction makes player's speed drop down to one. The remaining speed makes the change of direction smoother.
            if (getLastHorizontalMovement() != 0 && getLastHorizontalMovement() != InputManager.mainHorizontal()) {
                speed = 1;
            }

            /* The last horizontal movement variable is saved in lastHorizontalMovement
               This variable is used to keep the player moving for some time when keys are released.
            */
            if (InputManager.mainHorizontal() < 0) {
                setLastHorizontalMovement(-1);
            }
            else {
                setLastHorizontalMovement(1);
            }

            // The vector lastMovement is initialized.
            lastMovement = new Vector3((float) getLastHorizontalMovement(), 0f, 0f);

            // Speed is increased, until it reaches the value 5.
            if (speed < speedLimit) {
                speed += acceleration * Time.deltaTime;
            }

            // Decrease speed back to "normal" after dashing.
            else if (speed > speedLimit) {
                speed--;
            }

            // Elude the problem of constant speed change due to float type.
            if ((speed <= (speedLimit+1)) && (speed >= speedLimit)) {
                speed = speedLimit;
            }

            /* For moving the vector lastMovement is used, not the vector InputManager.mainJoyStick().
               The other vector was problematic each time the key was released. (value ca. 0,04 -> no visual movement)
             */
            // transform.position += lastMovement * speed * Time.deltaTime;
            rb.velocity = new Vector2(lastMovement.x * speed, rb.velocity.y);
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

        
    }

    // Get method for dashWait.
    public int getDashWait() {
        return dashWait;
    }

    // Set method for dashWait.
    public void setDashWait(int dashWait) {
        this.dashWait = dashWait;
    }

    // Dash function (push player forward fast + set speed to max).
    public void dash(int direction) {
        speed += dashBoost;
    }

    // Makes dash unavailable for certain time.
    public IEnumerator noDashies(int direction) {
        
        // Not able to dash right after dashing.
        dashPermit = false;

        // Dash forward.
        dash(direction);

        // Suppress dashing for 2 seconds.
        yield return new WaitUntil(() => getDashWait() >= dashWaitTime);
        
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
