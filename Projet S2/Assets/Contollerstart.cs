using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Contollerstart : MonoBehaviour
{
    public GameObject ancien;
    public GameObject hommeTictac;
    public GameObject Damefleur;
    public GameObject player;
    public Image pierre1;
    public AudioSource clic;
    public Dashing dashing;
    // Start is called before the first frame update
    void Start()
    {
        if(GestionGeneral.Map2){
            SceneManager.LoadScene("2eme_map");
        }
        dashing.enabled = false;
        if(!GestionTicTac.ajoue){
            PlayerPrefs.DeleteKey("MiniGameResult");
        }
        else{
            if(!GestionFlower.complet){
                Damefleur.SetActive(true);
            }
            GestionTicTac.ajoue = true;
            ancien.SetActive(false);
            hommeTictac.SetActive(true);
            pierre1.color = Color.white;
            dashing.enabled=true;
            player.transform.position = new Vector3(610, 14.9f , 373.9f);
        }
    }
}
