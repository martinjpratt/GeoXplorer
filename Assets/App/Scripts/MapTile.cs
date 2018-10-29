
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
            GeoImageUrl = MapBuilder.GetGeoUrl(_tileData);
        }
    }


    //public TileInfo TileData
    //{
    //    get { return _tileData; }
    //    private set
    //    {
    //        _tileData = value;
    //        GeoImageUrl = MapBuilder.GetGeoUrl(_tileData);
    //    }
    //}

    private string _mapToken = "Ambq7PXp_TFBRYiA9uwOm6EncCPq5IUTzirPxVpUmv4HR04dMduJeLSv4unTQJGP";

    public bool IsDownloading { get; private set; }

    private WWW _downloader;

    private void StartLoadElevationDataFromWeb()
    {
        if (_tileData == null)
        {
            return;
        }
        var northEast = _tileData.GetNorthEast();
        var southWest = _tileData.GetSouthWest();

		this.GetComponent<TapInfoResponder> ().northeast = northEast;
		this.GetComponent<TapInfoResponder> ().southwest = southWest;

        var urlData = string.Format(
        "http://dev.virtualearth.net/REST/v1/Elevation/Bounds?bounds={0},{1},{2},{3}&rows=11&cols=11&key={4}",
         southWest.Lat, southWest.Lon, northEast.Lat, northEast.Lon, _mapToken);

        TextMesh[] tmeshes = GetComponentsInChildren<TextMesh>();
        for (int i = 0; i < tmeshes.Length; i++)
        {
            if (i == 0)
            {
                tmeshes[i].text = southWest.Lon.ToString();
            }
            if (i == 1)
            {
                tmeshes[i].text = southWest.Lat.ToString();
            }
            if (i == 2)
            {
                tmeshes[i].text = northEast.Lon.ToString();
            }
            if (i == 3)
            {
                tmeshes[i].text = southWest.Lat.ToString();
            }
            if (i == 4)
            {
                tmeshes[i].text = southWest.Lon.ToString();
            }
            if (i == 5)
            {
                tmeshes[i].text = northEast.Lat.ToString();
            }
            if (i == 6)
            {
                tmeshes[i].text = northEast.Lon.ToString();
            }
            if (i == 7)
            {
                tmeshes[i].text = northEast.Lat.ToString();
            }
        }

		
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
            var elevationData = JsonUtility.FromJson<ElevationResult>(_downloader.text);

			try {
				if (elevationData == null || elevationData.resourceSets[0].resources[0].elevations[0] == null)
				{
					return;
				}

				ApplyElevationData(elevationData);

			} catch (System.Exception) {
				StartLoadElevationDataFromWeb ();
			}

        }
    }

    private void ApplyElevationData(ElevationResult elevationData)
    {
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
    }

    private void RebuildMesh(Mesh mesh, List<Vector3> verts)
    {
        mesh.SetVertices(verts);
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        DestroyImmediate(gameObject.GetComponent<MeshCollider>());
        var meshCollider = gameObject.AddComponent<MeshCollider>();
        meshCollider.sharedMesh = mesh;

        float meshYpos = mesh.bounds.min.y;

        Transform[] labelTrans = GetComponentsInChildren<Transform>();
        for (int i = 1; i < labelTrans.Length; i++)
        {
            labelTrans[i].localPosition = new Vector3(labelTrans[i].localPosition.x, meshYpos, labelTrans[i].localPosition.z);
        }

        
    }
}
