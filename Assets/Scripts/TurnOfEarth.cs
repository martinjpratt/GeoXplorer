//Written by Martin Pratt, Fossett Lab for Virtual Planetary Exploration
//Washington University in St. Louis
//
//June 2018

using UnityEngine;
using System;

public class TurnOfEarth : MonoBehaviour {

    //Code to hide Earth objects on click

    public void SwitchCurrentEarthOff()
    {
        GameObject earth = GameObject.FindGameObjectWithTag("Earths");
        
        if (earth)
        {
            Vector3 newPosition = earth.transform.localPosition;
            earth.GetComponent<MoveToMars>().GetLocations(newPosition);
        }
    }
    
}
