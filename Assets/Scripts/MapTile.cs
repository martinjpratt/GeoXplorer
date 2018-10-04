
using HoloToolkitExtensions.RemoteAssets;
using System.Collections.Generic;
using UnityEngine;


public class MapTile : DynamicTextureDownloader
{
    public IMapUrlBuilder MapBuilder { get; set; }

    private TileInfo _tileData;

    public MapTile()
    {
        MapBuilder = MapBuilder != null ? MapBuilder : new OpenStreetMapTileBuilder();
    }

    public void SetTileData(TileInfo tiledata, bool forceReload = false)
    {
        if (_tileData == null || !_tileData.Equals(tiledata) || forceReload)
        {
            TileData = tiledata;
            StartLoadElevationDataFromWeb();
        }
    }

    public TileInfo TileData
    {
        get { return _tileData; }
        private set
        {
            _tileData = value;
            ImageUrl = MapBuilder.GetTileUrl(_tileData);
        }
    }

	private string _mapToken = "Ambq7PXp_TFBRYiA9uwOm6EncCPq5IUTzirPxVpUmv4HR04dMduJeLSv4unTQJGP";

    public bool IsDownloading { get; private set; }

	public float maxMeshValue;
	public float minMeshValue;

    private WWW _downloader;

    private void StartLoadElevationDataFromWeb()
    {
        if (_tileData == null)
        {
            return;
        }
        var northEast = _tileData.GetNorthEast();
        var southWest = _tileData.GetSouthWest();
		//print (southWest.Lon + " " + northEast.Lon);
		//print (southWest.Lat + " " + northEast.Lat);
		/*
        var urlData = string.Format(
        "http://dev.virtualearth.net/REST/v1/Elevation/Bounds?bounds={0},{1},{2},{3}&rows=11&cols=11&key={4}",
         southWest.Lat, southWest.Lon, northEast.Lat, northEast.Lon, _mapToken);
		*/
		var urlData = string.Format(
			"https://www.gmrt.org:443/services/GridServer?minlongitude={1}&maxlongitude={3}=&minlatitude={0}&maxlatitude={2}&format=esriascii&resolution=low&layer=topo",
			southWest.Lat, southWest.Lon, northEast.Lat + (3 * 0.00054931640625f), northEast.Lon); //fudge factor on the max latitude.
		//print (urlData);
        _downloader = new WWW(urlData);
        IsDownloading = true;
    }

    protected override void OnUpdate()
    {
        ProcessElevationDataFromWeb();
    }

    private void ProcessElevationDataFromWeb()
    {
        if (TileData == null || _downloader == null)
        {
            return;
        }

        if (IsDownloading && _downloader.isDone)
        {
            IsDownloading = false;
            //var elevationData = JsonUtility.FromJson<ElevationResult>(_downloader.text);

			//try {
			//	if (elevationData == null || elevationData.resourceSets[0].resources[0].elevations[0] == null)
			//	{
			//		return;
			//	}
			//
			//	ApplyElevationData(elevationData);
			//
			//} catch (System.Exception ex) {
			//	StartLoadElevationDataFromWeb ();
			//}

			ApplyElevationData ();

        }
    }

    //private void ApplyElevationData(ElevationResult elevationData)
	private void ApplyElevationData()
	{
		/*  //Old Code with coarse elevation data
        var threeDScale = TileData.ScaleFactor;
		//print(threeDScale);
        var resource = elevationData.resourceSets[0].resources[0];

        var verts = new List<Vector3>();
        var mesh = GetComponent<MeshFilter>().mesh;
        for (var i = 0; i < mesh.vertexCount; i++)
        {
            var newPos = mesh.vertices[i];
			newPos.y = resource.elevations [i] / threeDScale;
            verts.Add(newPos);
        }
        RebuildMesh(mesh, verts);
        */


		Vector3[] vertices;
		Mesh mesh;
		var verts = new List<Vector3>();
		//print (_downloader.text);
		GetComponent<MeshFilter>().mesh = mesh = new Mesh();
		string[] lineData = _downloader.text.Split ("\n" [0]);
		string[] colData = lineData [0].Split (" " [0]);
		string[] rowData = lineData [1].Split (" " [0]);
		string[] cellSizeData = lineData [4].Split (" " [0]);
		string[] xllData = lineData [2].Split (" " [0]);
		string[] yllData = lineData [3].Split (" " [0]);
		int nCols = int.Parse(colData [1]);
		int nRows = int.Parse(rowData [1])-1;
		float cellSize = float.Parse (cellSizeData [1]);
		float xll = float.Parse (xllData [1]);
		float yll = float.Parse (yllData [1]);
		float xct = xll;
		float yct = yll;
		//print (nRows * nCols);
		//print (yll);
		vertices = new Vector3[nCols * nRows];
		Vector2[] uv = new Vector2[vertices.Length];

		float maxY = 0f;
		float minY = 0f;
		for (int i = 0, y = 0; y < nRows; y++) 
		{
			string[] arrayLine = lineData [(nRows + 5) - y].Split (" " [0]);
			for (int x = 0; x < nCols; x++, i++) 
			{
				if (float.Parse(arrayLine[x]) > maxY) {
					maxY = float.Parse(arrayLine[x]);
				}

				if (float.Parse(arrayLine[x]) < minY) {
					minY = float.Parse(arrayLine[x]);
				}

				vertices[i] = new Vector3(xct, float.Parse(arrayLine[x]) / 111190f, yct);
				uv[i] = new Vector2((float)x / nCols, (float)y / nRows);
				verts.Add(vertices[i]);
				xct += cellSize;
				//print (xct + " " +yct);
			}
			yct += cellSize;
			xct = xll;
		}

		maxMeshValue = maxY;
		minMeshValue = minY;

		if (maxMeshValue > this.transform.parent.gameObject.GetComponent<MapBuilder>().superMaxMeshTile) {
			this.transform.parent.gameObject.GetComponent<MapBuilder> ().superMaxMeshTile = maxMeshValue;
		}


		if (minMeshValue < this.transform.parent.gameObject.GetComponent<MapBuilder>().superMinMeshTile) {
			this.transform.parent.gameObject.GetComponent<MapBuilder> ().superMinMeshTile = minMeshValue;
		}



		mesh.vertices = vertices;
		mesh.uv = uv;


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


		RebuildMesh(mesh, verts);
		this.transform.parent.gameObject.GetComponent<MapBuilder> ().tilesCompleted += 1;
		//this.transform.localScale = new Vector3 (this.transform.localScale.x / (nCols-1), this.transform.localScale.y, this.transform.localScale.z / (nRows-1));
    }

    private void RebuildMesh(Mesh mesh, List<Vector3> verts)
    {
		

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();	


        DestroyImmediate(gameObject.GetComponent<MeshCollider>());
        var meshCollider = gameObject.AddComponent<MeshCollider>();
        meshCollider.sharedMesh = mesh;

    }
}
