using System;
using System.Collections;
using UnityEngine;

public class BuildGlobe : MonoBehaviour {

    public GameObject planeSegement;
    public string defaultInstrument;
    public string defaultResolution;
    public string defaulImageType;
    DateTime dateNow = DateTime.Now;

	// Use this for initialization
	void Start () {
        dateNow = DateTime.Now;
        string defaultDate = dateNow.AddDays(-1).ToString("yyyy-MM-dd");
        StartCoroutine(GenerateGlobe(defaultInstrument, defaultResolution, defaulImageType, defaultDate));
        
	}

    private IEnumerator GenerateGlobe(string instrument, string resolution, string imageType, string imageDate)
    {
        float deltaLat = 36;
        float deltaLon = 36;
        float startLat = 90;
        float startLon = 0;

        //if globe already exists, delete and start again
        //if (instrument != "Coastlines")
        //{
            GameObject[] tileplanes = GameObject.FindGameObjectsWithTag("TilePlane");
            if (tileplanes.Length > 0)
            {
                foreach (var go in tileplanes)
                {
                    Destroy(go);
                }
            }

        //}


        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                GameObject newPlaneObject = Instantiate(planeSegement);
                newPlaneObject.transform.parent = this.transform;
                newPlaneObject.GetComponent<RestructurePlane>().maxLat = startLat - (i * deltaLat);
                newPlaneObject.GetComponent<RestructurePlane>().minLat = startLat - ((i + 1) * deltaLat);
                newPlaneObject.GetComponent<RestructurePlane>().minLon = startLon + (j * deltaLon);
                newPlaneObject.GetComponent<RestructurePlane>().maxLon = startLon + ((j + 1) * deltaLon);


                StartCoroutine(AddTexture(newPlaneObject, i, j, instrument, resolution, imageType, imageDate));

                if(instrument == "Coastlines")
                {
                    newPlaneObject.tag = "Coastlines";
                }
            }
        }

        this.transform.eulerAngles = new Vector3(0, -90, 0);
        yield return tileplanes;
    }

    private IEnumerator AddTexture(GameObject newPlaneObject, int i, int j, string instrument, string resolution, string imageType, string imageDate)
    {
        string url = "https://gibs.earthdata.nasa.gov/wmts/epsg4326/best/" + instrument + "/default/" + imageDate + "/" + resolution + "/3/" + i + "/" + j + "." + imageType;
        
        Texture2D tex;
        tex = new Texture2D(4, 4, TextureFormat.DXT1, false);
        using (WWW www = new WWW(url))
        {
            yield return www;
            www.LoadImageIntoTexture(tex);
            newPlaneObject.GetComponent<Renderer>().material.mainTexture = tex;
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
