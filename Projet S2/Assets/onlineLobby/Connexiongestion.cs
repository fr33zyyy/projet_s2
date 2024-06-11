using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine.SceneManagement;

public class Connexiongestion : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    public InputField nameInputField;
    public Button button1;
    private string pseudo = "";
    public Image load;
    public Text empty;
    void Start()
    {
        Debug.Log("yo");
        PhotonNetwork.ConnectUsingSettings();
        button1.onClick.AddListener(OnButtonClick1);
    }
    public override void OnConnectedToMaster(){
        Destroy(load);
    }
     public void OnSubmit()
    {
        pseudo = nameInputField.text;
        PhotonNetwork.NickName = pseudo;
        if(pseudo != ""){
            empty.text = "";
        }
    }
    void OnButtonClick1(){
        if(pseudo == ""){
            empty.text = "Please submit a nickname!";
            return;
        }
        else{
        Hashtable expectedCustomRoomProperties = new Hashtable();
        expectedCustomRoomProperties.Add("GameType", "TicTac");

        // Tente de rejoindre une salle existante ou de créer une nouvelle salle avec les propriétés spécifiées
        PhotonNetwork.JoinOrCreateRoom("RoomName", new Photon.Realtime.RoomOptions { MaxPlayers = 2, CustomRoomProperties = expectedCustomRoomProperties, CustomRoomPropertiesForLobby = new string[] { "GameType" } }, null);
        SceneManager.LoadScene("waittic");
        }
    }
}
