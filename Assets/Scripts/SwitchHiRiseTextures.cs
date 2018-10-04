using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchHiRiseTextures : MonoBehaviour {

    public TextMesh maxHeightText;
    public TextMesh minHeightText;

    public void SwitchColorAltimetry()
    {
        GameObject go = GameObject.FindGameObjectWithTag("scalable");
        
        go.GetComponent<UVMapper>().surfaceType = "cb";
        go.GetComponent<UVMapper>().FetchTexture();

        maxHeightText.text = go.GetComponent<UVMapper>().maxHeight;
        minHeightText.text = go.GetComponent<UVMapper>().minHeight;
    }
    

    public void SwitchSatelliteImage()
    {

        GameObject go = GameObject.FindGameObjectWithTag("scalable");
        go.GetComponent<UVMapper>().surfaceType = "sb";
        go.GetComponent<UVMapper>().FetchTexture();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
