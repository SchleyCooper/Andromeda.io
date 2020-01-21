using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviourPun
{
    public List<Weapon> weaponsList;
    public Transform firePosition;

    private int currentWeapon = 0;
    private List<Transform> bulletPools;

    private void Awake()
    {
        //// spawn pools here or in Start?
        //for (int i = 0; i < weaponsList.Count; i++)
        //{
        //    GameObject container = new GameObject(weaponsList[i].name + "s");
        //    container.transform.SetParent(this.transform);
        //    Transform[] children = new Transform[50];
        //    bulletPools.Add(container.transform);
        //}
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Jump") || Input.GetMouseButton(0))
        {
            photonView.RPC("Shoot", RpcTarget.All); // SHOULD SEND TO MASTER...?
        }
    }

    [PunRPC]
    void Shoot()
    {
        // pull from weapon's bullet pool & 'instantiate'/enable it and launch it
        //Instantiate(weaponsList[currentWeapon].bullet, firePosition.position, firePosition.rotation);
    }
}
