using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotVolcanoes : MonoBehaviour {

	public TextAsset VolcanoData;
	public GameObject VolcanoSprite ;

	// Use this for initialization
	void Start () {
		var w = VolcanoData.text;

		string[] dataText = w.Split("\n"[0]);
		for (int i = 2; i < dataText.Length; i++)
		{
			string[] lineText = dataText[i].Split("\t"[0]);
			string name = lineText[0];

			float lat = float.Parse(lineText[8]);
			float lon = float.Parse(lineText[9])+90f;

			GameObject newVolcano = Instantiate(VolcanoSprite);
			newVolcano.transform.parent = this.transform;
			float cosLat = Mathf.Cos(lat * Mathf.PI / 180f);
			float sinLat = Mathf.Sin(lat * Mathf.PI / 180f);
			float cosLon = Mathf.Cos(lon * Mathf.PI / 180f);
			float sinLon = Mathf.Sin(lon * Mathf.PI / 180f);
			float rad = 0.995f;
			newVolcano.transform.localPosition = new Vector3(rad * cosLat * cosLon, rad * sinLat, rad * cosLat * sinLon);
			newVolcano.transform.localEulerAngles = new Vector3(-lat, 450f-lon, 0);
			newVolcano.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);

		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
