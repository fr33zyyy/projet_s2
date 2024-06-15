using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown = 1.0f; // Cooldown time for attacks
    [SerializeField] private float attackDuration = 1.650f; // Duration of the attack animation
    [SerializeField] private float punchRange = 1.0f; // Range of the punch
    [SerializeField] private float punchDamage = 10.0f; // Damage dealt by the punch
    [SerializeField] private LayerMask targetLayerMask; // Layer mask for detecting targets
    [SerializeField] private GameObject punchVFXPrefab; // Prefab for the punch VFX
    [SerializeField] private GameObject explosionVFXPrefab; // Prefab for the explosion VFX
    [SerializeField] private Transform handTransform; // Transform of the player's hand
    [SerializeField] private float explosionDuration = 1.0f; // Duration before the explosion VFX is destroyed

    private Animator animator;
    private move moveScript; // Reference to the movement script
    private bool canAttack = true; // Control variable to allow or disallow a new attack
    private float lastAttackTime;
    private GameObject currentVFX; // Reference to the current VFX instance

   

    void Start()
    {
        animator = GetComponent<Animator>();
        moveScript = GetComponent<move>(); // Get the reference to the movement script
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
        canAttack = false; // Disable the ability to attack again
        moveScript.enabled = false; // Disable the movement script during the attack

        animator.SetTrigger("Attack"); // Trigger the attack animation in the Animator

        currentVFX = Instantiate(punchVFXPrefab, handTransform.position, handTransform.rotation);
        currentVFX.transform.SetParent(handTransform);

        // Wait until the middle of the attack animation
        yield return new WaitForSeconds(attackDuration / 2);

        DealDamage();

        // Wait for the rest of the attack animation to finish
        yield return new WaitForSeconds(attackDuration / 2);

        currentVFX.transform.SetParent(null);
        Destroy(currentVFX);

        // Instantiate the explosion VFX at the current position of the hand
        GameObject explosionVFX = Instantiate(explosionVFXPrefab, handTransform.position, Quaternion.identity);

        DealDamage();

        // Destroy the explosion VFX after a delay
        Destroy(explosionVFX, explosionDuration);

        // Re-enable the movement script and allow a new attack
        moveScript.enabled = true; // Re-enable the movement script after the attack
        canAttack = true; // Allow a new attack

        lastAttackTime = Time.time;
    }

    private void DealDamage()
    {   
        Collider[] hits = Physics.OverlapSphere(transform.position, punchRange, targetLayerMask);
        foreach (var hit in hits)
        {
            Enemy enemy = hit.GetComponent<Enemy>();
            Boss boss = hit.GetComponent<Boss>();
            if (enemy != null)
            {
                enemy.TakeDamage(punchDamage);
                enemy.HitVFX(hit.transform.position);
            }
            if(boss != null)
            {
                boss.TakeDamage(punchDamage);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, punchRange);
    }
}