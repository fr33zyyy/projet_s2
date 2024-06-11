
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class leaveOnlinelobby : MonoBehaviourPunCallbacks
{
    public void LoadScene()
    {
        // Charger la scène spécifiée
        
        PhotonNetwork.LocalPlayer.CustomProperties["Ready"] = false;
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("lobbyonline");
    }
}
