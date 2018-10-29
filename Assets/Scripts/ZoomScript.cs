using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomScript : MonoBehaviour {

    GameObject modelStage;
    TextMesh ZoomLevelText;

    public void ZoomIn()
    {
        int oldZoomLevel = modelStage.GetComponent<MapBuilder>().ZoomLevel;
        modelStage.GetComponent<MapBuilder>().ZoomLevel = oldZoomLevel + 1;
        modelStage.GetComponent<MapBuilder>().ShowMap();
        ZoomLevelText.text = "Zoom Level: " + modelStage.GetComponent<MapBuilder>().ZoomLevel.ToString();
    }

    public void ZoomOut()
    {
        int oldZoomLevel = modelStage.GetComponent<MapBuilder>().ZoomLevel;
        modelStage.GetComponent<MapBuilder>().ZoomLevel = oldZoomLevel - 1;
        modelStage.GetComponent<MapBuilder>().ShowMap();
        ZoomLevelText.text = "Zoom Level: " + modelStage.GetComponent<MapBuilder>().ZoomLevel.ToString();
    }

    // Use this for initialization
    void Start () {
        modelStage = GameObject.FindGameObjectWithTag("GameController");
        ZoomLevelText = GameObject.FindGameObjectWithTag("ZoomIndicator").GetComponent<TextMesh>();
        ZoomLevelText.text = "Zoom Level: " + modelStage.GetComponent<MapBuilder>().ZoomLevel.ToString();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
