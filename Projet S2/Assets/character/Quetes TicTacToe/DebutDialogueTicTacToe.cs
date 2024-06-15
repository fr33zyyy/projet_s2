using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebutDialogueTicTacToe : MonoBehaviour
{
    public Dialogue dialogueScript;
    public Transform InteractorSource;
    public Animator animator;



    private void Start()
    {
        animator = GetComponent<Animator>();
        dialogueScript.lines[0] = "Hmpf, encore un prétendant à la gloire, n'est-ce pas ?";
        dialogueScript.lines[1] = "Tu prétends pouvoir sauver notre village de Cataclysm ?";
        dialogueScript.lines[2] = "Ha ! Je ne te crois pas capable de grand-chose.";
        dialogueScript.lines[3] = "Mais si tu veux prouver ta valeur, tu devras me battre à une partie de Morpion.";
        dialogueScript.lines[4] = "Ce jeu peut sembler simple, mais il demande une grande stratégie.";
        dialogueScript.lines[5] = "Si, par miracle, tu réussis à me vaincre, je te remettrai ce que tu cherches.";
        dialogueScript.lines[6] = "Je t'attends donc, si tu oses relever ce défi !";
    }

  
    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player") && !GestionTicTac.ajoue && !GestionGeneral.ChercheCode && !GestionGeneral.ramasse)
        {
            animator.SetBool("IsTalking", true);
            dialogueScript.StartDialogue("NPC3");
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
