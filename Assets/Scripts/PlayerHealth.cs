using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    public int health = 100;
    public int maxHealth { get; internal set; } = 100;

	// Use this for initialization
	void Start () {
        health = 100;
	}
	
	// Update is called once per frame
	void Update () {
		if (health <= 0)
        {
            // destroy ship, play pfx

        }
	}

    // Do damage to the player
    public void DoDamage(int damageVal)
    {
        health -= damageVal;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            //Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            //if (bullet.GetBulletType() == BulletType.RED)
            //{
            //    DoDamage(bullet.damage);
            //}
            //if (bullet.GetBulletType() == BulletType.GREEN)
            //{
            //    DoDamage(bullet.damage);
            //}
            //if (bullet.GetBulletType() == BulletType.BLUE)
            //{
            //    DoDamage(bullet.damage);
            //}
        }
    }
}
