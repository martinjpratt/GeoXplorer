//Written by Martin Pratt, Fossett Lab for Virtual Planetary Exploration
//Washington University in St. Louis
//
//June 2018

using System;
using System.Collections;
using UnityEngine;

public class PlateBoundariesFromWeb : MonoBehaviour {

    //Code to download a plate boundary location file from the web and plot them in 3D spherical on the globe

    //Download location
    string boundariesURL = "http://peterbird.name/oldFTP/PB2002/PB2002_boundaries.dig.txt";
    public GameObject segment;
    
    // Use this for initialization
    void Start () {
        StartCoroutine(DownloadBoundaries());
	}
	
    IEnumerator DownloadBoundaries()
    {
        WWW w = new WWW(boundariesURL);
        yield return w;

        string[] dataText = w.text.Split(new string[] { "*** end of line segment ***\r\n" }, StringSplitOptions.None);

        for (int i = 0; i < dataText.Length-1; i++)
        {
            GameObject newSegment = Instantiate(segment);
            newSegment.transform.parent = this.transform;

            string[] segmentText = dataText[i].Split("\n"[0]);//, StringSplitOptions.RemoveEmptyEntries);
            
            newSegment.GetComponent<LineRenderer>().positionCount = segmentText.Length - 2;
            
            for (int j = 0; j < segmentText.Length-1; j++)
            {
                if (j==0)
                {
                    char boundaryType = segmentText[j][2];
                    if (boundaryType == '-')
                    {
                        newSegment.GetComponent<LineRenderer>().startColor = Color.red;
                        newSegment.GetComponent<LineRenderer>().endColor = Color.red;
                    }
                }
                else
                {
                    string[] lineText = segmentText[j].Split(","[0]);
                    float longitude = float.Parse(lineText[0]) + 90f;
                    float latitude = float.Parse(lineText[1]);

                    float cosLat = Mathf.Cos(latitude * Mathf.PI / 180f);
                    float sinLat = Mathf.Sin(latitude * Mathf.PI / 180f);
                    float cosLon = Mathf.Cos(longitude * Mathf.PI / 180f);
                    float sinLon = Mathf.Sin(longitude * Mathf.PI / 180f);
                    var rad = 0.99f * ((6371 - 0) / 6371);
                    newSegment.GetComponent<LineRenderer>().SetPosition(j-1, new Vector3(rad * cosLat * cosLon, rad * sinLat, rad * cosLat * sinLon));
                }
                newSegment.transform.localPosition = Vector3.zero;
                newSegment.transform.localRotation = Quaternion.identity;
                newSegment.transform.localScale = Vector3.one;
            }
        }
        
    }
}
