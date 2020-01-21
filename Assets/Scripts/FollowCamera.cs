using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {

    public enum FollowType
    {
        STATIC,
        ROTATE
    }

    public FollowType followType = FollowType.STATIC;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (followType == FollowType.STATIC)
        {
            this.transform.eulerAngles = new Vector3(90, 0, 0);
        }
    }
}
