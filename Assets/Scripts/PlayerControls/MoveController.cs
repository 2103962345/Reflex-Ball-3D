using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    // Component Controller Definition
    private CharacterController controller;
    private Vector3 direction;
    public Transform ball;

    // Movement Speed & Jump
    public float forwardSpeed;
    public float jumpForce;
    public float rotateSpeed;
    public float gravity;

    // Horizontal Movement Lane Distance
    public float laneDistance;
    private int currentLane = 1; // 0-left 1-mid 2-right

    // Game Control
    public static bool gameStarted = false;

    // Audio Controller
    public AudioSource adSource;
    public AudioClip movement;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        //Check game started
        if (gameStarted)
        {
            // Forward Move and Gravity
            direction.z = forwardSpeed;
            direction.y += gravity * Time.deltaTime;

            // ball rotation
            ball.transform.Rotate(rotateSpeed, 0, 0);

            // Jump and gravity controller
            if (controller.isGrounded)
            {
                gravity = -30;
                if (SwipeManager.swipeUp)
                {
                    // Jump function called
                    adSource.PlayOneShot(movement);
                    Jump();
                }
            }
            else
            {
                if (SwipeManager.swipeDown)
                {
                    gravity += -200;
                }
            }

            //  Change lane controller
            if (SwipeManager.swipeRight)
            {
                adSource.PlayOneShot(movement);
                currentLane++;
                if (currentLane == 3)
                {
                    currentLane = 2;
                }
            }
            if (SwipeManager.swipeLeft)
            {
                adSource.PlayOneShot(movement);
                currentLane--;
                if (currentLane == -1)
                {
                    currentLane = 0;
                }
            }

            //  Move Controller
            Vector3 target = transform.position.z * transform.forward + transform.position.y * transform.up;
            if (currentLane == 0)
            {
                target += Vector3.left * laneDistance;
            }
            else if (currentLane == 2)
            {
                target += Vector3.right * laneDistance;
            }
            if (transform.position == target)
            {
                return;
            }

            //  Added smooth changing
            Vector3 diff = target - transform.position;
            Vector3 moveDirr = diff.normalized * 25 * Time.deltaTime;
            if (moveDirr.sqrMagnitude < diff.sqrMagnitude)
            {
                controller.Move(moveDirr);
            }
            else
            {
                controller.Move(diff);
            }
        }
    }

    private void FixedUpdate()
    {
        // moving with CharacterController
        if (gameStarted)
        {
            controller.Move(direction * Time.fixedDeltaTime);
        }
    }


    // Jump function
    private void Jump()
    {
        direction.y = jumpForce;
    }

}
