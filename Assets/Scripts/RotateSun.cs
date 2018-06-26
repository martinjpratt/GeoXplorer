//Written by Martin Pratt, Fossett Lab for Virtual Planetary Exploration
//Washington University in St. Louis
//
//June 2018

using UnityEngine;
using System;

public class RotateSun : MonoBehaviour
{
    //Rotate sun to current incidence angle

    private Vector3 targetAngle;
    
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

        targetAngle = new Vector3 (maxSunAngle * Mathf.Cos(numberOfPeriods * 2 * Mathf.PI), 0 ,0);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.eulerAngles != targetAngle)
        {
            currentAngle = new Vector3(
            Mathf.LerpAngle(currentAngle.x, targetAngle.x, Time.deltaTime / 2f),
            Mathf.LerpAngle(currentAngle.y, targetAngle.y, Time.deltaTime / 2f),
            Mathf.LerpAngle(currentAngle.z, targetAngle.z, Time.deltaTime / 2f));

            transform.eulerAngles = currentAngle;
        }
    }
}
