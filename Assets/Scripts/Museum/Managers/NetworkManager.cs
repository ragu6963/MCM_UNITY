using Photon.Pun;
using Photon.Realtime;
using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    private string gameVerstion = "1";
    private string RoomName;
    private string CharacterName; 

    [DllImport("__Internal")]
    private static extern void JoinGame(string nickname, string guid);

    private void Awake()
    {
        RoomName = "ROOM";
        this.CharacterName = LobbyManager.Instance.CharacterName;
        PhotonNetwork.NickName = LobbyManager.Instance.NickName;
        PhotonNetwork.GameVersion = gameVerstion;
        PhotonNetwork.ConnectUsingSettings();

    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinOrCreateRoom(RoomName, new RoomOptions { MaxPlayers = (byte)LobbyManager.Instance.MaxPlayers }, null);
    }

    public override void OnJoinedRoom()
    {
        string CharacterNamePath = "Character/prefabs/" + this.CharacterName;
        PhotonNetwork.Instantiate(CharacterNamePath, new Vector3(-86,2,-72), Quaternion.identity);
        PlayerManager.Instance.guid = Guid.NewGuid();

#if !UNITY_EDITOR && UNITY_WEBGL
        JoinGame(LobbyManager.Instance.NickName, PlayerManager.Instance.guid.ToString());
#endif
    }

}
