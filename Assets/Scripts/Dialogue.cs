using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue")]
public class Dialogue : ScriptableObject
{

   [TextArea(5, 10)] [SerializeField] private string dialogue;

   [SerializeField] private string
      dialoguePerson;
   
   [SerializeField] private Dialogue[] nextDialogue;
   


   public string GetDialoguePerson()
   {
      return dialoguePerson;
   }
   public string GetDialogue()
   {
      return dialogue;
   }

   public Dialogue[] GetNextDialogue()
   {
      return nextDialogue;
   }
   
}
