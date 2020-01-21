using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject playerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        if (NetworkPlayer.LocalPlayerInstance == null)
        {
            PhotonNetwork.Instantiate(playerPrefab.name, 
                new Vector3(Random.Range(200, 800), 0, Random.Range(200, 800)),
                playerPrefab.transform.rotation, 0).GetComponent<NetworkPlayer>(); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        Debug.Log(newPlayer.NickName + " has joined the game");
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        Debug.Log(otherPlayer.NickName + " has left the game");
    }
}
