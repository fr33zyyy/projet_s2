using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebutDialogueTicTacToe2 : MonoBehaviour
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
        if (other.CompareTag("Player") && GestionTicTac.ajoue && !GestionTicTac.agagne && !GestionGeneral.ramasse)
        {
            dialogueScript.StartDialogue("NPC3");
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
