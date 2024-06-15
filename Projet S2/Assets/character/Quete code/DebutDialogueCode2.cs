using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DebutDialogueCode2 : MonoBehaviour
{
    
    public Dialogue dialogueScript;
    public Transform InteractorSource;
    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        dialogueScript.lines[0] = "Ah, tu as trouvé le bon code ! Félicitations !";
        dialogueScript.lines[1] = "Tu es vraiment doué pour résoudre ces énigmes.";
        dialogueScript.lines[2] = "Voici donc le dernier ingrédient que tu cherchais.";
    }

  
    public void Activation() 
    {
       
        animator.SetBool("IsTalking", true);
        dialogueScript.StartDialogue("NPC5");

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