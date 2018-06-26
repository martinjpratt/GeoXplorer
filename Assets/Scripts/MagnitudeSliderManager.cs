//Written by Martin Pratt, Fossett Lab for Virtual Planetary Exploration
//Washington University in St. Louis
//
//June 2018

using System.Collections;
using UnityEngine;

public class MagnitudeSliderManager : MonoBehaviour {

    //Script to control the magnitude menu text

    float minMag = 0f;
    float maxMag = 10f;
    public GameObject startSphere;
    public GameObject endSphere;
    public GameObject selectionCylinder;
    public GameObject startMagText;
    public GameObject endMagText;
    float magDelta;
    
    
    // Use this for initialization
    void Start () {
        float magSpan = maxMag - minMag;
        magDelta = 1f / magSpan;
        startMagText.GetComponent<TextMesh>().text = minMag.ToString();
        endMagText.GetComponent<TextMesh>().text = maxMag.ToString();
    }
	
	// Update is called once per frame
	void Update () {
        float startPos = startSphere.transform.localPosition.x;
        float endPos = endSphere.transform.localPosition.x;
        float halfWay = (endPos + startPos) / 2f;
        float halfScale = (endPos - startPos) / 2f;
        selectionCylinder.transform.localPosition = new Vector3(halfWay, 0, 0);
        selectionCylinder.transform.localScale = new Vector3(selectionCylinder.transform.localScale.x, halfScale, selectionCylinder.transform.localScale.z);

        float newStartPos = startSphere.transform.localPosition.x + 0.5f;
        float startMags = newStartPos / magDelta;
        float newMinMag = minMag + startMags;
        startMagText.GetComponent<TextMesh>().text = newMinMag.ToString("F2");

        float newEndPos = endSphere.transform.localPosition.x - 0.5f;
        float endMags = newEndPos / magDelta;
        float newMaxMag = maxMag + endMags;
        endMagText.GetComponent<TextMesh>().text = newMaxMag.ToString("F2");
    }
}
