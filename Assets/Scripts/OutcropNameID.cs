//Written by Martin Pratt, Fossett Lab for Virtual Planetary Exploration
//Washington University in St. Louis
//
//October 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutcropNameID : MonoBehaviour {

    public float Latitude;
    public float Longitude;
    public string modelName;
    public string prefabName;
    public string bundleString;
    public string authorName;
    public bool selected;
    Color originalColor;

    private void Start()
    {
        originalColor = this.GetComponent<Renderer>().material.color;
    }

    public void iconSelected()
    {
        this.GetComponent<Renderer>().material.color = Color.blue;
        selected = true;
    }

    public void iconDeselected()
    {
        this.GetComponent<Renderer>().material.color = originalColor;
        selected = false;
    }
}
