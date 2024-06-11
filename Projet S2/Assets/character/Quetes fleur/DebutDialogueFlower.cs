using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebutDialogueFlower : MonoBehaviour
{
    public Dialogue dialogueScript;
    public Transform InteractorSource;

    private void Start()
    {
    }

  
    public void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            dialogueScript.StartDialogue();
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
