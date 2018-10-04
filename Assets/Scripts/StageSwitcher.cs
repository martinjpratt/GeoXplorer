using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSwitcher : MonoBehaviour {

    public GameObject GoToStage;
    public GameObject GoFromStage;
    bool selected = false;
    Vector3 goToPosition;
    Vector3 goFromPosition;
    float startTime;
    float journeyLength;

    public void Initiate()
    {
        goToPosition = GoToStage.transform.localPosition;
        goFromPosition = GoFromStage.transform.localPosition;
        journeyLength = Vector3.Distance(goFromPosition, goToPosition);
        startTime = Time.time;
        selected = true;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (selected)
        {
            
            // Distance moved = time * speed.
            float distCovered = (Time.time - startTime) * 10f;

            // Fraction of journey completed = current distance divided by total distance.
            float fracJourney = distCovered / journeyLength;

            // Set our position as a fraction of the distance between the markers.
            GoFromStage.transform.localPosition = Vector3.Lerp(goFromPosition, goToPosition, fracJourney);
            GoToStage.transform.localPosition = Vector3.Lerp(goToPosition, goFromPosition, fracJourney);
            if (fracJourney> 1)
            {
                selected = false;
            }
        }
		
	}
}
