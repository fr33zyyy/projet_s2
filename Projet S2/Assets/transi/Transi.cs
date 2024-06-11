using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Transi : MonoBehaviour
{
    public GameObject textattention;
    public InputField inputField;
    public void Choose(){
        if(inputField.text == ""){

        }
        else{
            GestionGeneral.Name = inputField.text;
            // Charger la scène spécifiée
            SceneManager.LoadScene("SampleScene");
            textattention.SetActive(true);

        }
    }
}
