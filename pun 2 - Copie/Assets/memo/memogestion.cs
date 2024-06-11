using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun.Demo.Cockpit;
using UnityEngine;
using UnityEngine.UI;
public class memogestion : MonoBehaviour
{
     public Color[] colors; // Tableau de couleurs
    public Image imageComponent; // Référence au composant Image à modifier
    public Sprite[] sprites;
    public float changeInterval = 1f; // Intervalle de changement en secondes
    private int currentlevel = 1;

    private void Start()
    {
        // Démarre la coroutine pour changer la couleur
        StartCoroutine(ChangeImageCoroutine());
    }

    IEnumerator ChangeImageCoroutine()
    {
        // Répète indéfiniment
        while (true)
        {
            List<(int,int)> trace = new List<(int,int)>();
            for (int i = 0; i < currentlevel; i++)
            {
                // Choisis aléatoirement un index dans le tableau de couleurs
            int randomIndexc = UnityEngine.Random.Range(0, colors.Length);
            int randomIndexi = UnityEngine.Random.Range(0, sprites.Length);


            // Change la couleur de l'image affichée
            imageComponent.color = colors[randomIndexc];
            imageComponent.sprite = sprites[randomIndexi];
            trace.Add((randomIndexi,randomIndexc));
            
            // Attend pendant l'intervalle de temps spécifié
            yield return new WaitForSeconds(changeInterval);
            }
            Debug.Log("niveau " + currentlevel);
            for (int i = 0; i < trace.Count; i++)
            {
                (int a,int b) = trace[i];
                Debug.Log("image " + a);
                Debug.Log("couleur " + b);
            }
            currentlevel++;
        }
    }
    
    


}
