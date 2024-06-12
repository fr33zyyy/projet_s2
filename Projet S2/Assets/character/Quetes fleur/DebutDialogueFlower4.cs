using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebutDialogueFlower4 : MonoBehaviour
{
    public Dialogue dialogueScript;
    public Transform InteractorSource;
    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

  
    public void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player") && GestionGeneral.ChercheCode)
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
