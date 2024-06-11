using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class dialogueFlower : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textspeed;
    public int index;

    public void StartFlower()
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


    public void StartDialogueFlower()
    {
        index = 0;
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
        }
    }

}

