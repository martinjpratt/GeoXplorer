//Written by Martin Pratt, Fossett Lab for Virtual Planetary Exploration
//Washington University in St. Louis
//
//June 2018

using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class MoveEarthandMoon : MonoBehaviour, IInputClickHandler {

    //Move planetary objects to the side and to the center of the viewing area

    public GameObject earth;
    public GameObject moon;
    Transform earthTransform;
    Transform moonTransform;
    Vector3 moonStart;
    Vector3 earthStart;
    Vector3 moonScale;
    Vector3 earthScale;
    float speed = 2.0F;
    float startTime;
    float journeyLength;
    bool isStarted = false;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        GetLocations();
    }

    private void GetLocations()
    {
        earthTransform = earth.transform;
        moonTransform = moon.transform;

        earthStart = earth.transform.position;
        moonStart = moon.transform.position;
        earthScale = earth.transform.localScale;
        moonScale = moon.transform.localScale;
        journeyLength = Vector3.Distance(earthStart, moonStart);
        startTime = Time.time;

        isStarted = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (isStarted)
        {
            float distCovered = (Time.time - startTime) * speed;
            float fracJourney = distCovered / journeyLength;
            earthTransform.position = Vector3.Lerp(earthStart, -moonStart, fracJourney);
            moonTransform.position = Vector3.Lerp(moonStart, earthStart, fracJourney);
            earthTransform.localScale = Vector3.Lerp(earthScale, moonScale, fracJourney);
            moonTransform.localScale = Vector3.Lerp(moonScale, earthScale, fracJourney);
            
            if (fracJourney > 1)
            {
                isStarted = false;
                moon.GetComponent<CreateIcons>().enabled = true;
                moon.GetComponent<MoveEarthandMoon>().enabled = false;
                moon.GetComponent<HandRotate>().enabled = true;
                earth.GetComponent<HandRotate>().enabled = false;
                earth.GetComponent<MoveEarthandMoon>().enabled = true;
            }
        }

    }
}
