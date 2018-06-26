//Written by Martin Pratt, Fossett Lab for Virtual Planetary Exploration
//Washington University in St. Louis
//
//June 2018

using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class HighlightSphere : MonoBehaviour, IFocusable {

    //Script to make spheres change color as the cursor passes over them

    Material mat;

    public void OnFocusEnter()
    {
        mat = gameObject.GetComponent<Renderer>().material;
        mat.SetColor("_EmissionColor", Color.magenta);
    }

    public void OnFocusExit()
    {
        mat.SetColor("_EmissionColor", Color.black);
    }
    
}
