using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTetris5 : MonoBehaviour
{
    public Dialogue2 dialogueScript;
    public Transform InteractorSource;
    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        dialogueScript.lines[0] = "L’heure est venue pour toi d’affronter Cataclysm.";
        dialogueScript.lines[1] = "Je te dis adieu avec une pointe de tristesse, car aucun autre élu n’a jamais réussi à vaincre ce terrible ennemi.";
        dialogueScript.lines[2] = "L'étoile de téléportation vers le domaine de Cataclysm est sur le pont.";
        dialogueScript.lines[3] = "Revient en vie, s'il te plaît. Nous comptons sur toi.";
    }

  
    public void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player") && Gestion2.atester)
        {
            animator.SetBool("IsTalking", true);
            dialogueScript.StartDialogue("NPC5");
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
