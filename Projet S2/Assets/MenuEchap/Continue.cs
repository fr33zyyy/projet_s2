using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Continue : MonoBehaviour
{
     public GameObject menu;
    public move move;
    public Dashing dashing;

    public void oups(){
        
            menu.SetActive(false);
            dashing.enabled = true;
            move.enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        
    }

}
