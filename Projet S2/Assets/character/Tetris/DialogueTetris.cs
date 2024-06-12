using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTetris : MonoBehaviour
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
        if (other.CompareTag("Player"))
        {
            dialogueScript.StartDialogue("NPC0");
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
