//Written by Martin Pratt, Fossett Lab for Virtual Planetary Exploration
//Washington University in St. Louis
//
//June 2018

using UnityEngine;

public class HoloSphereController : MonoBehaviour {
    //Code to animate a teleportation marker

    Color matColor;

	// Use this for initialization
	void Start () {
        matColor = GetComponent<Renderer>().material.color;
    }
	
	// Update is called once per frame
	void Update () {
        transform.localScale = new Vector3(Mathf.PingPong(Time.time, 0.25f), Mathf.PingPong(Time.time, 0.25f), Mathf.PingPong(Time.time, 0.25f));
        matColor = new Color(matColor.r, matColor.g, matColor.b, Mathf.PingPong(Time.time, 1.1f));
    }
}
