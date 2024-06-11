using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    public bool isPickedUp = false; // Indique si la fleur a été ramassée ou non

    // Fonction pour ramasser une fleur
    public void Pickup()
    {
        isPickedUp = true;
        // Ajouter ici tout effet visuel ou sonore de ramassage
        Destroy(gameObject); // Détruire l'objet de la fleur une fois ramassée
    }

    // Fonction pour vérifier si la fleur peut être ramassée par le joueur
    public bool CanBePickedUp(Vector3 playerPosition, float pickupDistance)
    {
        // Calculer la distance entre la fleur et le joueur
        float distance = Vector3.Distance(transform.position, playerPosition);
        // Si la distance est inférieure ou égale à la distance de ramassage et que la fleur n'a pas été ramassée, retourner true
        return distance <= pickupDistance && !isPickedUp;
    }
}
