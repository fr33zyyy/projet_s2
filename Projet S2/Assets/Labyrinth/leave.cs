using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class leave : MonoBehaviour
{
    // Start is called before the first frame update
    public void Onclick(){
        SceneManager.LoadScene("labyrinth");
    }
}
