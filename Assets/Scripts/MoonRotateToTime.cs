//Written by Martin Pratt, Fossett Lab for Virtual Planetary Exploration
//Washington University in St. Louis
//
//June 2018

using UnityEngine;
using System;

public class MoonRotateToTime : MonoBehaviour {

    //Places the moon in the correct current monthly phase

    float period = 29.530589f;
    float radius = 5;
    DateTime startTime = DateTime.Parse("2017-09-06T08:03");

	// Use this for initialization
	void Start () {
        DateTime dateNow = DateTime.Now;
        
        double totalDays = (dateNow-startTime).TotalDays;

        float numberOfPeriods = (float)totalDays / period;

        float x = radius * Mathf.Cos(-numberOfPeriods * 2 * Mathf.PI);
        float y = radius * Mathf.Sin(-numberOfPeriods * 2 * Mathf.PI);

        transform.localPosition = new Vector3(y, 0, x);
        transform.Rotate(0,  (Mathf.Rad2Deg * -numberOfPeriods * 2 * Mathf.PI), 0,Space.Self);
    }
}
