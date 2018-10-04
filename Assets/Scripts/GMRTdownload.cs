using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMRTdownload : MonoBehaviour {
	private Vector3[] vertices;
	private Mesh mesh;

	public float minLat = 37.71859f;
	public float minLon = 14.94141f;
	public float maxLat = 37.78808f;
	public float maxLon = 15.0293f;

	// Use this for initialization
	void Start () {
		StartCoroutine (downloadGMRT ());
	}

	IEnumerator downloadGMRT(){
		GetComponent<MeshFilter>().mesh = mesh = new Mesh();
		mesh.name = "Procedural Grid";
		print ("https://www.gmrt.org:443/services/GridServer?minlongitude=" + minLon + "&maxlongitude=" + maxLon + "&minlatitude=" + minLat + "&maxlatitude=" + maxLat + "&format=esriascii&mresolution=200&layer=topo");
		WWW w = new WWW("https://www.gmrt.org:443/services/GridServer?minlongitude=" + minLon + "&maxlongitude=" + maxLon + "=&minlatitude=" + minLat + "&maxlatitude=" + maxLat + "&format=esriascii&mresolution=200&layer=topo");
		yield return w;

		string[] lineData = w.text.Split ("\n" [0]);
		string[] colData = lineData [0].Split (" " [0]);
		string[] rowData = lineData [1].Split (" " [0]);
		string[] cellSizeData = lineData [4].Split (" " [0]);
		int nCols = int.Parse(colData [1]);
		int nRows = int.Parse(rowData [1])-1;
		float cellSize = float.Parse (cellSizeData [1]);
		print (nRows * nCols);
		print (cellSize);
		vertices = new Vector3[nCols * nRows];
		for (int i = 0, y = 0; y < nRows; y++) 
		{
			string[] arrayLine = lineData [y + 6].Split (" " [0]);
			for (int x = 0; x < nCols; x++, i++) 
			{
				vertices[i] = new Vector3(x, float.Parse(arrayLine[x]) * (cellSize * nRows), y);
			}
		}
		mesh.vertices = vertices;

		int[] triangles = new int[(nCols-1) * (nRows-1) * 6];
		for (int ti = 0, vi = 0, y = 0; y < (nRows-1); y++, vi++) {
			for (int x = 0; x < (nCols-1); x++, ti += 6, vi++) {
				triangles[ti] = vi;
				triangles[ti + 3] = triangles[ti + 2] = vi + 1;
				triangles[ti + 4] = triangles[ti + 1] = vi + (nCols-1) + 1;
				triangles[ti + 5] = vi + (nCols-1) + 2;
			}
		}
		mesh.triangles = triangles;
		mesh.RecalculateNormals ();

	}

	// Update is called once per frame
	void Update () {
		
	}
}
