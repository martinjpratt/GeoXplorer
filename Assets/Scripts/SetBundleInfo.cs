using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Sharing.Tests;
using HoloToolkit.Examples.InteractiveElements;

public class SetBundleInfo : MonoBehaviour {

    public GameObject modelDownloader;
    public string bundleName;
    public string prefabName;
    bool selected = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (this.GetComponent<InteractiveToggle>().IsSelected && !selected)
        {
            StartCoroutine(modelDownloader.GetComponent<LoadHandSamples>().DownloadHandSampleAsset(bundleName, prefabName));
            selected = true;
        }

        if (!this.GetComponent<InteractiveToggle>().IsSelected && selected)
        {
            Destroy(GameObject.Find(prefabName));
            selected = false;
        }
    }
}
