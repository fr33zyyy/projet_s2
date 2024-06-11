using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float pickupDistance = 2f; // Distance à partir de laquelle le joueur peut ramasser une fleur

    void Update()
    {
        // Vérifier si le joueur appuie sur un bouton pour ramasser une fleur
        if (Input.GetKeyDown(KeyCode.T))
        {
            // Récupérer toutes les fleurs dans la scène
            Flower[] flowers = FindObjectsOfType<Flower>();

            // Récupérer la position actuelle du joueur
            Vector3 playerPosition = transform.position;

            // Parcourir toutes les fleurs et vérifier si le joueur peut en ramasser une
            foreach (Flower flower in flowers)
            {
                if (flower.CanBePickedUp(playerPosition, pickupDistance))
                {
                    flower.Pickup(); // Ramasser la fleur
                    Debug.Log("Fleur ramassée !");
                }
            }
        }
    }
}
