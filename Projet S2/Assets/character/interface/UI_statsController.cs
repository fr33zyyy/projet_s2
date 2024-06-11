using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_statsController : MonoBehaviour
{
    public UI_stats healthBar;
    public UI_stats manaBar;
 
    // Start is called before the first frame update
    void Start()
    {
        PlayerStats playerStats = FindObjectOfType<PlayerStats>();
        playerStats.healthChanged += healthBar.ChangeValue;
        playerStats.manaChanged += manaBar.ChangeValue;
    }

}