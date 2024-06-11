using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class ButtonScript : MonoBehaviour
{
    public Button button;
    public Text buttonText;
    public GameManager1 gamemanager;
    private bool isOccupied{get;set;} = false;
    public int ligne;
    public int colonne;
    

    private void Start(){
        button.onClick.AddListener(OnButtonClick);
        gamemanager = GameObject.FindObjectOfType<GameManager1>();
        if(gamemanager == null){
            Debug.Log("pas trouve");
        }
    }

    public void OnButtonClick(){
        bool a = gamemanager.playerturn;
        if(!isOccupied && gamemanager.GetcurrenPlayer() == "X" && !gamemanager.isuse[ligne,colonne]){
            buttonText.text = gamemanager.GetcurrenPlayer();
            isOccupied = true;
            gamemanager.UptdateGrid(ligne,colonne,gamemanager.GetcurrenPlayer());
        }
        
    }

    public void restart(){
        isOccupied = false;
    }

    

}
