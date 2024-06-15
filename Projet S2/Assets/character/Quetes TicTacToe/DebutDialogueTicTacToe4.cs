using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebutDialogueTicTacToe4 : MonoBehaviour
{
    public Dialogue dialogueScript;
    public Transform InteractorSource;
    public Animator animator;



    private void Start()
    {
        animator = GetComponent<Animator>();
        
    }

  
    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player") && GestionGeneral.ChercheCode)
        {
            animator.SetBool("IsTalking", true);
            dialogueScript.StartDialogue("NPC0");
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
