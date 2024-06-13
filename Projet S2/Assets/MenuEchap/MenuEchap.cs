using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuEchap : MonoBehaviour
{
    public GameObject menu;
    public move move;
    public Dashing dashing;

    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            menu.SetActive(true);
            dashing.enabled = false;
            move.enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
