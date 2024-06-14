using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Transi : MonoBehaviour
{
    public GameObject textattention;
    public InputField inputField;
    void Start(){
        if(GestionGeneral.Gotname){
            SceneManager.LoadScene("SampleScene");
        }
    }
    public void Choose(){
        if(inputField.text == ""){
                textattention.SetActive(true);
        }
        else{
            GestionGeneral.Name = inputField.text;
            GestionGeneral.Gotname = true;
            // Charger la scène spécifiée
            SceneManager.LoadScene("SampleScene");
            

        }
    }
}
