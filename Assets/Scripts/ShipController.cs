using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour {

    #region Public Fields

    public float fwdSpeed = 2.0f;
    public float boostMult = 2.0f;
    public float rotSpeed = 2.0f;
    public float tiltMult = 2.0f;



    #endregion

    #region Private Fields

    private float hIn;
    private float vIn;

    private Animator    anim;
    private Rigidbody   rb;
    private Transform   sprite;
    private Sprite      shipSprite;

    private Camera      followCam;

    #endregion

    #region MonoBehaviour API

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        sprite = transform.Find("Sprite");
        shipSprite = sprite.GetComponent<Sprite>();
        followCam = GetComponentInChildren<Camera>();
        Debug.Log(sprite.transform.localEulerAngles);
	}
	
	// Update is called once per frame
	void Update () {
        hIn = Input.GetAxis("Horizontal");
        vIn = Input.GetAxis("Vertical");

        //// Transform movement
        //transform.Translate(new Vector3(vIn * fwdSpeed, 0, 0), Space.Self);
        //transform.Rotate(new Vector3(0, 0, hIn * rotSpeed), Space.Self);

        // Rigidbody movement
        rb.AddForce(transform.forward * vIn * (Input.GetKey(KeyCode.LeftShift) ? boostMult * fwdSpeed : fwdSpeed), ForceMode.Acceleration);
        rb.AddTorque(new Vector3(0, hIn * rotSpeed, 0), ForceMode.Acceleration);

        Lean();

        //camera.transform.SetPositionAndRotation(camera.transform.position, new Quaternion(0,0,0,0));
    }

    #endregion

    #region Controller Methods

    // rotation lean
    private void Lean()
    {
        // yaw sprite with ship rotation
        sprite.transform.localRotation = Quaternion.Euler(
            sprite.transform.localEulerAngles.x, 
            sprite.transform.localEulerAngles.y, 
            sprite.transform.localEulerAngles.z
            ); 
        //Vector3 newRotation = new Vector3(Mathf.LerpAngle(sprite.transform.rotation.x, hIn * 100, Time.deltaTime), 
        //    sprite.transform.rotation.y, sprite.transform.rotation.z);
        ////Quaternion.Lerp(sprite.transform.rotation, new Quaternion(0, hIn, 0, 0), Time.deltaTime);
        ////sprite.GetComponent<Transform>().Rotate(new Vector3(-hIn, 0, 0));
        //sprite.transform.eulerAngles = newRotation;
    }

    #endregion
}
