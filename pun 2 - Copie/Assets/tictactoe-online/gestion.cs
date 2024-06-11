using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using System.Diagnostics.Tracing;

public class gestion : MonoBehaviourPunCallbacks
{
    public string nameInputField; // Référence à l'InputField où le joueur saisit son pseudonyme
    public Text playerListText; // Référence au Text qui affiche la liste des joueurs dans la salle
    private int playernumber;
    public Button button;
    private bool allPlayersReady = false;
    public Image image; 

    // Start is called before the first frame update
    void Start()
    {
         Hashtable hash = new Hashtable();
        hash.Add("Ready", false); // Définit la propriété personnalisée "Ready" du joueur
        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        DisplayPlayerList();
        button.onClick.AddListener(OnButtonClick);
    }

    

    void DisplayPlayerList()
    {
        if (PhotonNetwork.CurrentRoom != null)
        {
            playernumber = PhotonNetwork.PlayerList.Length;
            playerListText.text = "Players in the room:\n";
            foreach (Player player in PhotonNetwork.PlayerList)
            {
                string playerName = player.NickName;
                if (player.CustomProperties.ContainsKey("Ready") && (bool)player.CustomProperties["Ready"])
                {
                    playerName += " (Ready)"; // Ajoute "(Ready)" à côté du pseudo si le joueur est prêt
                }
                playerListText.text += playerName + "\n"; // Ajoute le pseudo (et éventuellement "(Ready)") à la liste
            }
        }
    }

    void Update(){
        if(PhotonNetwork.PlayerList.Length != playernumber){
            DisplayPlayerList();
        }
    }

    void OnButtonClick()
    {
        // Définit la propriété personnalisée "Ready" du joueur
        Hashtable playerProperties = PhotonNetwork.LocalPlayer.CustomProperties;
        playerProperties["Ready"] = true;
        PhotonNetwork.LocalPlayer.SetCustomProperties(playerProperties);
    }
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        Debug.Log("qqn est pas pret");
        allPlayersReady = true;
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            if (!player.CustomProperties.ContainsKey("Ready") || !(bool)player.CustomProperties["Ready"])
            {
                allPlayersReady = false; // Si un joueur n'est pas prêt, tous les joueurs ne sont pas prêts
                break;
            }
        }
        if(PhotonNetwork.PlayerList.Length != 2){
            allPlayersReady = false;
        }
        if(allPlayersReady == true){
            Debug.Log("tout le monde est pret");
            GameStart();
        }
        DisplayPlayerList();
    }

    void GameStart(){
        Debug.Log("lancemant du jeu");
        PhotonNetwork.LoadLevel("secenejeu");
    }
}