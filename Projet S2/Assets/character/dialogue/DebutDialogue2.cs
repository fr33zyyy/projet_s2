using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DebutDialogue2 : MonoBehaviour
{
    public Dialogue dialogueScript;
    public Transform InteractorSource;
    public Animator animator;



    private void Start()
    {
        animator = GetComponent<Animator>();
        dialogueScript.lines[0] = "Ah, te voilà ! As-tu apporté les trois ingrédients ?";
dialogueScript.lines[1] = "Excellent travail. En récompense, je te donne la deuxième pierre.";
dialogueScript.lines[2] = "Voici la pierre verte de la force.";
dialogueScript.lines[3] = "Pour l'activer, appuie sur le clic droit.";
dialogueScript.lines[4] = "Elle te confère une puissance de coup de poing exceptionnelle.";
dialogueScript.lines[5] = "Mais j'ai une nouvelle terrifiante pour toi.";
dialogueScript.lines[6] = "Tu dois te rendre dans le Monde Catastrophe.";
dialogueScript.lines[7] = "C'est un lieu sombre et périlleux.";
dialogueScript.lines[8] = "Là-bas, règnent les monstres de la nuit.";
dialogueScript.lines[9] = "Pour y accéder, tu dois utiliser un téléporteur en forme d'étoile.";
        }

  
    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player") && GestionGeneral.CodeQuete)
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