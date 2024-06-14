using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Changerdemap1 : MonoBehaviour
{
     

    // Update is called once per frame
    
    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            Gestion2.boss = true;
            SceneManager.LoadScene("final_boss");
        }
    }
}
