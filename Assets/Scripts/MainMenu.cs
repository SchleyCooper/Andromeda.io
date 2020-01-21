using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviourPunCallbacks {

    [SerializeField]
    private GameObject      connectionStatus;
    private TextMeshProUGUI connectionText;

    [SerializeField]
    private TMP_InputField  usernameInput;

    private void Awake()
    {
        if (connectionStatus)
        {
            connectionText = connectionStatus.GetComponent<TextMeshProUGUI>();
        }
        else
        {
            connectionStatus.SetActive(false);
        }
    }

    // Use this for initialization
    void Start () {
        if (connectionStatus)
        {
            connectionText.text = "Connecting to Andromeda.io servers...";
        }
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();

        // set username to last setting, if exists
        var name = PlayerPrefs.GetString("Username");
        usernameInput.text = name;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // loads game scene
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // opens next scene
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        connectionText.text = "Connected to Andromeda.io servers";
    }

    public void ConnectToLobby()
    {
        //if (usernameInput && usernameInput.text != "")
        //{
        //    PhotonNetwork.NickName = usernameInput.text;
        //}
        //else
        //{
        //    PhotonNetwork.NickName = "user" + System.Guid.NewGuid().ToString();
        //}

        SetName();
        connectionText.text = "Connecting user " + PhotonNetwork.NickName + " to lobby...";
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        connectionText.text = "Connecting user " + PhotonNetwork.NickName + " to random room...";
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        roomOptions.MaxPlayers = 4;
        string[] propertiesList = { "gameType", "playerList" };
        roomOptions.CustomRoomPropertiesForLobby = propertiesList;
        string roomName = "room_" + System.Guid.NewGuid().ToString();
        connectionText.text = "Attempting to connect to room\n" + roomName + "...";
        PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        connectionText.text = "Joined room\n" + PhotonNetwork.CurrentRoom.Name;
        PhotonNetwork.LoadLevel("Game");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        connectionText.text = "Failed to create room";
    }

    void SetName()
    {
        // run username check first

        PhotonNetwork.NickName = usernameInput.text;
        PlayerPrefs.SetString("Username", PhotonNetwork.NickName);
    }
}
