using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Examples.InteractiveElements;
using HoloToolkit.Sharing.Spawning;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.Sharing.Tests;
using HoloToolkit.Unity.Collections;

public class BuildMenu : MonoBehaviour {

    public GameObject toggleButtonPrefab;
    public GameObject modelDownloader;
    public string AssetBundleURL;

	// Use this for initialization
	void Start () {
        StartCoroutine(DownloadAndBuild());
        
	}

    private IEnumerator DownloadAndBuild()
    {
        WWW w = new WWW(AssetBundleURL + "MetaData.txt");
        //var w = localMetadata;
        yield return w;

        string[] lineData = w.text.Split("\n"[0]);
        float lineCounter = 0;
        float lineArrange = 0;
        

        for (int i = 0; i < lineData.Length; i++)
        {
            string[] modelData = lineData[i].Split("\t"[0]);
            GameObject newButton = Instantiate(toggleButtonPrefab);
            newButton.transform.parent = this.transform;
            newButton.transform.localPosition = new Vector3((lineCounter - 2) * 0.25f, lineArrange, 0);
            newButton.GetComponent<LabelTheme>().Default = modelData[0];
            newButton.GetComponent<LabelTheme>().Selected = modelData[0];

            newButton.AddComponent<SetBundleInfo>();
            newButton.GetComponent<SetBundleInfo>().modelDownloader = modelDownloader;
            newButton.GetComponent<SetBundleInfo>().bundleName = modelData[5];
            newButton.GetComponent<SetBundleInfo>().prefabName = modelData[4];


            lineCounter += 1;
            if (lineCounter == 5)
            {
                lineCounter = 0;
                lineArrange -= 0.1f;
            }
            
        }

        this.GetComponent<ObjectCollection>().UpdateCollection();

    }

    // Update is called once per frame
    void Update () {
		
	}
}
