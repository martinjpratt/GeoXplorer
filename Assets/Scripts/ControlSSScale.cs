using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSSScale : MonoBehaviour {

    float journeyLength = 5f;
    float speed = 1f;
    Vector3 startPos;
    Vector3 endPos;
    bool selected = false;
    float startTime;

    public void zoomToInnerSS()
    {
        startTime = Time.time;
        endPos = Vector3.one;
        startPos = Vector3.one * 0.1f;
        selected = true;
    }

    public void zoomToOuterSS()
    {
        startTime = Time.time;
        startPos = Vector3.one;
        endPos = Vector3.one * 0.1f;
        selected = true;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (selected)
        {
            float distCovered = (Time.time - startTime) * speed;
            
            float fracJourney = distCovered / journeyLength;

            this.transform.localScale = Vector3.Lerp(startPos, endPos, fracJourney);
        }

        
    }
}
