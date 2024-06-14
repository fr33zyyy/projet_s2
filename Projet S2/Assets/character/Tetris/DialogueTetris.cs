using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTetris : MonoBehaviour
{
     public Dialogue2 dialogueScript;
    public Transform InteractorSource;
    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        dialogueScript.lines[0] = GestionGeneral.Name + ", tu as perdu tes pouvoirs en venant ici.";
dialogueScript.lines[1] = "Pour les retrouver, tu dois gagner une partie de Tetris du Monde Catastrophe.";
dialogueScript.lines[2] = "Mais sois prudent, la nuit approche vite.";
dialogueScript.lines[3] = "Avec elle viennent les redoutables monstres de la nuit.";
    }

  
    public void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player") && !Gestion2.ajouetet)
        {
            dialogueScript.StartDialogue("NPC0");
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
