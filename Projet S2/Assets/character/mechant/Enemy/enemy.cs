using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health = 3;
    
    [Header("combat")]
    [SerializeField] float attackCD = 3f;
    [SerializeField] float attackRange = 3f;
    [SerializeField] float aggroRange = 4f;
    [SerializeField] float damageAmount = 10f;
    [SerializeField] GameObject hitVFX;
    [SerializeField] GameObject ragdoll;

    
    private GameObject player;
    NavMeshAgent agent;
    private Animator animator;
    float timePassed = 0f;
    float newDestinationCD = 0.5f;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

    }
    void Update()
    {
        animator.SetFloat("speed", agent.velocity.magnitude / agent.speed);

        if(timePassed >= attackCD)
        {
            if(Vector3.Distance(player.transform.position, transform.position) <= attackRange)
            {
                animator.SetTrigger("attacka");
                PlayerStats playerStats = player.GetComponent<PlayerStats>();
                if (playerStats != null)
                {
                    playerStats.AddDamage((int)damageAmount);
                }
                timePassed = 0;
            }
        }
        timePassed += Time.deltaTime;

        if(newDestinationCD <= 0 && Vector3.Distance(player.transform.position, transform.position)<= aggroRange)
        {
            newDestinationCD = 0.5f;
            agent.SetDestination(player.transform.position);
        }
        newDestinationCD -= Time.deltaTime;
        transform.LookAt(player.transform);
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        animator.SetTrigger("damage");

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Instantiate(ragdoll, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }

    public void HitVFX(Vector3 hitposition)
    {
        GameObject hit = Instantiate(hitVFX, hitposition, Quaternion.identity);
        Destroy(hit,3f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, aggroRange);

    }
}