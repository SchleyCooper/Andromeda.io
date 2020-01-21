using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class UIManager : MonoBehaviourPun {

    GameObject hud;
    GameObject pauseMenu;
    GameObject storeMenu;
    GameObject playerInfo;
    
    // Leaderboard
    GameObject leaderboard;
    TextMeshProUGUI[] leaderboardPositions;

    // Player data
    Slider healthbar;

    TextMeshProUGUI coordinates;

    Player player;

    private bool isPaused;

	// Use this for initialization
	void Awake () {
        // main overlays
        hud = GameObject.Find("HUD");
        pauseMenu = GameObject.Find("PauseMenu");
        storeMenu = GameObject.Find("StoreMenu");

        leaderboard = hud.transform.Find("Leaderboard").gameObject;
        leaderboardPositions = leaderboard.GetComponentsInChildren<TextMeshProUGUI>();
        coordinates = hud.transform.Find("Coordinates").GetComponent<TextMeshProUGUI>();

        healthbar = hud.transform.Find("HealthBar").GetComponent<Slider>();

        // world UI
        //playerInfo = GameObject.Find("PlayerInfo");    // FIX TO GET ALL PI's
	}

    private void OnEnable()
    {
        hud.SetActive(true);
        pauseMenu.SetActive(false);
        storeMenu.SetActive(false);

        //playerInfo = NetworkPlayer.LocalPlayerInstance.gameObject;    // FIX TO GET ALL PI's

    }

    private void Start()
    {
        healthbar.value = healthbar.maxValue = NetworkPlayer.LocalPlayerInstance.playerHealth.maxHealth;
    }

    // Update is called once per frame
    void Update () {
        if (!isPaused)
        {
            //if (Input.GetKeyDown(KeyCode.Tab) && !playerInfo.activeSelf)
            //{
            //    playerInfo.SetActive(true);
            //}
            //else if (Input.GetKeyDown(KeyCode.Tab) && playerInfo.activeSelf)
            //{
            //    playerInfo.SetActive(false);
            //}
            //else 
            if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
            {
                isPaused = true;
                Pause();
            }
        }
        else if (isPaused)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isPaused = false;
                Unpause();
            }
        }

        coordinates.text = photonView.Owner.NickName + "\n" + transform.position.ToString();

        UpdateLeaderboard();

        UpdatePlayerUI();
	}

    void UpdateLeaderboard()
    {
        // set names + scores on leaderboard (start at 1 to ignore label)
        for (int i = 1; i < leaderboardPositions.Length; i++)
        {
            if (i - 1 < NetworkManager.players.Count)
                leaderboardPositions[i].text = i + ". " + NetworkManager.players[i-1].photonView.Owner.NickName + " (" + NetworkManager.players[i-1].Score + ")";
            else
                leaderboardPositions[i].text = "";  // empty if not enough players to fill
        }
    }

    void UpdatePlayerUI()
    {
        healthbar.value = NetworkPlayer.LocalPlayerInstance.health;
        Debug.Log("Healthbar = " + healthbar.value + "/" + healthbar.maxValue);
    }

    void Pause()
    {
        hud.SetActive(false);
        pauseMenu.SetActive(true);
        storeMenu.SetActive(false);
    }

    public void Unpause()
    {
        hud.SetActive(true);
        pauseMenu.SetActive(false);
        storeMenu.SetActive(false);
    }

    public void ExitGame()
    {
        PhotonNetwork.LeaveRoom();
    }
}
