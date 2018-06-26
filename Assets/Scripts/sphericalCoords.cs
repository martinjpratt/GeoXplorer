//Written by Martin Pratt, Fossett Lab for Virtual Planetary Exploration
//Washington University in St. Louis
//
//June 2018

using System.Collections;
using UnityEngine;
using System;
using System.Globalization;


public class sphericalCoords : MonoBehaviour {

    //Plot the regional earthquakes in 3D spherical. 

    public static string[] dataEventList;
    public static float meanLat;
    public static float meanLon;
    public static float maxdep;
    public static float minLat;
    public static float minLon;
    public static float maxLat;
    public static float maxLon;
    public static float minmaxLat;
    public static float minmaxLon;

    Renderer rend;
    public Material shallow;
    public Material d100;
    public Material d200;
    public Material d300;
    public Material d400;
    public Material d500;
    public Material d600;
    public Material d700;

    public GameObject infoObject;
    public GameObject textObject;

    public GameObject startTime;
    public GameObject endTime;

    public GameObject minimumMagnitude;
    public GameObject maximumMagnitude;

    public GameObject minimumDepth;
    public GameObject maximumDepth;

    public GameObject numberText;

    // Use this for initialization
    void OnEnable () {
        StartCoroutine(Check());
    }

    private IEnumerator Check()
    {

        float[] scores = new float[48];

        float extentMaxLat = TapLocationSet.topLeftLat;
        float extentMinLat = TapLocationSet.bottomRightLat;
        float extentMaxLon = TapLocationSet.bottomRightLon;
        float extentMinLon = TapLocationSet.topLeftLon;
        

        string st = startTime.GetComponent<TextMesh>().text;
        string et = endTime.GetComponent<TextMesh>().text;
        string minMag = minimumMagnitude.GetComponent<TextMesh>().text;
        string maxMag = maximumMagnitude.GetComponent<TextMesh>().text;
        string minDep = minimumDepth.GetComponent<TextMesh>().text;
        string maxDep = maximumDepth.GetComponent<TextMesh>().text;

        DateTime startDate = DateTime.ParseExact(st, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        DateTime endDate = DateTime.ParseExact(et, "dd/MM/yyyy", CultureInfo.InvariantCulture);

       

        WWW w = new WWW("http://service.iris.edu/fdsnws/event/1/query?starttime=" + startDate.ToString("yyyy-MM-ddTHH':'mm':'ss") + "&endtime=" + endDate.ToString("yyyy-MM-ddTHH':'mm':'ss") + "&minmag=" + minMag + "&catalog=NEIC+PDE&orderby=magnitude&format=text&maxlat=" + extentMaxLat + "&minlon=" + extentMinLon + "&maxlon=" + extentMaxLon + "&minlat=" + extentMinLat + "&nodata=404");
        yield return w;
        if (w.error != null)
        {
            Debug.Log("Error .. " + w.error);
        }
        else
        {
            Debug.Log("Found ... ==>" + w.text + "<==");
        }

        // example code to separate all that text in to lines:
        var eventData = w.text;
        string[] eventStr = eventData.Split("\n"[0]);
        dataEventList = eventStr;

        minLat = 90;
        maxLat = -90;
        minLon = 180;
        maxLon = -180;
        maxdep = 0;

        Debug.Log(eventStr.Length);
        float numberEvents = eventStr.Length;
        numberText.GetComponent<TextMesh>().text = "Displaying " + (eventStr.Length -2).ToString() + " events";
        if (eventStr.Length > 1002)
        {
            numberEvents = 1001;
            numberText.GetComponent<TextMesh>().text = "Displaying 1000 largest magnitude events out of " + (eventStr.Length-2).ToString();
        }
        

        for (var i = 1; i < numberEvents; i++)
        {
            var dataList = eventStr[i].Split("|"[0]);


            if (dataList.Length > 1)
            {
                GameObject stationSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                stationSphere.transform.position = new Vector3(0, 0, 0);
                stationSphere.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                stationSphere.transform.parent = this.transform;
                stationSphere.tag = "Player";
                stationSphere.AddComponent<IDNumber>();
                stationSphere.AddComponent<TapEarthquakeResponder>();
                stationSphere.GetComponent<TapEarthquakeResponder>().infoObject = infoObject;
                stationSphere.GetComponent<TapEarthquakeResponder>().textObject = textObject;

                int year = int.Parse(dataList[1].Substring(0,4)) - 1970;
                
                float lat = float.Parse(dataList[2]);
                float lon = float.Parse(dataList[3]);
                float dep = float.Parse(dataList[4]);

                if (dep > maxdep)
                {
                    maxdep = dep;
                }

                if (lat > maxLat)
                {
                    maxLat = lat;
                }

                if (lon > maxLon)
                {
                    maxLon = lon;
                }

                if (lat < minLat)
                {
                    minLat = lat;
                }

                if (lon < minLon)
                {
                    minLon = lon;
                }

            }
        }



        minmaxLat = maxLat - minLat;
        minmaxLon = maxLon - minLon;
        meanLat = minLat + (minmaxLat / 2);
        meanLon = minLon + (minmaxLon / 2);

        GameObject[] gameObjects;
        gameObjects = GameObject.FindGameObjectsWithTag("Player");

        int datacounter = 1;
        foreach (GameObject go in gameObjects)
        {
            //lat lons
            var ndataEventList = dataEventList[datacounter].Split("|"[0]);

            float lat = float.Parse(ndataEventList[2]);
            float lon = float.Parse(ndataEventList[3]);
            float depth = float.Parse(ndataEventList[4]);
            float mag = float.Parse(ndataEventList[10]);

            float cosLat = Mathf.Cos(lat * Mathf.PI / 180f);
            float sinLat = Mathf.Sin(lat * Mathf.PI / 180f);
            float cosLon = Mathf.Cos(lon * Mathf.PI / 180f);
            float sinLon = Mathf.Sin(lon * Mathf.PI / 180f);
            var rad = 10 * ((6371 - depth) / 6371);
            go.transform.position = new Vector3(rad * cosLat * cosLon, rad * sinLat, rad * cosLat * sinLon);

            go.transform.localScale = new Vector3(Mathf.Exp(mag) * 0.00005f, Mathf.Exp(mag) * 0.00005f, Mathf.Exp(mag) * 0.00005f);
            rend = go.GetComponent<Renderer>();
            go.GetComponent<IDNumber>().IDValue = ndataEventList[8];

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



            datacounter = datacounter + 1;
        }

        this.transform.Rotate(Vector3.up, 90 + meanLon, Space.World);
        this.transform.Rotate(Vector3.right, 90 - meanLat, Space.World);
        this.transform.position = new Vector3(0f, -10.2f, 1.5f);

        //TapReset.origPosition = this.transform.position;

        //Debug.Log(scores[1]);
        //barChart.GetComponent<BarChart>().DisplayGraph(scores);
        


    }
}
