//Written by Martin Pratt, Fossett Lab for Virtual Planetary Exploration
//Washington University in St. Louis
//
//June 2018

using UnityEngine;

public class PlotHotspots : MonoBehaviour {

    //Plot hotspot locations from local file

    public TextAsset hotspotData;
    public GameObject hotspotPrefab;

	// Use this for initialization
	void Start () {
        var w = hotspotData.text;

        string[] dataText = w.Split("\n"[0]);
        for (int i = 0; i < dataText.Length-1; i++)
        {
            string[] lineText = dataText[i].Split("\t"[0]);
            string name = lineText[0];
            float lat = float.Parse(lineText[1]);
            float lon = float.Parse(lineText[2])+90f;

            GameObject newHotspot = Instantiate(hotspotPrefab);
            newHotspot.transform.parent = this.transform;
            float cosLat = Mathf.Cos(lat * Mathf.PI / 180f);
            float sinLat = Mathf.Sin(lat * Mathf.PI / 180f);
            float cosLon = Mathf.Cos(lon * Mathf.PI / 180f);
            float sinLon = Mathf.Sin(lon * Mathf.PI / 180f);
            float rad = 0.995f;
            newHotspot.transform.localPosition = new Vector3(rad * cosLat * cosLon, rad * sinLat, rad * cosLat * sinLon);
            newHotspot.transform.localEulerAngles = new Vector3(-lat, 450f-lon, 0);
            newHotspot.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);

        }

    }
}
