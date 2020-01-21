using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBelt : MonoBehaviour {

    public float fieldRadius = 500.0f;
    public int numAsteroids = 100;

    public GameObject asteroidPrefab;

    public List<GameObject> asteroids;

    //private Mesh mesh;

    // Use this for initialization
    void Start () {
        //mesh = this.GetComponent<MeshFilter>().mesh;
        Vector3 center = this.transform.localPosition;

	    for (int i = 0; i < numAsteroids; i++)
        {
            // instantiate asteroids as children of asteroid belt
            asteroids.Add(Instantiate(asteroidPrefab,
                RandomPerimeter(center, fieldRadius),
                Quaternion.identity, transform));
            asteroids[i].GetComponent<Rigidbody>().AddTorque(
                Random.Range(1, 11), Random.Range(1, 11), Random.Range(1, 11));
        }
	}

    Vector3 RandomPerimeter(Vector3 center, float radius)
    {
        float ang = Random.value * 360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y;
        pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        return pos;
    }
}
