using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkPlayer : MonoBehaviourPun, IPunObservable
{
    public static NetworkPlayer LocalPlayerInstance;

    public int Score { get { return score; } }
    public float Health { get { return health; } }

    [SerializeField]
    public int score { get; internal set; }
    [SerializeField]
    public int health { get; internal set; }
    [SerializeField]
    public int currentDust { get; internal set; }

    [SerializeField]
    private MonoBehaviour[] scriptsToIgnore;

    private SpriteRenderer  spriteRenderer;
    private Camera          playerCamera;

    private PlayerInfo      playerInfo;
    private ShipController  shipController;
    public PlayerHealth     playerHealth { get; internal set; }

    private void Awake()
    {
        playerInfo = GetComponent<PlayerInfo>();
        playerCamera = GetComponentInChildren<Camera>();
        playerHealth = GetComponent<PlayerHealth>();

        if (photonView.IsMine)
        {
            LocalPlayerInstance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Initialize()
    {
        if (photonView.IsMine)
        {

        }
        else
        {
            // if player does not control the object, then disable all the necessary control elements
            playerCamera.gameObject.SetActive(false);
            foreach (MonoBehaviour script in scriptsToIgnore)
            {
                script.enabled = false;
            }
        }

        // add to NetworkManager db
        NetworkManager.AddPlayer(this);

        // set player properties
        score = 0;
        health = playerHealth.maxHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            // make sure colliding with planet
            if (other.tag == "Planet")
            {
                // make sure planet matches assigned planet
                //if (other.name == Player.planetIdx;
                //{
                //      Cache collected dust
                //}
            }
            else if (other.tag == "Dust")
            {
                photonView.RPC("RPCAddDust", RpcTarget.AllViaServer, 10);
            }
            else if (other.tag == "Bullet")
            {
                //Bullet bullet = other.GetComponent<Bullet>();
                //      Check which type of bullet
                //          Do damage based on type of bullet
                photonView.RPC("RPCApplyDamage", RpcTarget.AllViaServer, 11);
            } 
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(score);
            stream.SendNext(health);
            stream.SendNext(currentDust);
        }
        else
        {
            this.score = (int)stream.ReceiveNext();
            this.health = (int)stream.ReceiveNext();
            this.currentDust = (int)stream.ReceiveNext();
        }
    }

    [PunRPC]
    private void RPCApplyDamage(int damage)
    {
        Debug.Log("ApplyDamage to " + photonView.Owner.NickName);
        health -= damage;
    }

    [PunRPC]
    private void RPCAddDust(int value)
    {
        Debug.Log("AddDust to " + photonView.Owner.NickName);
        score += value;
        currentDust += value;
    }
}
