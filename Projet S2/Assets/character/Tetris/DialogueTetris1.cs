using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTetris1 : MonoBehaviour
{
     public Dialogue2 dialogueScript;
    public Transform InteractorSource;
    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        dialogueScript.lines[0] = "Tu as échoué à gagner la partie de Tetris.";
dialogueScript.lines[1] = "Le temps presse, la nuit approche rapidement.";
dialogueScript.lines[2] = "Tu dois te dépêcher de réessayer avant qu'il ne soit trop tard.";
    }

  
    public void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player") && Gestion2.ajouetet && !Gestion2.agagne)
        {
            dialogueScript.StartDialogue("NPC1");
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
