using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inputfieldcode : MonoBehaviour
{
    // Start is called before the first frame update
    public InputField inputField;
    public GameObject ecrit;
    public DebutDialogueCode2 dialoguebon;
    public DebutDialogueCode3 dialoguemauvais;
    public move scriptMove;
    public Dashing dashing;
    
    private int code = 8440;
    void Awake()
    {
        inputField = GetComponent<InputField>();
        inputField.contentType = InputField.ContentType.Custom;
        inputField.onValueChanged.AddListener(OnValueChanged);
    }

    void Update(){
        if(ecrit.activeInHierarchy){
            Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                scriptMove.enabled = false;
                dashing.enabled = false;    
        }
    }

    void OnValueChanged(string text)
    {
        string filteredText = "";
        foreach (char c in text)
        {
            if (char.IsDigit(c))
            {
                filteredText += c;
            }
        }

        if (filteredText != text)
        {
            inputField.text = filteredText;
        }
    }

    public void Submit(){
        if(inputField.text == code + ""){
            dialoguebon.Activation();
        }
        else{
            dialoguemauvais.Activation();
        }
        ecrit.SetActive(false);
        scriptMove.enabled = true;
        dashing.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


}
