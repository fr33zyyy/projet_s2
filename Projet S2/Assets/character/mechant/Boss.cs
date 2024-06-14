using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public delegate void HealthChangedEvent(float currentValue);
    public event HealthChangedEvent healthChanged;
    public NavMeshAgent agent;
    public Transform player;
    public float health = 500f;
    public Animator animator;
    public int meleeAttackDamage = 20;
    public int jumpAttackDamage = 30;
    public int meteorDamage = 10;
    public GameObject meteorPrefab;  // Prefab de la météorite
    public GameObject ragdoll;

    private float meleeCooldown = 5f;
    private float jumpCooldown = 10f;
    private float meteorCooldown = 10f;

    private float lastMeleeTime = 0f;
    private float lastJumpTime = 0f;
    private float lastMeteorTime = 0f;

    private bool canMelee = true;
    private bool canJump = true;
    private bool canMeteor = true;

    // Portée pour commencer à poursuivre le joueur
    public float pursueRange = 10f;

    private bool isAttacking = false; // Variable pour indiquer si le boss est en train d'attaquer
    void Start(){
        if(Gestion2.fini){
            
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
            SceneManager.LoadScene("fin");
        }
    }
    void Update()
    {
        if (health <= 0f)
            return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (!isAttacking && distanceToPlayer <= pursueRange)
        {
            agent.SetDestination(player.position);
        }

        if (!isAttacking)
        {
            if (canMelee && Time.time - lastMeleeTime >= meleeCooldown && distanceToPlayer <= 2f)
            {
                MeleeAttack();
                lastMeleeTime = Time.time;
            }
            else if (canJump && Time.time - lastJumpTime >= jumpCooldown)
            {
                JumpAttack();
                lastJumpTime = Time.time;
            }
            else if (canMeteor && Time.time - lastMeteorTime >= meteorCooldown)
            {
                MeteorAttack();
                lastMeteorTime = Time.time;
            }
        }

        animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    void MeleeAttack()
    {
        Debug.Log("Boss performs Melee Attack");
        isAttacking = true;
        canMelee = false;
        animator.SetTrigger("MeleeAttack");
        agent.isStopped = true; // Stop movement

        // Ensure the boss resumes movement after the attack animation
        StartCoroutine(ResumeMovementAfterAnimation("MeleeAttack", () => { canMelee = true; }));
        
        if (Vector3.Distance(transform.position, player.position) <= 2f)
        {
            player.GetComponent<PlayerStats>().AddDamage(meleeAttackDamage);
        }
    }

    void JumpAttack()
    {
        Debug.Log("Boss performs Jump Attack");
        isAttacking = true;
        canJump = false;
        animator.SetTrigger("JumpAttack");
        agent.isStopped = true; // Stop movement
        
        // Move towards the player position
        StartCoroutine(JumpToPlayer());

        // Ensure the boss resumes movement after the attack animation
        StartCoroutine(ResumeMovementAfterAnimation("JumpAttack", () => { canJump = true; }));
    }

    IEnumerator JumpToPlayer()
    {
        yield return new WaitForSeconds(1.0f); // Wait for jump animation to play
        agent.SetDestination(player.position);
    }

    void MeteorAttack()
    {
        Debug.Log("Boss performs Meteor Attack");
        isAttacking = true;
        canMeteor = false;
        animator.SetTrigger("MeteorAttack");
        agent.isStopped = true; // Stop movement

        StartCoroutine(MeteorDamageCoroutine());

        // Ensure the boss resumes movement after the attack animation
        StartCoroutine(ResumeMovementAfterAnimation("MeteorAttack", () => { canMeteor = true; }));
    }

    IEnumerator MeteorDamageCoroutine()
    {
        Vector3 meteorPosition = player.position;
        GameObject meteorZone = Instantiate(meteorPrefab, meteorPosition, Quaternion.identity); // Instantiate the meteor prefab at the player's position
        Destroy(meteorZone, 3f); // Destroy the meteor zone after 3 seconds
        float duration = 3f;
        float interval = 0.5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            yield return new WaitForSeconds(interval);
            if (Vector3.Distance(player.position, meteorPosition) < 3f) // Assuming the meteorite zone radius is 3 units
            {
                player.GetComponent<PlayerStats>().AddDamage(meteorDamage);
            }
            elapsed += interval;
        }
    }

    IEnumerator ResumeMovementAfterAnimation(string animationName, System.Action onComplete)
    {
        // Wait for the animation to finish
        yield return new WaitUntil(() => !animator.GetCurrentAnimatorStateInfo(0).IsName(animationName));
        agent.isStopped = false; // Resume movement
        isAttacking = false; // Mark as not attacking
        onComplete?.Invoke(); // Invoke the onComplete action if provided
    }

    public void TakeDamage(float amount)
    {
        
        health -= amount;
        Debug.Log("boss prend des degats");
        if (health <= 0f)
        {
            Die();
        }
        UpdateHealthUI();
    }

    

    void Die()
    {
        Debug.Log("Boss Died");
        agent.isStopped = true;
        Instantiate(ragdoll, transform.position, transform.rotation);
        Destroy(this.gameObject);
        Gestion2.fini = true;
        SceneManager.LoadScene("fin");
        
    }
    void UpdateHealthUI()
    {
        float currentHealthPercent = health / 500f; // Assurez-vous d'ajuster la valeur maximale si nécessaire
        healthChanged?.Invoke(currentHealthPercent);
    }

    void OnDrawGizmosSelected()
    {
        // Draw melee attack range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 2f);

        // Draw pursue range
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, pursueRange);

        // Draw meteor attack zone (assuming a fixed radius)
        if (player != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(player.position, 3f); // Assuming the meteorite zone radius is 3 units
        }
    }
}