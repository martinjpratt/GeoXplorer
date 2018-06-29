using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutRealtimeEarth : MonoBehaviour {

    GameObject targetObject;

	// Use this for initialization
	void Start () {
        targetObject = GameObject.FindGameObjectWithTag("Earth");
	}
	
	// Update is called once per frame
	void Update () {
        float objectDistance = Vector3.Distance(this.transform.position, targetObject.transform.position);
        if (objectDistance < 3 && objectDistance > 2)
        {
            GameObject[] tileObjects = GameObject.FindGameObjectsWithTag("TilePlane");
            foreach (var tileObject in tileObjects)
            {
                tileObject.GetComponent<Renderer>().material.color = new Color(tileObject.GetComponent<Renderer>().material.color.r, tileObject.GetComponent<Renderer>().material.color.g, tileObject.GetComponent<Renderer>().material.color.b, objectDistance - 2f);
            }
        }
	}
}
