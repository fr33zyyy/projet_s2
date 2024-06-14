using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DebutDialogueCode3 : MonoBehaviour
{
    
    public Dialogue dialogueScript;
    public Transform InteractorSource;
    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        dialogueScript.lines[0] = "Oh non, ce n'est pas le bon code ?";
dialogueScript.lines[1] = "Continue à chercher, je crois en toi";
dialogueScript.lines[2] = "N'oublie pas, tous les chiffres sont pair.";
    }

  
    public void Activation() 
    {
       
        dialogueScript.StartDialogue("NPC6");

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dialogueScript.Start(); 
        }
    }
}