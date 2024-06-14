using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTetris2 : MonoBehaviour
{
     public Dialogue2 dialogueScript;
    public Transform InteractorSource;
    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        dialogueScript.lines[0] = "Félicitations ! Tu as brillamment remporté la partie de Tetris.";
dialogueScript.lines[1] = "Grâce à ta victoire, tu récupères maintenant tes pouvoirs.";
dialogueScript.lines[2] = "Oh non !!!. La nuit est tombée.";
dialogueScript.lines[3] = "je te souhaite bonne chance!";
    }

  
    public void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player") && Gestion2.agagne && !Gestion2.fightopponant)
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
