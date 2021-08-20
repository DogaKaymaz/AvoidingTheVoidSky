using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SubsystemsImplementation;
using UnityEngine.UI;
using DG.Tweening;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogueComponent;
    [SerializeField] private TextMeshProUGUI dialoguePersonComponent;
    [SerializeField] private Dialogue startingDialogue;
    [SerializeField] private Canvas dialogueCanvas;
    [SerializeField] private GameObject chosenPanel;
    
    
    public Transform chosenTransform;

    private LatifaSection latifaSectionScript;
    
    private int 
        choiceControl = 0;

    private float
        chosenTransformChangeablePosition = 0,
        chosenTransformPosition = -453.000000f + 540,
        animatorsfloat = 0,
        chosenTransformChangeDistance = 40.000000f;
    

    private bool
        enterToken = false;

    private Dialogue dialogue;
    


    private void Start()
    {
        dialogue = startingDialogue;
        dialogueComponent.text = dialogue.GetDialogue();
        dialoguePersonComponent.text = dialogue.GetDialoguePerson();
        chosenTransformChangeablePosition = chosenTransformPosition;
        choiceControl = 1;
        chosenTransform.position = new Vector3(960, chosenTransformPosition, 0);
        dialogueCanvas.enabled = false;
        latifaSectionScript = GameObject.Find("Player").GetComponent<LatifaSection>();
        chosenPanel.SetActive(false);
        animatorsfloat = 0.75f;
        latifaSectionScript.animatorLatifa.SetFloat("Talking", 0.25f, 0.1f, Time.deltaTime);

        // chosenTransform.position = new Vector3(default, chosenTransformPosition+chosenTransformChangeDistance, default);


    }

    private void Update()
    {


        if (latifaSectionScript.isNearLatifa)
        {
            dialogueCanvas.enabled = true;
        }
        else if (!latifaSectionScript.isNearLatifa)
        {
            dialogueCanvas.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && chosenTransformChangeablePosition >= chosenTransformPosition+chosenTransformChangeDistance-1)
        {
            chosenTransformChangeablePosition -= chosenTransformChangeDistance;
            choiceControl = 1;
        }

        else if (Input.GetKeyDown(KeyCode.UpArrow) && chosenTransformChangeablePosition <= chosenTransformPosition+1)
        {
            chosenTransformChangeablePosition += chosenTransformChangeDistance;
            choiceControl = 0;
        }
        
        chosenTransform.DOMoveY(chosenTransformChangeablePosition, 1);

        if(Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
        {
            enterToken = true;
            Debug.Log("Enterı aldım");
        }
        
        ManageDialogue(enterToken);
        enterToken = false;
    }

    void ManageDialogue(bool enterToken)
    {
        var nextDialogue = dialogue.GetNextDialogue();
        Debug.Log("Manage Dialogue'a girebildim");
        Debug.Log(" entertoken değeri:" + enterToken);
        Debug.Log("choice control değeri" + choiceControl);
        
        if (nextDialogue[1] != null)
        {
            chosenPanel.SetActive(true);
        }
        else if (nextDialogue[1] == null)
        {
            chosenPanel.SetActive(false);
        }
        
        if (enterToken)
        {
            

            switch(choiceControl)
            {
                case 0:
                    dialogue = nextDialogue[choiceControl];
                    animatorsfloat = 0.5f;
                    enterToken = false;
                    break;
                    
                    case 1:
                        if (nextDialogue[choiceControl] != null)
                        {
                            dialogue = nextDialogue[choiceControl];
                            animatorsfloat = 1f;
                            enterToken = false;
                            break;
                        }

                        else if(nextDialogue[choiceControl] == null)
                        {
                            dialogue = nextDialogue[choiceControl-1];
                            animatorsfloat = 0.75f;
                            enterToken = false;
                            break;
                        }
                        break;
            }
           
            if (latifaSectionScript.isNearLatifa==true)
            {
                latifaSectionScript.animatorLatifa.SetFloat("Talking", animatorsfloat, 0.01f, Time.deltaTime);
            }
            enterToken = false;
            
        }

        dialogueComponent.text = dialogue.GetDialogue();
        dialoguePersonComponent.text = dialogue.GetDialoguePerson();

    }

    
}
