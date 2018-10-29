using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;
using HoloToolkit.Examples.InteractiveElements;

public class ControlTextureAlpha : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void FindTileObjects(float eventData)
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("MapTile");
        Renderer[] rends = new Renderer[tiles.Length];
        eventData = GetComponent<SliderGestureControl>().SliderValue;
        //print(eventData);
        for (int i = 0; i < tiles.Length; i++)
        {
            rends[i] = tiles[i].GetComponent<Renderer>();
        }

        foreach (var rend in rends)
        {
            rend.material.SetFloat("_Blend", eventData);
        }
    }
}
