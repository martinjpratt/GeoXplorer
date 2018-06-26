//Written by Martin Pratt, Fossett Lab for Virtual Planetary Exploration
//Washington University in St. Louis
//
//June 2018

using System;
using UnityEngine;

public class RotateSun_nonEarthView : MonoBehaviour {
    
    //Rotates the sun to current incidence angle when not looking at the Earth....

    private Vector3 currentAngle;
    float maxSunAngle = 23.43695f;
    float period = 365.256f;
    DateTime startTime = DateTime.Parse("2017-06-21T04:24");

    // Use this for initialization
    void Start()
    {
        DateTime dateNow = DateTime.Now;
        double totalDays = (dateNow - startTime).TotalDays;

        float numberOfPeriods = (float)totalDays / period;


        currentAngle = transform.eulerAngles;

        transform.eulerAngles = new Vector3(maxSunAngle * Mathf.Cos(numberOfPeriods * 2 * Mathf.PI), 0, 0);
    }
}
