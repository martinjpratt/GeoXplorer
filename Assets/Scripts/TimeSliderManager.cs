//Written by Martin Pratt, Fossett Lab for Virtual Planetary Exploration
//Washington University in St. Louis
//
//June 2018

using UnityEngine;
using System;

public class TimeSliderManager : MonoBehaviour {

    //Controls the time slider menu text

    DateTime startDate = new DateTime(1970,01,01);
    DateTime date = DateTime.Now;
    public GameObject startSphere;
    public GameObject endSphere;
    public GameObject selectionCylinder;
    public GameObject startTimeText;
    public GameObject endTimeText;
    float dayDelta;

    // Use this for initialization
    void Start () {
        TimeSpan duration = date - startDate;
        dayDelta = 2f / duration.Days;
        startTimeText.GetComponent<TextMesh>().text = startDate.ToString("dd/MM/yyyy");
        endTimeText.GetComponent<TextMesh>().text = date.ToString("dd/MM/yyyy");
	}
	
	// Update is called once per frame
	void Update () {
        float startPos = startSphere.transform.localPosition.x;
        float endPos = endSphere.transform.localPosition.x;
        float halfWay = (endPos + startPos) / 2f;
        float halfScale = (endPos - startPos) / 2f;
        selectionCylinder.transform.localPosition = new Vector3(halfWay,0,0);
        selectionCylinder.transform.localScale = new Vector3(selectionCylinder.transform.localScale.x, halfScale, selectionCylinder.transform.localScale.z);

        float newStartPos = startSphere.transform.localPosition.x + 1f;
        float startDays = newStartPos / dayDelta;
        DateTime newStartDate = startDate.AddDays(startDays);
        startTimeText.GetComponent<TextMesh>().text = newStartDate.ToString("dd/MM/yyyy");

        float newEndPos = endSphere.transform.localPosition.x - 1f;
        float endDays = newEndPos / dayDelta;
        DateTime newEndDate = date.AddDays(endDays);
        endTimeText.GetComponent<TextMesh>().text = newEndDate.ToString("dd/MM/yyyy");
    }
}
