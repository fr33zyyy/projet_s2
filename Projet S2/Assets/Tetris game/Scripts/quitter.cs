using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class quitter : MonoBehaviour
{
    public Board board;
    // Start is called before the first frame update
    public void click(){
        if(Gestion2.preced == "map2"){
            Gestion2.tetrisscore = board.score;
            Gestion2.preced = "";
            Debug.Log(Gestion2.tetrisscore);
            SceneManager.LoadScene("2eme_map");
        }   
        else{
            SceneManager.LoadScene("lobby-solo");
        }
    }
}
