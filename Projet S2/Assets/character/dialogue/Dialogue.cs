using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
<<<<<<< HEAD
=======
using System;
<<<<<<< HEAD
using UnityEngine.UI;
=======
>>>>>>> 1c7cdca318f09deeb88e503c810615cf119934fd
>>>>>>> 839cfcd55b277d0200f5470a1ad53838a6882a26

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textspeed;
    public int index;
    public GameObject fleurs;
    public GameObject damefleur;
    public GameObject ancien;
    public GameObject hommeTictac;
    public GameObject CompteurFlower;

    // Variable to hold the current NPC identifier
    public string currentNpc;
     public List<string> npcsWithMinigame;
    // List of NPCs that should launch the mini-game
    public List<string> npcsFlower;

    public void Start()
    {
<<<<<<< HEAD
        if(PlayerPrefs.GetInt("MiniGameResult", 0) == 1){
            GestionTicTac.agagne = true;
        }
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
=======
<<<<<<< HEAD
        
=======
        if(!SceneData.ajouertictac){
            PlayerPrefs.DeleteKey("MiniGameResult");;
        }
>>>>>>> 1c7cdca318f09deeb88e503c810615cf119934fd
>>>>>>> 839cfcd55b277d0200f5470a1ad53838a6882a26
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
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
            if (ShouldLaunchMinigame(currentNpc) && !GestionTicTac.agagne)
            {
                minijeu();
            }
            if(ShouldFlower(currentNpc)){
                StartFlowerQ();
                GestionFlower.vu = true;
            }
            if (GestionFlower.vu && GestionFlower.complet && ShouldFlower(currentNpc)){
                CompteurFlower.SetActive(false);
                damefleur.SetActive(false);
                hommeTictac.SetActive(true);
                GestionGeneral.Flower = true;
            }
            if(currentNpc == "NPC1"){
                GestionGeneral.parlerancien = true;
                damefleur.SetActive(true);
                ancien.SetActive(false);
            }
            if(currentNpc == "NPC3" && GestionTicTac.agagne){
                hommeTictac.SetActive(false);
                GestionGeneral.MorpionQuete = true;
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
<<<<<<< HEAD
        
=======
<<<<<<< HEAD
        SceneData.previousScene = SceneManager.GetActiveScene().name;
        Debug.Log(SceneData.previousScene);
        SceneManager.LoadScene("lobby-solo");
        Debug.Log(SceneData.previousScene);
=======
        Debug.Log(PlayerPrefs.GetInt("MiniGameResult", -1));
>>>>>>> 839cfcd55b277d0200f5470a1ad53838a6882a26
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneData.previousScene = SceneManager.GetActiveScene().name;
        GestionTicTac.ajoue = true;
        // Charger la scène spécifiée
        SceneManager.LoadScene("titac ai");
>>>>>>> 1c7cdca318f09deeb88e503c810615cf119934fd
    }
    public void StartFlowerQ(){
        fleurs.SetActive(true);
        CompteurFlower.SetActive(true);
    }
}
