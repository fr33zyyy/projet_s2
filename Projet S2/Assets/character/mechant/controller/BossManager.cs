using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    #region Singleton

    public static BossManager Instance;

    void Awake()
    {
        Instance = this;
    }

    #endregion

    public GameObject player;

}
