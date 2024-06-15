using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTetris4 : MonoBehaviour
{
     public Dialogue2 dialogueScript;
    public Transform InteractorSource;
    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

  
    public void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player") && !Gestion2.atester && Gestion2.aparler)
        {
            animator.SetBool("IsTalking", true);
            dialogueScript.StartDialogue("NPC4");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("IsTalking", false);
            dialogueScript.Start(); 
        }
    }

}
