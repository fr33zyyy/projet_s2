using System.Collections;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    public float speed = 0.250f;
    public float rotationSpeed = 5.0f;
    public float punchCooldown = 10.0f;
    public float jumpCooldown = 4.0f;
    public float jumpForce = 10.0f; // Force du saut
    public float punchi;
    

    private GameObject player;
    private Animator animator;

    private enum State
    {
        Idle,
        Moving,
        Punching,
        Jumping
    }

    private State currentState;
    private float punchTimer;
    private float jumpTimer;
    private Vector3 jumpDirection; // Direction du saut

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();

        currentState = State.Moving;
        punchTimer = punchCooldown;
        jumpTimer = jumpCooldown;
    }

    private void Update()
    {
        switch (currentState)
        {   
            case State.Idle:
                punchTimer -= Time.deltaTime;
                jumpTimer -= Time.deltaTime;
                Vector3 lookDirection = player.transform.position - transform.position;
            lookDirection.y = 0;

            if (lookDirection != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(lookDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }
                if(jumpTimer < 0){
                    currentState = State.Jumping;
                    punchi = 3.7f;
                    // Capture de la direction du saut
                    jumpDirection = (player.transform.position - transform.position).normalized;
                    jumpDirection.y = 0;
                }
                if(Vector3.Distance(transform.position, player.transform.position) > 5.0f){
                    animator.SetBool("IsMooving", true);
                    currentState = State.Moving;
                }
                if (punchTimer <= 0)
                {
                    currentState = State.Punching;
                    punchi = 2.667f;
                    animator.SetBool("IsAttacking", true);
                    animator.SetBool("IsMooving", false);
                }
                
                break;
            case State.Moving:
                MoveTowardsPlayer();
                punchTimer -= Time.deltaTime;
                jumpTimer -= Time.deltaTime;
                if(jumpTimer < 0 && Vector3.Distance(transform.position, player.transform.position) < 10.0f){
                    currentState = State.Jumping;
                    punchi = 3.7f;
                    // Capture de la direction du saut
                    jumpDirection = (player.transform.position - transform.position).normalized;
                    jumpDirection.y = 0;
                }
                if(Vector3.Distance(transform.position, player.transform.position) < 5.0f){
                    animator.SetBool("IsMooving", false);
                    currentState = State.Idle;
                }
                if (punchTimer <= 0)
                {
                    currentState = State.Punching;
                    punchi = 2.667f;
                    animator.SetBool("IsAttacking", true);
                    animator.SetBool("IsMooving", false);
                }
                break;

            case State.Punching:
                // Do nothing, just let the animation play
                animator.SetBool("IsAttacking", false);
                animator.SetBool("IsMooving", true);
                punchi -= Time.deltaTime;
                if (punchi <= 0){
                    punchTimer = punchCooldown + 6f ;
                    jumpTimer = jumpCooldown;
                    currentState = State.Moving;
                }
                break;

            case State.Jumping:
                // Do nothing, just let the animation play
                animator.SetBool("IsJumping", true);
                animator.SetBool("IsMooving", false);
                punchi -= Time.deltaTime;
                if (punchi>2.0f){
                    transform.position += jumpDirection * jumpForce * Time.deltaTime/3 ;
                }
                
                if (punchi <= 0){
                    punchTimer = punchCooldown;
                    jumpTimer = jumpCooldown;
                    currentState = State.Moving;
                }
                
                break;

            default:
                break;
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        direction.y = 0;
        Vector3 movement = direction * speed * Time.deltaTime/3;
        transform.position += movement;

        // Verrouillage de la direction pendant le saut
        if (currentState != State.Jumping)
        {
            Vector3 lookDirection = player.transform.position - transform.position;
            lookDirection.y = 0;

            if (lookDirection != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(lookDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }
   
    
}
