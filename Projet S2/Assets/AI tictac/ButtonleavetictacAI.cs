using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonleavetictacAI : MonoBehaviour
{
    public void LoadScene()
    {
        // Charger la scène spécifiée
        if (!string.IsNullOrEmpty(SceneData.previousScene))
            {
                SceneManager.LoadScene(SceneData.previousScene);
            }
            else
            {
                Debug.LogWarning("Previous scene not set.");
            }
    }
}
