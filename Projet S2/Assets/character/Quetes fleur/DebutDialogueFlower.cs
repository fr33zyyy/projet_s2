using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebutDialogueFlower : MonoBehaviour
{
    public Dialogue dialogueScript;
    public Transform InteractorSource;
    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        dialogueScript.lines[0] = "Bonjour, jeune homme. J'ai entendu parler de ta quête pour sauver notre village.";
        dialogueScript.lines[1] = "Pour prouver ta détermination, je te demande de ramasser dix fleurs bleues.";
        dialogueScript.lines[2] = "Ces fleurs sont rares et précieuses, mais elles sont essentielles.";
        dialogueScript.lines[3] = "Lorsque tu les auras rassemblées, reviens me voir.";
        dialogueScript.lines[4] = "appuie sur T pour les ramasser";
    }

  
    public void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player") && !GestionFlower.vu && !GestionGeneral.ChercheCode)
        {
            animator.SetBool("IsTalking", true);
            dialogueScript.StartDialogue("NPC2");
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
