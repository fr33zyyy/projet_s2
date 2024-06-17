using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class quit : MonoBehaviour
{
    // Start is called before the first frame update
    public void oupsy(){
        AudioSound.instance.ambiance.Stop();
        AudioSound.instance.night.Stop();
        SceneManager.LoadScene("firstlobby");
    }
}
