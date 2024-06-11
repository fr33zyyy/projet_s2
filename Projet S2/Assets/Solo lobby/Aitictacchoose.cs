using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Aitictacchoose : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadScene()
    {
        SceneData.previousScene = SceneManager.GetActiveScene().name;
        // Charger la scène spécifiée
        SceneManager.LoadScene("titac ai");
    }
}
