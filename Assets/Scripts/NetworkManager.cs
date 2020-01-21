using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static List<NetworkPlayer> players { get; internal set; } = new List<NetworkPlayer>();

    private void Awake()
    {
        //players = new List<NetworkPlayer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ReorderPlayers();
    }

    void ReorderPlayers()
    {
        players = players.OrderByDescending(p => p.Score).ToList();
    }

    public static void AddPlayer(NetworkPlayer player)
    {
        players.Add(player);
    }

}
