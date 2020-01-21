using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorkTest.AndromedaIO
{
    public class ShipController : MonoBehaviour
    {
        #region Public Fields

        public float maxVelocityNormal = 5.0f;
        public float maxVelocityBoost = 7.0f;
        public float rotationSpeed = 2.0f;

        #endregion

        #region Private Fields

        private Rigidbody rb;
        private Animator anim;

        private float horizontalInput, verticalInput;
        private bool isBoosting;

        #endregion

        #region MonoBehaviour API

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            anim = GetComponent<Animator>();
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            GetInput();
            ThrustForward();
            Rotate();
        }

        #endregion

        #region Controller Methods

        private void GetInput()
        {
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");

            isBoosting = Input.GetButton("Boost");
        }

        private void ClampVelocity()
        {
            // Playfield on X-Z plane, so clamp X and Z movement
            float x = Mathf.Clamp(rb.velocity.x, -maxVelocityNormal, maxVelocityNormal);
            float z = Mathf.Clamp(rb.velocity.z, -maxVelocityNormal, maxVelocityNormal);

            rb.velocity = new Vector3(x, 0, z);
        }

        private void ThrustForward()
        {
            rb.AddForce(transform.forward * verticalInput);
            ClampVelocity();
        }

        private void Rotate()
        {
            // Playfield on X-Z plane, so rotate about the Y-axis
            transform.Rotate(0, rotationSpeed * horizontalInput, 0);

            // Roll the ship for more realistic turning visual
            anim.SetFloat("Roll", horizontalInput);
        }

        #endregion
    } 
}
