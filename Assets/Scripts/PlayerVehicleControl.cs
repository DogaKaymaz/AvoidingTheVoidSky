using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.TextCore;
using UnityEngine.UI;

public class PlayerVehicleControl : MonoBehaviour
{
    private BoardController scriptBoardController;
    private PlayerMovement scriptPlayerMovement;
    private PositionConstraint bindingPositionConstraint;
    private RotationConstraint bindingRotationConstraint;
    private Rigidbody playerRinRigidbody;
    private GameObject switchingCameras;
    private CameraSwitch scriptCameraSwitch;
    
    private GameObject playerRin;
    
    [SerializeField] private Text pressEText;
    
    private Animator animatorRin;

    private bool 
        onTriggerBool = false,
        willBind = true;
   
    Vector3 rinAfterGetOffBoardRotation = new Vector3(0,default,0);
    
    private void Awake()
    {
        scriptPlayerMovement = GetComponent<PlayerMovement>();
        animatorRin = GetComponentInChildren<Animator>();
        playerRin = GameObject.Find("Player");
        bindingPositionConstraint = GetComponent<PositionConstraint>();
        bindingRotationConstraint = GetComponent<RotationConstraint>();
        playerRinRigidbody = GetComponentInChildren<Rigidbody>();
        switchingCameras = GameObject.Find("CameraScript");
        scriptCameraSwitch = switchingCameras.GetComponent<CameraSwitch>();


    }

    private void Update()
    {
        if (!scriptBoardController.enabled && scriptPlayerMovement.enabled && onTriggerBool)
        {
            pressEText.text = "PRESS E TO USE HOVERBOARD";
            if (Input.GetKeyDown(KeyCode.E))
            {
                scriptCameraSwitch.switchCamera();
                pressEText.text = "";
                Debug.Log("ON BOARD & Update");
                UniteObjects(willBind);
                scriptBoardController.enabled = true;
                scriptPlayerMovement.enabled = false;

            }
            
            
        }
        else if (scriptBoardController.enabled && !scriptPlayerMovement.enabled)
        {
            Debug.Log("ELSE IF CONDITION GO BRR");
            if (Input.GetKeyDown(KeyCode.E))
            {
                scriptCameraSwitch.switchCamera();
                pressEText.text = "";
                Debug.Log ("ON FEET & Update");
                UniteObjects(!willBind);
                scriptPlayerMovement.enabled = true;
                scriptBoardController.enabled = false;
            }
        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out BoardController scriptBoard))
        {
            scriptBoardController = scriptBoard;
            onTriggerBool = true;
        }
    }
    

    private void OnTriggerExit(Collider other)
    {
        onTriggerBool = false;
        pressEText.text = "";
    }

    IEnumerator OnBoard(bool usingBoard)
    {
        Debug.Log("Rin on board.");
        if (!usingBoard)
        {
            animatorRin.SetTrigger("UseBoard");
            yield return new WaitForSeconds(1.0f);
            pressEText.text = "PRESS E TO GET OFF THE HOVERBOARD";
            yield return new WaitForSeconds(5.0f);
            pressEText.text = "";
            usingBoard = true;
        }
        else
        {
            animatorRin.SetTrigger(("SkatingToIdle"));
            yield return new WaitForSeconds(1.0f);
            usingBoard = false;
        }
    }
   
    



    void UniteObjects(bool willBind)
    {
        bindingPositionConstraint.constraintActive = willBind;
        bindingRotationConstraint.constraintActive = willBind;
        
        if (willBind)
        {
            StartCoroutine(OnBoard(false));
            animatorRin.SetTrigger("Skating");
            Debug.Log("Rin is skating!");
        }
        else
        {
            StartCoroutine(OnBoard(true));
            animatorRin.SetTrigger("GetOffTheBoard");
            Debug.Log("Rin off the board");
            playerRin.transform.eulerAngles = rinAfterGetOffBoardRotation;
            playerRinRigidbody.constraints = RigidbodyConstraints.FreezeRotation & RigidbodyConstraints.FreezePositionY;
        }
    }
    


}
