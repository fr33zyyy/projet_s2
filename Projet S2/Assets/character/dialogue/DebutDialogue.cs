using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DebutDialogue : MonoBehaviour
{
    public Dialogue dialogueScript;
    public Transform InteractorSource;
    public Animator animator;



    private void Start()
    {
        animator = GetComponent<Animator>();
        dialogueScript.lines[0] = "Bonjour " + GestionGeneral.Name +", Je suis heureux de te rencontrer.";
    }

  
    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player") && !GestionGeneral.ChercheCode)
        {
            animator.SetBool("IsTalking", true);
            dialogueScript.StartDialogue("NPC1");
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