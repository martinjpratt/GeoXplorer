//Written by Martin Pratt, Fossett Lab for Virtual Planetary Exploration
//Washington University in St. Louis
//
//October 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToContext : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadContext()
    {
        GameObject modelObject = GameObject.FindGameObjectWithTag("scalable");
        Renderer[] modrends = modelObject.GetComponentsInChildren<Renderer>();
        foreach (var rend in modrends)
        {
            rend.enabled = false;
        }
        Collider[] modcols = modelObject.GetComponentsInChildren<Collider>();
        foreach (var col in modcols)
        {
            col.enabled = false;
        }


        GameObject contextStage = GameObject.FindGameObjectWithTag("contextStage");
        Renderer[] conrends = contextStage.GetComponentsInChildren<Renderer>();
        foreach (var rend  in conrends)
        {
            rend.enabled = true;
        }
        Collider[] concols = contextStage.GetComponentsInChildren<Collider>();
        foreach (var col in concols)
        {
            col.enabled = true;
        }

        GameObject contextLoader = GameObject.FindGameObjectWithTag("GameController");
        if (!contextLoader.GetComponent<MapBuilder>().enabled)
        {
            contextLoader.GetComponent<MapBuilder>().enabled = true;
        }
        else
        {
            contextLoader.GetComponent<MapBuilder>().ShowMap();
        }
    }

    public void HideContext()
    {
        GameObject modelObject = GameObject.FindGameObjectWithTag("scalable");
        Renderer[] modrends = modelObject.GetComponentsInChildren<Renderer>();
        foreach (var rend in modrends)
        {
            rend.enabled = true;
        }
        Collider[] modcols = modelObject.GetComponentsInChildren<Collider>();
        foreach (var col in modcols)
        {
            col.enabled = true;
        }


        GameObject contextStage = GameObject.FindGameObjectWithTag("contextStage");
        Renderer[] conrends = contextStage.GetComponentsInChildren<Renderer>();
        foreach (var rend in conrends)
        {
            rend.enabled = false;
        }
        Collider[] concols = contextStage.GetComponentsInChildren<Collider>();
        foreach (var col in concols)
        {
            col.enabled = false;
        }
    }
}
