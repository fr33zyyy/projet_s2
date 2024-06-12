using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown = 1.0f; // Cooldown time for attacks

    private Animator animator;
    private move moveScript; // Référence au script de mouvement
    private bool canAttack = true; // Variable de contrôle pour autoriser ou non une nouvelle attaque
    private float lastAttackTime;

    void Start()
    {
        animator = GetComponent<Animator>();
        moveScript = GetComponent<move>(); // Récupérer la référence au script de mouvement
    }

    void Update()
    {
        // Handle attack input
        if (Input.GetMouseButtonDown(1) && canAttack && Time.time - lastAttackTime > attackCooldown)
        {
            StartCoroutine(AttackCoroutine());
        }
    }

    IEnumerator AttackCoroutine()
    {
        canAttack = false; // Désactiver la possibilité d'attaquer à nouveau
        moveScript.enabled = false; // Désactiver le script de mouvement pendant l'attaque

        animator.SetTrigger("Attack"); // Déclencher l'animation d'attaque dans l'Animator

        // Attendre que l'animation d'attaque soit terminée
        yield return new WaitForSeconds(1.650f); // Remplacez 0.8f par la durée réelle de votre animation d'attaque

        // Fin de l'attaque : réactiver le script de mouvement et autoriser une nouvelle attaque
        moveScript.enabled = true; // Réactiver le script de mouvement après l'attaque
        canAttack = true; // Autoriser une nouvelle attaque

        lastAttackTime = Time.time;
    }
}