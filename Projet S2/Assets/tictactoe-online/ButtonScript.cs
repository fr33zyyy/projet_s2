using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellButton : MonoBehaviour
{
    public int cellIndex;
    public GameManager gameManager;

    public void Start(){
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }
    public void OnClick()
    {
        Debug.Log("click");
        if (gameManager != null)
        {
            gameManager.OnCellButtonClick(cellIndex);
        }
    }
}
