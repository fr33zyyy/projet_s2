using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebutDialogueFlower2 : MonoBehaviour
{
    public Dialogue dialogueScript;
    public Transform InteractorSource;
    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        dialogueScript.lines[0] = "As-tu réussi à rassembler les dix fleurs bleues que je t'ai demandées ?";
dialogueScript.lines[1] = "Il semble que tu n'as pas encore toutes les fleurs.";
dialogueScript.lines[2] = "Reviens me voir quand tu les auras toutes.";
    }

  
    public void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player") && GestionFlower.vu && !GestionFlower.complet && !GestionGeneral.ChercheCode)
        {
            dialogueScript.StartDialogue("NPC2");
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
