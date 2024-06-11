using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class perso : MonoBehaviour
{ 
     public float vitesse = 5f; // Vitesse de déplacement
     public GameObject rouge;
     public GameObject vert;
     public float tempsEcoule{get;set;} = 0f;
     public bool enjeu= false;
     public GameObject fin;
     public Text final;
     public int penalité = 0;
     private Vector2 pos;
     public Text temps;
     private float bestTime;
     
    void Start(){
        fin.SetActive(false);
        pos = this.transform.position;
        bestTime = PlayerPrefs.GetFloat("BestTime", Mathf.Infinity);
        temps.text = "temps à battre: " + (int)bestTime + " secondes";
        
    }
    public void SetEnjeu(){
        enjeu = true;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Appeler une fonction pour quitter le jeu
            SceneManager.LoadScene("labyrinth");
        }
        if(enjeu){
        float deplacementHorizontal = Input.GetAxis("Horizontal");
        float deplacementVertical = Input.GetAxis("Vertical");

        Vector3 deplacement = new Vector3(deplacementHorizontal, deplacementVertical, 0f) * vitesse * Time.deltaTime;
        Deplacer(deplacement);
        
             tempsEcoule += Time.deltaTime;
        }
    }

    void Deplacer(Vector3 deplacement)
    {
        // Effectuer le déplacement
        transform.Translate(deplacement);

        // Vérifier les collisions avec les murs
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.1f); // Utiliser un cercle de petit rayon pour détecter les collisions
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Wall"))
            {
                Debug.Log("kok");
                // Si le personnage entre en collision avec un mur, annuler le déplacement
                transform.Translate(-deplacement);
                penalité += 1;
                this.transform.position = pos;
                StartCoroutine(WaitOneSecond());
                break; // Sortir de la boucle dès qu'une collision est détectée
            }
            if(collider.CompareTag("rouge")){
                Debug.Log("kfr");
                GameObject.Destroy(rouge);
            }
            if(collider.CompareTag("vert")){
                GameObject.Destroy(vert);
            }
            if(collider.CompareTag("win")){
                enjeu = false;
                fin.SetActive(true);
                final.text = "vous avez gagné en " + (int)tempsEcoule + " secondes " + penalité + "secondes de pénalités";
                tempsEcoule += penalité;
                if (tempsEcoule < bestTime) {
                    final.text += "\nNew Record!!!";
                    PlayerPrefs.SetFloat("BestTime",tempsEcoule);
                    PlayerPrefs.Save(); 
                }
            }
        }
    }
    IEnumerator WaitOneSecond()
    {
        Debug.Log("Coroutine started");
        yield return new WaitForSeconds(1f); // Attendre 1 seconde
        tempsEcoule += 1f;
        Debug.Log("One second has passed");
    }
}
