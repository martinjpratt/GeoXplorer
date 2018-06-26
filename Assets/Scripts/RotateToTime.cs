//Written by Martin Pratt, Fossett Lab for Virtual Planetary Exploration
//Washington University in St. Louis
//
//June 2018

using UnityEngine;
using System;

public class RotateToTime : MonoBehaviour {

    //Rotate the Earth so the location that is currently closest to midday is plotted towards the user on startup

    public GameObject resetButton;

	// Use this for initialization
	void Start () {
        float hh = DateTime.UtcNow.Hour;
        float mm = DateTime.UtcNow.Minute;
        float rotAngle = ((180-(hh - (mm / 60))) / 24) * 360;
        transform.Rotate(new Vector3(0, rotAngle, 0), Space.Self);

        resetButton.GetComponent<TapReset>().originalPosition = transform.eulerAngles;
        resetButton.GetComponent<TapReset>().resetObject = this.gameObject;
	}
	
}
