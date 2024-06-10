using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dashing : MonoBehaviour
{
    [SerializeField]
    public float dashSpeed = 20f;

    [SerializeField]
    public float dashTime = 0.2f;

    [SerializeField]
    public float dashCooldown = 1f;
    private Animator animator;

    private move scriptMove;
    private CharacterController characterController;
    private bool isDashing = false;
    private float lastDashTime = -Mathf.Infinity;

    void Start()
    {
        scriptMove = GetComponent<move>();
        characterController = GetComponent<CharacterController>(); 
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isDashing && Time.time >= lastDashTime + dashCooldown)
        {
            StartCoroutine(DashCoroutine());
        }

    }

    IEnumerator DashCoroutine()
    {

        isDashing = true;
        float startTime = Time.time;

        scriptMove.enabled = false;

        Vector3 dashDirection = characterController.transform.forward;

        while (Time.time < startTime + dashTime)
        {
            characterController.Move(dashDirection * dashSpeed * Time.deltaTime);
            yield return null;
        }
        
        scriptMove.enabled = true;
        isDashing = false;
        
        lastDashTime = Time.time;
    }
}
