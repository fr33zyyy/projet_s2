using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossmove : MonoBehaviour
{
    public float speed = 2.0f;
    public float rotationSpeed = 5.0f;
    public float punchCooldown = 2.0f;
    public float jumpCooldown = 4.0f;

    private GameObject player;
    private Rigidbody rb;
    private Animator animator;

    private enum State
    {
        Idle,
        Moving,
        Punching,
        Jumping
    }

    private State currentState;
    private float stateTimer;
    private float punchTimer;
    private float jumpTimer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        currentState = State.Idle;
        stateTimer = 0f;
        punchTimer = 0f;
        jumpTimer = 0f;
    }

    void Update()
    {
        if (player != null)
        {
            stateTimer -= Time.deltaTime;
            punchTimer -= Time.deltaTime;
            jumpTimer -= Time.deltaTime;

            switch (currentState)
            {
                case State.Idle:
                    animator.SetBool("IsMooving", false);
                    animator.SetBool("IsJumping", false);
                    animator.SetBool("IsAttacking", false);

                    if (stateTimer <= 0f)
                    {
                        currentState = State.Moving;
                        stateTimer = Random.Range(1f, 3f); // Random moving duration
                    }
                    break;

                case State.Moving:
                    MoveTowardsPlayer();

                    if (stateTimer <= 0f)
                    {
                        currentState = State.Idle;
                        stateTimer = Random.Range(1f, 2f); // Random idle duration
                    }

                    if (punchTimer <= 0f)
                    {
                        currentState = State.Punching;
                        stateTimer = 1f; // Punch duration
                    }

                    if (jumpTimer <= 0f)
                    {
                        currentState = State.Jumping;
                        stateTimer = 1f; // Jump duration
                    }
                    break;

                case State.Punching:
                    Punch();
                    if (stateTimer <= 0f)
                    {
                        currentState = State.Idle;
                        punchTimer = punchCooldown;
                    }
                    break;

                case State.Jumping:
                    Jump();
                    if (stateTimer <= 0f)
                    {
                        currentState = State.Idle;
                        jumpTimer = jumpCooldown;
                    }
                    break;
            }
        }
    }

    void MoveTowardsPlayer()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        direction.y = 0;
        Vector3 movement = direction * speed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);

        Vector3 lookDirection = player.transform.position - transform.position;
        lookDirection.y = 0;

        if (lookDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        animator.SetBool("IsMooving", true);
    }

    void Punch()
    {
        animator.SetBool("IsMooving", false);
        animator.SetBool("IsJumping", false);
        animator.SetBool("IsAttacking", true);
        // Logique d'attaque au coup de poing ici
    }

    void Jump()
    {
        animator.SetBool("IsMooving", false);
        animator.SetBool("IsJumping", true);
        animator.SetBool("IsAttacking", false);
        // Logique de saut vers le joueur ici
    }
}
