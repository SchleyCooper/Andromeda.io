using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public SpriteRenderer spriteRenderer;

    ShipController  controller;
    PlayerHealth    health;
    //PlayerHUD       hud;

    public float score;
    public float currentDust;

	// Use this for initialization
	void Start () {
        //spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        controller = GetComponent<ShipController>();
        health = GetComponent<PlayerHealth>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
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
        else if (other.tag == "Bullet")
        {
            //Bullet bullet = other.GetComponent<Bullet>();
            //      Check which type of bullet
            //          Do damage based on type of bullet
        }
    }
}
