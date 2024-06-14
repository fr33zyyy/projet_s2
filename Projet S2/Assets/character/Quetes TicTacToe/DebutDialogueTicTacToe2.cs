using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebutDialogueTicTacToe2 : MonoBehaviour
{
    public Dialogue dialogueScript;
    public Transform InteractorSource;
    public Animator animator;



    private void Start()
    {
        animator = GetComponent<Animator>();
        dialogueScript.lines[0] = "Ha ! Tu vois, je savais que tu n'avais aucune chance contre moi.";
dialogueScript.lines[1] = "Mais ne crois pas que je vais te faciliter la tâche la prochaine fois.";
dialogueScript.lines[2] = "Je suis prêt à te donner une chance de te racheter immédiatement.";
dialogueScript.lines[3] = "Une revanche ? Tu oses encore ? Soit. Prépare-toi à perdre à nouveau.";





    }

  
    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player") && GestionTicTac.ajoue && !GestionTicTac.agagne && !GestionGeneral.ramasse)
        {
            dialogueScript.StartDialogue("NPC3");
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
