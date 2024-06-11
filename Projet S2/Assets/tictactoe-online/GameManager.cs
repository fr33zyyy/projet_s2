using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEditor;

public class GameManager : MonoBehaviourPunCallbacks
{
    private bool myturn;
    public Text statusText;
    public Text turnText;
    public GameObject[] cellButtons;
    private string[] playerMarks = { "X", "O" };
    private int currentPlayerIndex = 0;
    private string[,] grid = new string[3, 3];
    private bool gameEnded = false;
    public Text yoursign;
    private int Xwin = 0;
    private int Ywin = 0;
    public Text Xvict;
    public Text Yvict;
    void Start()
    {
        statusText.text = "Game is ongoing";
        turnText.text = "Player " + playerMarks[currentPlayerIndex] + "'s turn";
        if (PhotonNetwork.PlayerList[0].IsLocal)
            {
                myturn = true;
                Debug.Log("Vous êtes le joueur 1.");
                yoursign.text = "X";
            }
        else{
            myturn = false;
            yoursign.text = "O";
        }
        
        InitializeGrid();
    }

    void InitializeGrid()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                grid[i, j] = "";
            }
        }
        foreach (var item in cellButtons)
        {
            item.GetComponentInChildren<Text>().text = "";
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        statusText.text = "Other player left the room. You win!";
        gameEnded = true;
    }

    public void OnCellButtonClick(int index)
    {
        if(!myturn){
            return;
        }
        if (gameEnded)
            return;

        int row = index / 3;
        int col = index % 3;

        if (grid[row, col] == "")
        {
            grid[row, col] = playerMarks[currentPlayerIndex];
            photonView.RPC("UpdateCellButton", RpcTarget.AllBuffered, index, playerMarks[currentPlayerIndex]);

            
        }
    }

    [PunRPC]
    void UpdateCellButton(int index, string mark)
    {
        grid[index/3,index%3] = mark;
        cellButtons[index].GetComponentInChildren<Text>().text = mark;
        if (CheckForWin(playerMarks[currentPlayerIndex]))
            {
                if(mark == "X"){
                    Xwin += 1;
                    Xvict.text = "X: " + Xwin;
                }
                else{
                    Ywin += 1;
                    Yvict.text = "O: " + Ywin;
                }
                InitializeGrid();
            }
            else if (CheckForDraw())
            {
                InitializeGrid();
            }
        currentPlayerIndex = 1 - currentPlayerIndex;
        myturn = !myturn;
        turnText.text = "Player " + playerMarks[currentPlayerIndex] + "'s turn";
        
    }

    bool CheckForWin(string mark)
    {
        // Check rows
        for (int i = 0; i < 3; i++)
        {
            if (grid[i, 0] == mark && grid[i, 1] == mark && grid[i, 2] == mark)
                return true;
        }

        // Check columns
        for (int i = 0; i < 3; i++)
        {
            if (grid[0, i] == mark && grid[1, i] == mark && grid[2, i] == mark)
                return true;
        }

        // Check diagonals
        if ((grid[0, 0] == mark && grid[1, 1] == mark && grid[2, 2] == mark) ||
            (grid[0, 2] == mark && grid[1, 1] == mark && grid[2, 0] == mark))
            return true;

        return false;
    }

    bool CheckForDraw()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (grid[i, j] == "")
                    return false;
            }
        }
        return true;
    }
}