using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CustomWeapon/WeaponStats")]
public class WeaponStats : ScriptableObject
{
    public enum FireType
    {
        NORMAL,
        BURST,
        SPREAD,
    }
    public string   name = "Red";
    public FireType fireType = FireType.NORMAL;
    public int      maxAmmo = 300;
    public int      poolSize = 30;
    public float    fireRate = 0.7f;
    public Color    bulletColor = new Color(0, 1, 0);
    public Sprite   bulletSprite;
    public GameObject bullet;
}
