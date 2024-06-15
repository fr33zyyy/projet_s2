using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DebutDialogueCode : MonoBehaviour
{
    
    public Dialogue dialogueScript;
    public Transform InteractorSource;
    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        dialogueScript.lines[0] = "Oh, bonjour ! Tu es ici pour l'ingrédient, n'est-ce pas ?";
        dialogueScript.lines[1] = "Je suis ravie de t'aider, même si je suis un peu maladroite...";
        dialogueScript.lines[2] = "L'ingrédient que tu cherches est enfermé dans un coffre-fort.";
        dialogueScript.lines[3] = "Le code à quatre chiffres est caché sous forme d'indices disséminés dans le village.";
        dialogueScript.lines[4] = "Pour commencer, je peux te dire que tous les chiffres du code sont pairs.";
    }

  
    public void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player") && !Codegestion.vu)
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