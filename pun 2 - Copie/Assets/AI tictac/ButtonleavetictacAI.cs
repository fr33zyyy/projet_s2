using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonleavetictacAI : MonoBehaviour
{
    public void LoadScene()
    {
        // Charger la scène spécifiée
        SceneManager.LoadScene("lobby-solo");
    }
}
