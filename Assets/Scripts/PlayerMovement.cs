using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float
        moveSpeed,
        rotateSpeed = 20,
        walkSpeed,
        runSpeed,
        jumpHeight,
        gravity = -7604.8f;
    
    private CharacterController charController;
    private Animator animatorRin;
    
    private Vector3 
        moveDirection, 
        velocity;
 
    private void Start()
    {
        charController = GetComponent<CharacterController>();
        animatorRin = GetComponentInChildren<Animator>();
        charController.slopeLimit = 70f;
        charController.minMoveDistance = 0.1f;

    }

    private void Update()
    {
        Move();
        Rotate();
    }

    private void Move()
    {

        if (velocity.y < 0)
        {
            velocity.y = -2f;
        }
        
        float moveZ = Input.GetAxis("Vertical");
        moveDirection = new Vector3(0,0,moveZ);
        moveDirection = transform.TransformDirection(moveDirection);

      
        
            if (moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
            {
                Walk();
            }

            else if (moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
            {
                Run();
            }
        
            else if (moveDirection == Vector3.zero)
            {
                Idle();
            }
            
            moveDirection *= moveSpeed;

            if (charController.isGrounded && Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
            
           
        

        charController.Move(moveDirection * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        charController.Move(velocity * Time.deltaTime);
    }

    
    
    
    private void Idle()
    {
        animatorRin.SetFloat("Speed", 0.5f,0.1f, Time.deltaTime);
    }

    private void Walk()
    {
        moveDirection *= walkSpeed;
        if(Input.GetAxis("Vertical") > 0) animatorRin.SetFloat("Speed", 0.75f, 0.1f, Time.deltaTime);
        else if(Input.GetAxis("Vertical") < 0) animatorRin.SetFloat("Speed", 0.25f, 0.1f, Time.deltaTime);
    }

    private void Run()
    {
        moveDirection *= runSpeed;
        if(Input.GetAxis("Vertical") > 0) animatorRin.SetFloat("Speed", 1f, 0.1f, Time.deltaTime);
        else if(Input.GetAxis("Vertical") < 0) animatorRin.SetFloat("Speed", 0f, 0.1f, Time.deltaTime);
    }

    private void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        
        animatorRin.SetTrigger("Jump");

    }

    private void Rotate()
    {
        float moveX = Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up, moveX);
    }

        // private void OnTriggerEnter(Collider other)
        // {
        //     if (other.TryGetComponent(out deneme cube))
        //     {
        //         Debug.Log("selam");
        //     }
        // }
}
