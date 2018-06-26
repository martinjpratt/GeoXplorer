//Written by Martin Pratt, Fossett Lab for Virtual Planetary Exploration
//Washington University in St. Louis
//
//June 2018

using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class MoveToMars : MonoBehaviour, IInputClickHandler {
    
    //Moves an object to the center of the viewing area, was originally just used for Mars, but things have changed!

    public GameObject otherPlanet1;
    public GameObject otherPlanet2;
    Transform thisTransform;
    Transform otherPlanet1Transform;
    Transform otherPlanet2Transform;
    Vector3 newPrimaryStart;
    Vector3 otherPlanet1Start;
    Vector3 otherPlanet2Start;

    Vector3 moonScale;
    Vector3 earthScale;
    Vector3 marsScale;
    float speed = 2.0F;
    float startTime;
    float journeyLength;
    bool isStarted = false;

    Vector3 newPostition;
    Vector3 centralPostition = Vector3.zero;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        newPostition = GazeManager.Instance.HitObject.transform.localPosition;
        GetLocations(); 
    }

    private void GetLocations()
    {
        thisTransform = this.transform;
        otherPlanet1Transform = otherPlanet1.transform;
        otherPlanet2Transform = otherPlanet2.transform;

        newPrimaryStart = this.transform.localPosition;
        otherPlanet1Start = otherPlanet1.transform.localPosition;
        otherPlanet2Start = otherPlanet2.transform.localPosition;
        earthScale = this.transform.localScale;
        moonScale = otherPlanet1.transform.localScale;
        marsScale = otherPlanet2.transform.localScale;

        journeyLength = Vector3.Distance(centralPostition, newPostition);
        startTime = Time.time;

        isStarted = true;
    }
    
	// Update is called once per frame
	void Update () {
        if (isStarted)
        {
            float distCovered = (Time.time - startTime) * speed;
            float fracJourney = distCovered / journeyLength;

            thisTransform.localPosition = Vector3.Lerp(newPrimaryStart, centralPostition, fracJourney);

            otherPlanet1Transform.localPosition = Vector3.Lerp(otherPlanet1Start, otherPlanet1Start - newPrimaryStart, fracJourney);
            otherPlanet2Transform.localPosition = Vector3.Lerp(otherPlanet2Start, otherPlanet2Start - newPrimaryStart, fracJourney);
            
            if (fracJourney > 1)
            {
                isStarted = false;
                this.GetComponent<CreateIcons>().enabled = true;
                this.GetComponent<MoveToMars>().enabled = false;
                this.GetComponent<HandRotate>().enabled = true;
                otherPlanet1.GetComponent<HandRotate>().enabled = false;
                otherPlanet1.GetComponent<MoveToMars>().enabled = true;
                otherPlanet2.GetComponent<HandRotate>().enabled = false;
                otherPlanet2.GetComponent<MoveToMars>().enabled = true;
            }
        }
    }
}
