using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToMini : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadScene()
    {
        // Charger la scène spécifiée
        SceneManager.LoadScene("Menu-Solo-multi");
    }

}
