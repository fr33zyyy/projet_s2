using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossmove : MonoBehaviour
{
    // Vitesse de déplacement en unités par seconde
    public float speed = 2.0f;

    // Vitesse de rotation
    public float rotationSpeed = 5.0f;

    // Référence au GameObject du joueur
    private GameObject player;

    // Référence au Rigidbody du personnage
    private Rigidbody rb;

    void Start()
    {
        // Trouve le GameObject avec le tag "Player"
        player = GameObject.FindGameObjectWithTag("Player");

        // Récupère le Rigidbody attaché au GameObject
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Vérifie si le joueur a été trouvé
        if (player != null)
        {
            // Calcule la direction vers la position du joueur
            Vector3 direction = (player.transform.position - transform.position).normalized;

            // Ignore la hauteur en ajustant les composantes y à 0
            direction.y = 0;

            // Calcule le déplacement nécessaire
            Vector3 movement = direction * speed * Time.deltaTime;

            // Applique le déplacement au Rigidbody
            rb.MovePosition(transform.position + movement);

            // Calcule la rotation vers le joueur en ignorant la hauteur
            Vector3 lookDirection = player.transform.position - transform.position;
            lookDirection.y = 0; // Ignore la hauteur

            if (lookDirection != Vector3.zero) // Vérifie que la direction n'est pas nulle
            {
                Quaternion toRotation = Quaternion.LookRotation(lookDirection);
                // Applique la rotation au GameObject
                transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }
}
