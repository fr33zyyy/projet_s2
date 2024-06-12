using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    public float lookRadius = 10f;
    public float punchRadius = 2f; // Rayon d'attaque pour le coup de poing
    public float jumpRadius = 6f; // Rayon d'attaque pour le saut
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

    private Vector3 jumpStartPosition;
    private Vector3 jumpTargetPosition;
    private float jumpProgress;

    void Start()
    {
        target = BossManager.Instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
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
        // Logique du coup de poing
        Debug.Log("Coup de poing !");

        PlayerStats playerStats = target.GetComponent<PlayerStats>();
        if (playerStats != null)
        {
            playerStats.AddDamage(punchDamage);
        }

        alreadyAttacked = true;
        Invoke(nameof(ResetAttack), timeBetweenPunches);
    }

    void JumpToTarget()
    {
        // Logique du saut
        Debug.Log("Saut !");

        jumpStartPosition = transform.position;
        jumpTargetPosition = target.position;
        jumpProgress = 0f;
        isJumping = true;
        agent.isStopped = true; // Arrête le mouvement du NavMeshAgent
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

            // Calculer le vecteur de direction opposée au boss
            Vector3 knockbackDirection = (target.position - transform.position).normalized;
            knockbackDirection.y = 0; // Ne pas repousser verticalement

            // Inverser le vecteur de direction
            knockbackDirection *= -1f;

            // Appliquer une force de repoussement au joueur
            Rigidbody playerRb = target.GetComponent<Rigidbody>();
            if (playerRb != null)
            {
            playerRb.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
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