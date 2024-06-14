using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebutDialogueFlower3 : MonoBehaviour
{
    public Dialogue dialogueScript;
    public Transform InteractorSource;
    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        dialogueScript.lines[0] = "Ah, te voilà de retour ! As-tu réussi à trouver les dix fleurs bleues ?";
dialogueScript.lines[1] = "Fantastique ! Tu as accompli cette tâche avec succès.";
dialogueScript.lines[2] = "En récompense de ton effort, voici l'ingrédient dont tu as besoin.";
    }

  
    public void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player") && GestionFlower.vu && GestionFlower.complet && !GestionGeneral.ChercheCode)
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
