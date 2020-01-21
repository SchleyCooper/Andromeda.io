using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PUNTutorial2D
{
    public class PhotonManager : MonoBehaviourPunCallbacks
    {
        [SerializeField]
        private GameObject player;
        [SerializeField]
        private GameObject lobbyCamera;

        // Start is called before the first frame update
        void Start()
        {
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            base.OnConnectedToMaster();
            PhotonNetwork.JoinLobby();
        }

        public override void OnJoinedLobby()
        {
            base.OnJoinedLobby();
            PhotonNetwork.JoinOrCreateRoom("Room", new Photon.Realtime.RoomOptions { MaxPlayers = 2 }, TypedLobby.Default);
            Debug.Log("JOINED LOBBY");
        }

        public override void OnJoinedRoom()
        {
            PhotonNetwork.Instantiate("Test/PUN 2D Tutorial/PlayerSquare", player.transform.position, Quaternion.identity, 0);
            lobbyCamera.SetActive(false);
            Debug.Log("JOINED ROOM");
        }
    } 
}
