using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;
using Photon.Realtime;
using Unity.VisualScripting;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textspeed;
    public int index;
    public move scriptMove;
    public Dashing dashing;
    public PlayerAttack playerAttack;
    public InputField inputFieldcode;
    public Image pierre1;
     public Image pierre2;
      public Image pierre3;
    
    public GameObject reponsecode;
    public GameObject hommecode;
    public GameObject fleurs;
    public GameObject damefleur;
    public GameObject ancien;
    public GameObject hommeTictac;
    public GameObject CompteurFlower;
    public GameObject Star;

    // Variable to hold the current NPC identifier
    public string currentNpc;
     public List<string> npcsWithMinigame;
    // List of NPCs that should launch the mini-game
    public List<string> npcsFlower;

    public void Start()
    {
        if(PlayerPrefs.GetInt("MiniGameResult", 0) == 1){
            GestionTicTac.agagne = true;
        }
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if(!SceneData.ajouertictac){
            PlayerPrefs.DeleteKey("MiniGameResult");;
        }
        gameObject.SetActive(false);
        textComponent.text = string.Empty;

        // Initialize the list of NPCs that should launch the mini-game
        npcsWithMinigame = new List<string> { "NPC3" };
        npcsFlower = new List<string>{"NPC2"};
        
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    // StartDialogue now takes a parameter to set the current NPC
    public void StartDialogue(string npc)
    {
        index = 0;
        currentNpc = npc;
        gameObject.SetActive(true);
        StartCoroutine(TypeLine());
    }

    public IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            AudioSound.instance.PlayClickSound();
            textComponent.text += c;
            yield return new WaitForSeconds(textspeed);
        }
    }

    public void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            AudioSound.instance.PlayClickSound();
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
            if (currentNpc == "NPC3" )
            {
                minijeu();
            }
            if(ShouldFlower(currentNpc) && !GestionGeneral.ChercheCode){
                StartFlowerQ();
                GestionGeneral.ramasse = true;
                GestionFlower.vu = true;
            }
            if (GestionFlower.vu && GestionFlower.complet && ShouldFlower(currentNpc)){
                CompteurFlower.SetActive(false);
                damefleur.SetActive(false);
                GestionGeneral.ramasse = false;
                GestionGeneral.Flower = true;
                if(GestionGeneral.MorpionQuete){
                    hommecode.SetActive(true);
                }
            }
            if(currentNpc == "NPC1" && !GestionGeneral.ChercheCode && !GestionGeneral.CodeQuete){
                GestionGeneral.parlerancien = true;
                pierre1.color = Color.white;
                damefleur.SetActive(true);
                hommeTictac.SetActive(true);
                ancien.SetActive(false);
                dashing.enabled = true;
            }
            if(currentNpc == "NPC1" && GestionGeneral.CodeQuete){
                ancien.SetActive(false);
                Star.SetActive(true);
                pierre2.color = Color.white;
                playerAttack.enabled = true;
            }
            if(currentNpc == "NPC10" ){
                hommeTictac.SetActive(false);
                GestionGeneral.MorpionQuete = true;
                if(GestionFlower.complet){
                    hommecode.SetActive(true);
                }
            }
            if(currentNpc == "NPC4" && !Codegestion.vu){
                Codegestion.vu = true;
                GestionGeneral.ChercheCode = true;
                ancien.SetActive(true);
                damefleur.SetActive(true);
                hommeTictac.SetActive(true);
            }
            else if (currentNpc == "NPC4" && Codegestion.vu){
                reponsecode.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                scriptMove.enabled = false;
                dashing.enabled = false;
                inputFieldcode.text = "";
            }
            if(currentNpc == "NPC5"){
                damefleur.SetActive(false);
                hommecode.SetActive(false);
                hommeTictac.SetActive(false);
                Codegestion.reussi = true;
                GestionGeneral.ChercheCode = false;
                GestionGeneral.CodeQuete = true;
            }
        }
    }

    // Function to determine if the minigame should be launched for the current NPC
    private bool ShouldLaunchMinigame(string npc)
    {
        return npcsWithMinigame.Contains(npc);
    }
    private bool ShouldFlower(string npc){
        return npcsFlower.Contains(npc);
    }

    public void minijeu()
    {
        SceneData.previousScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("lobby-solo");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneData.previousScene = SceneManager.GetActiveScene().name;
        GestionTicTac.ajoue = true;
        // Charger la scène spécifiée
        SceneManager.LoadScene("titac ai");
    }
    public void StartFlowerQ(){
        fleurs.SetActive(true);
        CompteurFlower.SetActive(true);
    }
}
