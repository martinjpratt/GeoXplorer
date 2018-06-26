//Written by Martin Pratt, Fossett Lab for Virtual Planetary Exploration
//Washington University in St. Louis
//
//June 2018

using UnityEngine;

public class TurnOfEarth : MonoBehaviour {

    //Code to hide Earth objects on click

    public void SwitchCurrentEarthOff()
    {
        GameObject earth = GameObject.FindGameObjectWithTag("Earth");
        if (earth)
        {
            earth.SetActive(false);
        }
        GameObject intEarth = GameObject.FindGameObjectWithTag("InteriorEarth");
        if (intEarth)
        {
            intEarth.SetActive(false);
        }
        GameObject NASAGIBSEarth = GameObject.FindGameObjectWithTag("NASAGIBSEarth");
        if (NASAGIBSEarth)
        {
            NASAGIBSEarth.SetActive(false);
        }
    }
    
}
