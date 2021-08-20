using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LatifaSection : MonoBehaviour
{
    public GameObject cam3;
    public GameObject cam2;
    private PlayerMovement scriptPlayerMovement;
    public bool isNearLatifa = false;
    [SerializeField] private Text quitDialogue;

    private void Start()
    {
        scriptPlayerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q)){
            StopCoroutine(LatifaSpeech());
            scriptPlayerMovement.enabled = true;
            cam2.SetActive(true);
            cam3.SetActive(false);
        }
      
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "TalkLatifaSpace")
        {
            isNearLatifa = true;
            Debug.Log("Lets Talk Latifa!");
            cam3.SetActive(true);
            cam2.SetActive(false);
            StartCoroutine(LatifaSpeech());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isNearLatifa = false;
        cam2.SetActive(true);
        cam3.SetActive(false);
    }


    IEnumerator LatifaSpeech()
    {
        scriptPlayerMovement.enabled = false;
        quitDialogue.text = "PRESS ENTER TO SKIP TO NEXT SCENE";
        yield return new WaitForSeconds(5f);
        quitDialogue.text = "PRESS Q TO QUIT THE CONVERSATION";
        yield return new WaitForSeconds(5f);
        quitDialogue.text = "";
        yield return new WaitForSeconds(35f);
        scriptPlayerMovement.enabled = true;
        cam2.SetActive(true);
        cam3.SetActive(false);
    }
    
}
