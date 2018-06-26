//Written by Martin Pratt, Fossett Lab for Virtual Planetary Exploration
//Washington University in St. Louis
//
//June 2018

using System;
using System.Collections;
using UnityEngine;

public class FindTheJR : MonoBehaviour {
    //Code to find the location of the JOIDES Resolution Drilling Ship and add a prefab to the globe


    public GameObject shipPrefab;
    float earthRadius = 1f;

    // Use this for initialization
    void Start () {
        StartCoroutine(FindTheJRNow());
    }

    private IEnumerator FindTheJRNow()
    {
        //Use website data to find the position...this is a little clunky as the site has a whole bunch of HTML
        WWW w = new WWW("http://joidesresolution.org/");
        yield return w;
        string[] stringSeparators = new string[] { "Current Ship Location:" };
        string[] stringSeparators1 = new string[] { "</a>" };
        string[] result = w.text.Split(stringSeparators, StringSplitOptions.None);
        string[] result1 = result[1].Split(stringSeparators1, StringSplitOptions.None);
        string[] result2 = result1[0].Split(","[0]);
        float lat = float.Parse(result2[0]);
        float lon = float.Parse(result2[1]);

        float xpos = earthRadius * Mathf.Cos(lat * Mathf.Deg2Rad) * Mathf.Cos((lon - 90) * Mathf.Deg2Rad) * -1;
        float zpos = earthRadius * Mathf.Cos(lat * Mathf.Deg2Rad) * Mathf.Sin((lon - 90) * Mathf.Deg2Rad) * -1;
        float ypos = earthRadius * Mathf.Sin(lat * Mathf.Deg2Rad);

        Vector3 locJR = new Vector3(xpos, ypos, zpos);
        GameObject theJR = Instantiate(shipPrefab, this.transform);
        theJR.transform.localPosition = locJR;
        theJR.transform.localEulerAngles = new Vector3(-(90 - lat), -180, 0);
        theJR.transform.Rotate(Vector3.up, -lon, Space.World);

    }
    
}
