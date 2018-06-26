//Written by Martin Pratt, Fossett Lab for Virtual Planetary Exploration
//Washington University in St. Louis
//
//June 2018

using UnityEngine;

public class HoloPortTrail : MonoBehaviour {

    //Make the teleport indicator trail have a velocity

    float radius = 0.1f;
    public float speed;
	
	// Update is called once per frame
	void Update () {
        float t = Time.time * speed;
        transform.localPosition = new Vector3(radius * Mathf.Cos(t),0, radius * Mathf.Sin(t));
	}
}
