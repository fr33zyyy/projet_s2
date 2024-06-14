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
        
        dialogueScript.lines[0] = GestionGeneral.Name + ", notre village est en danger. ";
dialogueScript.lines[1] = "Tu es l'élu pour sauver notre village de Cataclysm.";
dialogueScript.lines[2] = "Cataclysm est une créature destructrice.";
dialogueScript.lines[3] = "Si nous ne faisons rien, il plongera notre monde dans le chaos.";
dialogueScript.lines[4] = "Pour l'affronter, tu dois récupérer trois pierres magiques.";
dialogueScript.lines[5] = "Voici la première, la pierre de vitesse.";
dialogueScript.lines[6] = "Cette pierre te permet de faire des dashs rapides.";
dialogueScript.lines[7] = "Pour obtenir la deuxième, rapporte-moi trois ingrédients rares.";
dialogueScript.lines[8] = "Tu peux les trouver en parlant aux membre du village.";
dialogueScript.lines[9] = "Reviens avec eux et je te donnerai la deuxième pierre.";
dialogueScript.lines[10] = "appuie sur \"a\" si tu veux utiliser le premier pouvoir";
    }

  
    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player") && !GestionGeneral.ChercheCode && !GestionGeneral.CodeQuete)
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