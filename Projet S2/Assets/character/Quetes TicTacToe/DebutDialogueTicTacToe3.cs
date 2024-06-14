using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebutDialogueTicTacToe3 : MonoBehaviour
{
    public Dialogue dialogueScript;
    public Transform InteractorSource;
    public Animator animator;



    private void Start()
    {
        animator = GetComponent<Animator>();
        dialogueScript.lines[0] = "Quoi ?! Comment est-ce possible ? Tu... tu m'as vaincu.";
dialogueScript.lines[1] = "C'est impensable ! Je dois admettre que tu as plus de talent que je ne le pensais.";
dialogueScript.lines[2] = "Très bien, tu as prouvé ta valeur. Voici ce que tu cherches.";
    }

  
    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player") && GestionTicTac.ajoue && GestionTicTac.agagne && !GestionGeneral.ChercheCode && !GestionGeneral.ramasse)
        {
            dialogueScript.StartDialogue("NPC10");
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
