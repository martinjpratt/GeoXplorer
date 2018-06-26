//Written by Martin Pratt, Fossett Lab for Virtual Planetary Exploration
//Washington University in St. Louis
//
//June 2018

using UnityEngine;

public class CursorArrow : MonoBehaviour {
    //Code to control a direction arrow attached to the cursor

    public GameObject targetObject;
    Vector3 cursorPosition;
    
	// Update is called once per frame
	void Update () {
        Vector3 targetPosition = targetObject.transform.position;
        cursorPosition = this.transform.parent.transform.position;
        
        Vector3 heading = targetPosition - cursorPosition;
        float distance = heading.magnitude;
        Vector3 directionVector = (heading / distance) * 0.02f;

        this.transform.localPosition = directionVector;
	}
}
