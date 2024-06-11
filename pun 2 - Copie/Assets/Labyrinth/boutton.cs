using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boutton : MonoBehaviour
{
    public GameObject Canvas;
    public perso Perso;
    public void Onclick(){
        Canvas.SetActive(false);
        Perso.SetEnjeu();
    }
}
