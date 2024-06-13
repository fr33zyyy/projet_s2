using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Changerdemap : MonoBehaviour
{
     

    // Update is called once per frame
    
    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            GestionGeneral.Map2 = true;
            SceneManager.LoadScene("2eme map");
        }
    }
}
