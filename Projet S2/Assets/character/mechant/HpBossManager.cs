using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBossManager : MonoBehaviour
{
    public UI_stats healthBarboss;
 
    // Start is called before the first frame update
    void Start()
    {
        Boss boss = FindObjectOfType<Boss>();
        boss.healthChanged += healthBarboss.ChangeValue;
    }

}