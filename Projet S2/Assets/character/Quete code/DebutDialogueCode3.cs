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