using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dashing : MonoBehaviour
{
     [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float dashTime = 0.2f;
    [SerializeField] private float dashCooldown = 1f;
    [SerializeField] private GameObject dashEffectPrefab; // GameObject à activer pendant le dash

    private move scriptMove;
    private CharacterController characterController;
    private bool isDashing = false;
    private float lastDashTime = -Mathf.Infinity;
    private GameObject currentDashEffect; // Référence à l'effet actuellement activé

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

        // Créer l'effet de dash s'il est défini
        if (dashEffectPrefab != null)
        {
            currentDashEffect = Instantiate(dashEffectPrefab, transform.position, transform.rotation);
        }

        Vector3 dashDirection = characterController.transform.forward;

        while (Time.time < startTime + dashTime)
        {
            characterController.Move(dashDirection * dashSpeed * Time.deltaTime);

            // Déplacer l'effet de dash avec le joueur
            if (currentDashEffect != null)
            {
                currentDashEffect.transform.position = transform.position;
            }

            yield return null;
        }

        // Terminer le dash
        scriptMove.enabled = true;
        isDashing = false;

        // Détruire l'effet de dash à la fin du dash
        if (currentDashEffect != null)
        {
            Destroy(currentDashEffect);
        }

        lastDashTime = Time.time;
    }
}