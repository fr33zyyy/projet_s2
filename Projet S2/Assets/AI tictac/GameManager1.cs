using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using UnityEngine.SceneManagement;

public class GameManager1 : MonoBehaviour
{
    public List<Text> texts;
    private MediumAI mediumins;
    public Text managetext;
    public bool playerturn{get;set;} = true;
    public string[,] grid = new string[3,3]; 
    public bool[,] isuse{get;set;} = new bool[3,3];
    public Text joueurwin;
    public Text AIwin;
    private int joueurw = 0;
    private int AIw = 0;
    public GameObject endingpan;
    public Text winingtext;
    public List<Button> buttons;
        void Start(){
            SceneData.ajouertictac = true;
    managetext.text = "Player \"" + GetcurrenPlayer() + "\"'s turn";
    for(int i = 0; i<3;i++){
        for (int j = 0;j<3;j++){
            isuse[i,j] = false;
            grid[i,j] = "";
        } 
    }
    mediumins = new MediumAI();
    AIwin.text = "AI :" + AIw;
    joueurwin.text = "Your :" + joueurw;
    }

    
    // Start is called before the first frame update
    public void Switchplayer(){
        playerturn = !playerturn;
        managetext.text = "Player \"" + GetcurrenPlayer() + "\"'s turn";
    }

    public void UptdateGrid(int ligne, int colonne, string symbol){
        if(AIw>=5 || joueurw>=5 ){
            endingpan.SetActive(true);
        }
        else{
        grid[ligne,colonne] = symbol;
        if (CheckForWin(grid,GetcurrenPlayer())){

            managetext.text = "Player \"" + GetcurrenPlayer() + "\" win!";
            if(playerturn){
                joueurw += 1;
                joueurwin.text = "Player :" + joueurw;
            }
            else{
                AIw += 1;
                AIwin.text = "AI :" + AIw;
            }
            if(AIw>=5 || joueurw>=5 ){
            endingpan.SetActive(true);
            if(joueurw<5){
                winingtext.text = "perdu";
                PlayerPrefs.SetInt("MiniGameResult", PlayerPrefs.GetInt("MiniGameResult", 0));
                // Sauvegarder les changements
                PlayerPrefs.Save();
            }
            else{
                winingtext.text = "win";
                joueurwin.text = "Your :" + joueurw;
                PlayerPrefs.SetInt("MiniGameResult",  1);
                // Sauvegarder les changements
                PlayerPrefs.Save();
            }
        }
            for(int i = 0; i<3;i++){
            for (int j = 0;j<3;j++){
                isuse[i,j] = false;
                grid[i,j] = "";
            } 
            }
            foreach (var item in buttons)
            {
                item.GetComponent<ButtonScript>().restart();
            }
            foreach (var item in texts)
            {
                item.text = "";
            }
            if(playerturn){
            (int,int)a = mediumins.ChooseNextMove(grid,"O");
            (int b,int c) = a;
            Switchplayer();
            isuse[b,c] = true;
            texts[b*3+c].text = "O";
            UptdateGrid(b,c,"O");
            }
            else{
            Switchplayer();
            }
        
            
            
        }
        else if(Draw(grid)){
            managetext.text = "DRAW!";
            for(int i = 0; i<3;i++){
            for (int j = 0;j<3;j++){
                isuse[i,j] = false;
                grid[i,j] = "";
            } 
            }
            foreach (var item in buttons)
            {
                item.GetComponent<ButtonScript>().restart();
            }
            foreach (var item in texts)
            {
                item.text = "";
            }
            if(playerturn){
            (int,int)a = mediumins.ChooseNextMove(grid,"O");
            (int b,int c) = a;
            Switchplayer();
            isuse[b,c] = true;
            texts[b*3+c].text = "O";
            UptdateGrid(b,c,"O");
            }
            else{
            Switchplayer();
            }
        }
        else{
            if(GetcurrenPlayer() == "O"){
                Switchplayer();
            }
            else{
                (int,int)a = mediumins.ChooseNextMove(grid,"O");
            (int b,int c) = a;
            Switchplayer();
            isuse[b,c] = true;
            texts[b*3+c].text = "O";
            UptdateGrid(b,c,"O");
            
            

            }
            
            
            
        }
    }}
    public bool Draw(string[,] grid){
        foreach (var item in grid)
        {
            if(item == ""){
                return false;
            }
            
        }
        return true;
    }
    // Update is called once per frame
    public string GetcurrenPlayer(){
        return playerturn ? "X" : "O";
    }

    public bool CheckForWin(string[,] grid, string playerSymbol)
    {
        // Vérification des lignes
        for (int row = 0; row < 3; row++)
        {
            if (grid[row, 0] == playerSymbol && grid[row, 1] == playerSymbol && grid[row, 2] == playerSymbol)
                return true;
        }

        // Vérification des colonnes
        for (int col = 0; col < 3; col++)
        {
            if (grid[0, col] == playerSymbol && grid[1, col] == playerSymbol && grid[2, col] == playerSymbol)
                return true;
        }

        // Vérification de la diagonale principale
        if (grid[0, 0] == playerSymbol && grid[1, 1] == playerSymbol && grid[2, 2] == playerSymbol)
            return true;

        // Vérification de l'autre diagonale
        if (grid[0, 2] == playerSymbol && grid[1, 1] == playerSymbol && grid[2, 0] == playerSymbol)
            return true;

        // Aucune victoire trouvée
        return false;
    }

    
}
