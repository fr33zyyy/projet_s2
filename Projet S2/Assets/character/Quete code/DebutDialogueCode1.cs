using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DebutDialogueCode1 : MonoBehaviour
{
    
    public Dialogue dialogueScript;
    public Transform InteractorSource;
    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

  
    public void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player") && Codegestion.vu && !Codegestion.reussi)
        {
            dialogueScript.StartDialogue("NPC4");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dialogueScript.Start(); 
        }
    }
}