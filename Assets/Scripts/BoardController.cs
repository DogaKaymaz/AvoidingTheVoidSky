using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BoardController : MonoBehaviour
{
    [SerializeField] public Transform[] corners; // For identify the points under the board's corner.
    public Rigidbody rb; // To unite board's rigidbody to code section.

    [SerializeField] private Animator animatorRin;

    //Vector Variables for limit Z rotation of board.
    private Vector3
        biggerThanSixty = new Vector3(default, default, 60),
        smallerThanThreeHundred = new Vector3(default, default, 300),
        movement = new Vector3(0, 0, 0);
    // Up there, there is fixed positions, which we use to keep the board from spinning.


    // General float variables to identify the amount of the power of moves.
    [SerializeField] public float
        upForce = 20f, // Basis power of board's horizontal force when we are directing it.
        speed = 10f, // Basis power of board's which makes it always go forward.
        customGravity = 20f,
        // difference = 0,
        // playerHittingGround = 0f,
        amount = 20f; // Basis power of board's horizontal torque.


    RaycastHit[] hits = new RaycastHit[4];



    void FixedUpdate()
    {
        rb.AddForce(new Vector3(0, -1.0f, 0) * rb.mass * customGravity);


        for (int i = 0; i < corners.Length; i++)
        {
            ApplyForce(corners[i], hits[i]);



            movement.z = Input.GetAxis("Vertical") * speed;
            rb.AddRelativeForce(0, 0, movement.z, ForceMode.Impulse);


            float horizontalTorque = Input.GetAxis("Horizontal") * amount * Time.deltaTime;
            rb.AddRelativeTorque(transform.up * horizontalTorque, ForceMode.VelocityChange);


            if (transform.localRotation.eulerAngles.z > 60 && transform.localRotation.eulerAngles.z < 180)
            {
                // playerHittingGround = 1;
                // Vector3.RotateTowards(transform.eulerAngles, biggerThanSixty, 1 * Time.deltaTime, 0.0f);
                transform.eulerAngles = biggerThanSixty;


            }

            else if (transform.localRotation.eulerAngles.z < 300 && transform.localRotation.eulerAngles.z > 180)
            {
                // playerHittingGround = 2;
                // Vector3.RotateTowards(transform.eulerAngles, smallerThanThreeHundred, 1 * Time.deltaTime, 0.0f);
                transform.eulerAngles = smallerThanThreeHundred;


            }

        }

    }



// void FallingFromBoard()
// {
//     if (playerHittingGround == 1)
//     {
//         animatorRin.SetTrigger("Falling");
//         animatorRin.SetTrigger("StandUp");
//         animatorRin.SetTrigger("StandUpToMovement");
//         
//     }
//     else if (playerHittingGround == 2)
//     {
//         animatorRin.SetBool("FallingMirror", true);
//         animatorRin.SetTrigger("Falling");
//         animatorRin.SetTrigger("StandUp");
//         animatorRin.SetTrigger("StandUpToMovement");
//         animatorRin.SetBool("FallingMirror", false);
//         
//             
//         }
//     }




    void ApplyForce(Transform corners, RaycastHit hit)
    {
        if (Physics.Raycast(corners.position, -corners.up, out hit))
        {
            float force = 0;
            force = Mathf.Abs(1 / (hit.point.y - corners.position.y));
            rb.AddForceAtPosition(force * upForce * transform.up, corners.position, ForceMode.Acceleration);
        }
    }

}



