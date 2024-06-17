using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startfinal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioSound.instance.ambiance.Stop();
        AudioSound.instance.night.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
