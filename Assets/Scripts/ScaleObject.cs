//Written by Martin Pratt, Fossett Lab for Virtual Planetary Exploration
//Washington University in St. Louis
//
//June 2018

using UnityEngine;

public class ScaleObject : MonoBehaviour {
    
    //Scales a downloaded model

    bool foundObject = false;
    Vector3 startScale;
    Vector3 startPosition;
    public Vector3 endPosition = new Vector3(0,0,0);
    
	
	// Update is called once per frame
	void Update () {
        GameObject targetObject = GameObject.FindGameObjectWithTag("scalable");


        if (targetObject == null)
        {
            foundObject = false;
        }

        if (targetObject != null && foundObject == false)
        {
            startScale = targetObject.transform.localScale;
            startPosition = targetObject.transform.position;
            foundObject = true;
        }

        if (foundObject == true)
        {
            float scaleValue = float.Parse(this.GetComponent<TextMesh>().text);

            targetObject.transform.localScale = startScale / scaleValue;
            targetObject.transform.position = Vector3.Lerp(startPosition, endPosition, scaleValue / 10);
        }

	}
}
