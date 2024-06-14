using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTetris3 : MonoBehaviour
{
     public Dialogue2 dialogueScript;
    public Transform InteractorSource;
    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        dialogueScript.lines[0] = "Je te félicite d'avoir éliminé les 10 monstres de la nuit.";
dialogueScript.lines[1] = "En récompense, je te confie la dernière pierre, la plus puissante de toutes.";
dialogueScript.lines[2] = "Cette pierre envoie une boule enflammée très dangereuse.";
dialogueScript.lines[3] = "Pour l'utiliser, appuie sur le bouton gauche de la souris.";
    }

  
    public void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player") && Gestion2.fightopponant && !Gestion2.aparler)
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
