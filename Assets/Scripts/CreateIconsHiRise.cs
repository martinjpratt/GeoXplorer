using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateIconsHiRise : MonoBehaviour {

	public TextAsset hiriseMetadata;
	public GameObject iconPrefab;
	float earthRadius = 1f;

	// Use this for initialization
	public void CreateIcons () {
		var iconList = hiriseMetadata.text.Split("\n"[0]);
		for (int i = 0; i < iconList.Length; i++) {
			var dataList = iconList[i].Split("\t"[0]);
			GameObject newIconPrefab = Instantiate(iconPrefab);
			newIconPrefab.transform.SetParent(this.transform);

			float lat = float.Parse (dataList [2]);
			float lon = float.Parse (dataList [3]);

			float xpos = earthRadius * Mathf.Cos(lat * Mathf.Deg2Rad) * Mathf.Cos((lon + 90) * Mathf.Deg2Rad) * -1;
			float zpos = earthRadius * Mathf.Cos(lat * Mathf.Deg2Rad) * Mathf.Sin((lon + 90) * Mathf.Deg2Rad) * -1;
			float ypos = earthRadius * Mathf.Sin(lat * Mathf.Deg2Rad);

			newIconPrefab.transform.localPosition = new Vector3(xpos, ypos, zpos);

			newIconPrefab.name = dataList [1];
			newIconPrefab.GetComponent<HiRiseNameID> ().Name = dataList [0];
			newIconPrefab.GetComponent<HiRiseNameID> ().ID = dataList [1];
		}
	}

	public void DestroyIcons (){
		GameObject[] icons = GameObject.FindGameObjectsWithTag ("hiriseLocation");
		foreach (var icon in icons) {
			Destroy (icon);
		}
	}


	// Update is called once per frame
	void Update () {
		
	}
}

