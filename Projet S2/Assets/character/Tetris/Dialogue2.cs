using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;
using Photon.Realtime;
using Unity.VisualScripting;

public class Dialogue2 : MonoBehaviour
{
    public GameObject killcompt;
    public GameObject skelette;
    public int skelettetue{get;set;} = 0;
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textspeed;
    public int index;
    public Image pierre1;
    public Image pierre2;
    public Image pierre3;
    public GameObject lumiere;
    public GameObject lumieren;
    public Dashing dash;
    public PlayerAttack attack;
    public Projectile projectile;
    public GameObject star;
    
     
    
    public GameObject ancien;
    
    // Variable to hold the current NPC identifier
    public string currentNpc;
     public List<string> npcsWithMinigame;
    // List of NPCs that should launch the mini-game
    public List<string> npcsFlower;

    public void Start()
    {
        gameObject.SetActive(false);
        textComponent.text = string.Empty;
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
            if(currentNpc == "NPC2" && index == 1){
                lumiere.SetActive(false);
                lumieren.SetActive(true);
                AudioSound.instance.ambiance.Stop();
                AudioSound.instance.night.Play();
            }
            index++;
            AudioSound.instance.PlayClickSound();
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
            if(currentNpc == "NPC0"){
                Gestion2.preced = "map2";
                Gestion2.ajouetet = true;
                SceneManager.LoadScene("Tetris");
            }
            if(currentNpc == "NPC1"){
                Gestion2.preced = "map2";
                SceneManager.LoadScene("Tetris");
            }
            if(currentNpc == "NPC2"){
                ancien.SetActive(false);
                skelette.SetActive(true);
                pierre1.color = Color.white;
                pierre2.color = Color.white;
                attack.enabled = true;
                dash.enabled = true;
                killcompt.SetActive(true);
            }
            if(currentNpc == "NPC3"){
                killcompt.SetActive(false);
                pierre3.color = Color.white;
                projectile.enabled = true;
                Gestion2.aparler = true;
            }
            if (currentNpc == "NPC5"){
                
                star.SetActive(true);
                ancien.SetActive(false);
            }
            
        }
    }

    
}
