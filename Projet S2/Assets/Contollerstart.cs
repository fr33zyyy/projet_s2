using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contollerstart : MonoBehaviour
{
    public GameObject ancien;
    public GameObject hommeTictac;
    public GameObject Damefleur;
    // Start is called before the first frame update
    void Start()
    {
        if(!GestionTicTac.ajoue){
            PlayerPrefs.DeleteKey("MiniGameResult");
        }
        else{
            GestionTicTac.ajoue = true;
            ancien.SetActive(false);
            hommeTictac.SetActive(true);
        }
    }
}
