using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    public float lookRadius = 30f;
    public float punchRadius = 3.5f; // Rayon d'attaque pour le coup de poing
    public float jumpRadius = 10f; // Rayon d'attaque pour le saut
    public float damageRadius = 3f; // Rayon dans lequel les dégâts du saut sont appliqués
    public float timeBetweenPunches = 1f; // Temps entre deux coups de poing
    public float timeBetweenJumps = 5f; // Temps entre deux sauts
    public int punchDamage = 10; // Dégâts du coup de poing
    public int jumpDamage = 30; // Dégâts du saut
    public float jumpHeight = 5f; // Hauteur du saut
    public float jumpDuration = 1f; // Durée totale du saut
    public float knockbackForce = 10f; // Force de repoussement du joueur

    private Transform target;
    private NavMeshAgent agent;
    private bool alreadyAttacked;
    private bool isJumping;

    public Animator animator;

    private Vector3 jumpStartPosition;
    private Vector3 jumpTargetPosition;
    private float jumpProgress;

    public string punchAnimationEndEventName = "OnPunchAnimationEnd";

    void Start()
    {
        target = BossManager.Instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);

            if (distance <= punchRadius && !alreadyAttacked)
            {
                PunchTarget();
            }
            else if (distance <= jumpRadius && !alreadyAttacked && !isJumping)
            {
                
                JumpToTarget();
            }

            if (distance <= agent.stoppingDistance)
            {
                
                FaceTarget();
            }
            
        }


        if (isJumping)
        {
            JumpTowardsTarget();
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void PunchTarget()
    {
    
        
        Debug.Log("Coup de poing !");

        // Déclencher l'animation de coup de poing
        animator.SetBool("Punch", true);

        PlayerStats playerStats = target.GetComponent<PlayerStats>();
        if (playerStats != null)
        {
            playerStats.AddDamage(punchDamage);
        }

        alreadyAttacked = true;
        Invoke(nameof(ResetAttack), timeBetweenPunches);
        
        StartCoroutine(ResetPunchParameter());
    }
    IEnumerator ResetPunchParameter()
    {
        // Attendre un court délai
        yield return new WaitForSeconds(0.1f);

        // Réinitialiser le paramètre "Punch" à false
        animator.SetBool("Punch", false);
    }
    void JumpToTarget()
    {
        float distanceToTarget = Vector3.Distance(transform.position, target.position);
    
        // Vérifier si la distance à la cible est dans la plage autorisée pour le saut
        if (distanceToTarget >= 5f && distanceToTarget <= 10f)
        {
            // Logique du saut
            Debug.Log("Saut !");

            animator.SetBool("Jump", true);
        
            jumpStartPosition = transform.position;
            jumpTargetPosition = target.position;
            jumpProgress = 0f;
            isJumping = true;
            agent.isStopped = true; // Arrête le mouvement du NavMeshAgent

            StartCoroutine(ResetJump());
        }
    }
    IEnumerator ResetJump()
    {
        // Attendre un court délai
        yield return new WaitForSeconds(0.1f);

        // Réinitialiser le paramètre de l'animation de saut à false
        animator.SetBool("Jump", false);
    }

    void JumpTowardsTarget()
    {
        jumpProgress += Time.deltaTime / jumpDuration;

        // Utilisation d'une courbe de parabole pour le mouvement vertical
        float height = Mathf.Sin(Mathf.PI * jumpProgress) * jumpHeight;

        Vector3 currentPosition = Vector3.Lerp(jumpStartPosition, jumpTargetPosition, jumpProgress);
        currentPosition.y += height;

        transform.position = currentPosition;

        // Vérifie si le boss est arrivé à la position cible
        if (jumpProgress >= 1f)
        {
            isJumping = false;
            agent.isStopped = false; // Relance le mouvement du NavMeshAgent

            if (Vector3.Distance(target.position, transform.position) <= damageRadius)
            {
                // Si le joueur est dans la zone de dégâts, appliquer les dégâts du saut et le repoussement
                ApplyJumpDamageAndKnockback();
            }

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenJumps);
        }
    }

    void ApplyJumpDamageAndKnockback()
    {
        PlayerStats playerStats = target.GetComponent<PlayerStats>();
        if (playerStats != null)
        {
            playerStats.AddDamage(jumpDamage);

            // Calculer le vecteur de direction du repoussement
            Vector3 knockbackDirection = (target.position - transform.position).normalized;

            // Inverser le vecteur de direction pour repousser le joueur
            knockbackDirection *= -1f;

            // Appliquer une force de repoussement au joueur
            Rigidbody playerRb = target.GetComponent<Rigidbody>();
            if (playerRb != null)
            {
                // Calculer la hauteur de la trajectoire parabolique du repoussement
                float knockbackHeight = Mathf.Sin(Mathf.PI * jumpProgress) * jumpHeight;

                // Appliquer la force de repoussement avec la hauteur ajustée
                Vector3 knockbackForceWithHeight = knockbackDirection * knockbackForce;
                knockbackForceWithHeight.y += knockbackHeight;
                playerRb.AddForce(knockbackForceWithHeight, ForceMode.Impulse);
            }
        }
    }


    void ResetAttack()
    {
        alreadyAttacked = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, punchRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, jumpRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, damageRadius);
    }
}