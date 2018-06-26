//Written by Martin Pratt, Fossett Lab for Virtual Planetary Exploration
//Washington University in St. Louis
//
//June 2018

using System.Collections;
using UnityEngine;

public class FindRecentEarthquakes : MonoBehaviour {
    //Code to find the most recent earthquakes from the USGS and plot them in 3D spherical

    public Material shallow;
    public Material d100;
    public Material d200;
    public Material d300;
    public Material d400;
    public Material d500;
    public Material d600;
    public Material d700;

    Renderer rend;

    // Use this for initialization
    void Start () {

        StartCoroutine(FetchEqs());
    }

    private IEnumerator FetchEqs()
    {
        //Download location of CSV file
        WWW w = new WWW("https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/4.5_month.csv");
        yield return w;

        var eventData = w.text;
        string[] eventStr = eventData.Split("\n"[0]);
        float numberEvents = eventStr.Length;

        for (var i = 1; i < numberEvents; i++)
        {
            var dataList = eventStr[i].Split(","[0]);


            if (dataList.Length > 1)
            {
                GameObject stationSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                stationSphere.transform.position = new Vector3(0, 0, 0);
                stationSphere.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
                stationSphere.transform.parent = this.transform;                

                float lat = float.Parse(dataList[1]);
                float lon = float.Parse(dataList[2]) + 90f;
                float dep = float.Parse(dataList[3]);
                float mag = float.Parse(dataList[4]);
                
                //Convert lat long depth to cartesian coordinates and place within sphere
                float cosLat = Mathf.Cos(lat * Mathf.PI / 180f);
                float sinLat = Mathf.Sin(lat * Mathf.PI / 180f);
                float cosLon = Mathf.Cos(lon * Mathf.PI / 180f);
                float sinLon = Mathf.Sin(lon * Mathf.PI / 180f);
                var rad = 0.99f * ((6371 - dep) / 6371);
                stationSphere.transform.localPosition = new Vector3(rad * cosLat * cosLon, rad * sinLat, rad * cosLat * sinLon);
                stationSphere.transform.localScale = new Vector3(Mathf.Exp(mag) * 0.00005f, Mathf.Exp(mag) * 0.00005f, Mathf.Exp(mag) * 0.00005f);

                rend = stationSphere.GetComponent<Renderer>();

                StartCoroutine(FadeTo(rend, dep));
            }
        }
        
    }


    //Color function (was brought in from another program that made the spheres fade over time... hence the name)
    private IEnumerator FadeTo(Renderer rend, float depth)
    {
        if (depth < 100)
        {
            rend.material = shallow;
        }
        if (depth >= 100 && depth < 200)
        {
            rend.material = d100;
        }
        if (depth >= 200 && depth < 300)
        {
            rend.material = d200;
        }
        if (depth >= 300 && depth < 400)
        {
            rend.material = d300;
        }
        if (depth >= 400 && depth < 500)
        {
            rend.material = d400;
        }
        if (depth >= 500 && depth < 600)
        {
            rend.material = d500;
        }
        if (depth >= 600 && depth < 700)
        {
            rend.material = d600;
        }
        if (depth >= 700)
        {
            rend.material = d700;
        }
        yield return rend;
    }
}
