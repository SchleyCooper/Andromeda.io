using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PUNTutorial2D
{
    public class PlayerNetworking : MonoBehaviour
    {
        [SerializeField]
        private GameObject playerCamera;
        [SerializeField]
        private MonoBehaviour[] scriptsToIgnore;

        PhotonView photonView;

        // Start is called before the first frame update
        void Start()
        {
            photonView = GetComponent<PhotonView>();
            Initialize();
        }

        void Initialize()
        {
            if (photonView.IsMine)
            {

            }
            else
            {
                playerCamera.SetActive(false);
                foreach (MonoBehaviour item in scriptsToIgnore)
                {
                    item.enabled = false;
                }
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    } 
}
