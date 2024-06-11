using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
<<<<<<< HEAD
=======
using System;
>>>>>>> 1c7cdca318f09deeb88e503c810615cf119934fd

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textspeed;
    public int index;

    // Variable to hold the current NPC identifier
    public string currentNpc;
     public List<string> npcsWithMinigame;
    // List of NPCs that should launch the mini-game

    public void Start()
    {
<<<<<<< HEAD
        
=======
        if(!SceneData.ajouertictac){
            PlayerPrefs.DeleteKey("MiniGameResult");;
        }
>>>>>>> 1c7cdca318f09deeb88e503c810615cf119934fd
        gameObject.SetActive(false);
        textComponent.text = string.Empty;

        // Initialize the list of NPCs that should launch the mini-game
        npcsWithMinigame = new List<string> { "NPC3" };
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
            if (ShouldLaunchMinigame(currentNpc))
            {
                minijeu();
            }
        }
    }

    // Function to determine if the minigame should be launched for the current NPC
    private bool ShouldLaunchMinigame(string npc)
    {
        return npcsWithMinigame.Contains(npc);
    }

    public void minijeu()
    {
<<<<<<< HEAD
        SceneData.previousScene = SceneManager.GetActiveScene().name;
        Debug.Log(SceneData.previousScene);
        SceneManager.LoadScene("lobby-solo");
        Debug.Log(SceneData.previousScene);
=======
        Debug.Log(PlayerPrefs.GetInt("MiniGameResult", -1));
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneData.previousScene = SceneManager.GetActiveScene().name;
        // Charger la scène spécifiée
        SceneManager.LoadScene("titac ai");
>>>>>>> 1c7cdca318f09deeb88e503c810615cf119934fd
    }
    
}
