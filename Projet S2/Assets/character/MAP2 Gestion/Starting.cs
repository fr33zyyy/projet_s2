using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Starting : MonoBehaviour
{
    public Image pierre1;
    public Image pierre2;
    public Image pierre3;
    public Dashing dash;
    public PlayerAttack attack;
    public GameObject anien;

    // Start is called before the first frame update
    void Start()
    {
        GestionGeneral.Map2 = true;
        pierre1.color = Color.black;
        pierre2.color = Color.black;
        pierre3.color = Color.black;
        attack.enabled = false;
        dash.enabled = false;
        anien.SetActive(true);
    }

    
}
