using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInfo : MonoBehaviourPun {

    public Vector3 offset;

    private TextMeshProUGUI playerName;
    private TextMeshProUGUI playerScore;

    private NetworkPlayer   networkPlayer;

    Transform   parent;

    private void Awake()
    {
        parent = this.transform.parent;

        playerName = transform.Find("Name").GetComponent<TextMeshProUGUI>();
        playerScore = transform.Find("Score").GetComponent<TextMeshProUGUI>();

        networkPlayer = parent.GetComponent<NetworkPlayer>();
    }

    // Use this for initialization
    void Start () {
        //parent = this.transform.parent;
        playerName.text = networkPlayer.photonView.Owner.NickName;
	}
	
	// Update is called once per frame
	void Update () {
        FollowParent();

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            playerName.enabled = !playerName.enabled;
            playerScore.enabled = !playerScore.enabled;
        }

        playerScore.text = NetworkPlayer.LocalPlayerInstance.Score.ToString();
    }

    // Follow parent in same relative position & rotation
    private void FollowParent()
    {
        // check player rotation to display above or below
        //if (Mathf.Abs(Mathf.DeltaAngle(parent.eulerAngles.y, 0)) <= 90.0f)
        if (Vector3.Dot(parent.forward, Vector3.forward) > 0)
        {
            // above
            this.transform.position = parent.position + offset;
        }
        else
        {
            // below
            this.transform.position = parent.position - offset;
        }

        // reset rotation
        this.transform.eulerAngles = new Vector3(90, 0, 0);
    }

    public void SetPlayerName(string userName)
    {
        playerName.text = userName;
    }
}
