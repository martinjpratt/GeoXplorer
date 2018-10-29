//Written by Martin Pratt, Fossett Lab for Virtual Planetary Exploration
//Washington University in St. Louis
//
//October 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class TapHiRiseReset : MonoBehaviour {


	public void ResetHiRiseSelection()
	{
		GameObject[] gos = GameObject.FindGameObjectsWithTag ("hiriseLocation");
		foreach (var go in gos) {
			go.GetComponent<HiRiseNameID> ().iconDeselected ();
		}

        GameObject[] outgos = GameObject.FindGameObjectsWithTag("outcropLocation");
        foreach (var go in outgos) {
            go.GetComponent<OutcropNameID>().iconDeselected();
        }

        GameObject[] gos2 = GameObject.FindGameObjectsWithTag ("hiriseIcon");
		foreach (var go in gos2) {
			Destroy (go);
		}

		GameObject mars = GameObject.FindGameObjectWithTag ("Mars");
		mars.GetComponent<HiRiseIconInteraction> ().iconCounter = 0;

        GameObject earth = GameObject.FindGameObjectWithTag("Earth");
        earth.GetComponent<HiRiseIconInteraction>().iconCounter = 0;
    }
    
}
