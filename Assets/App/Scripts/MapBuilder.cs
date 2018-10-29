using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapBuilder : MonoBehaviour
{
    public int ZoomLevel = 12;

    public float MapTileSize = 0.5f;

    public float Latitude = 47.642567f;
    public float Longitude = -122.136919f;
    public float targetLatitude = 47.642567f;
    public float targetLongitude = -122.136919f;

    public GameObject MapTilePrefab;
    public GameObject targetLocation;
    public TextMesh LatLabel;
    public TextMesh LonLabel;

    public float MapSize = 12;

    private TileInfo _centerTile;
    private List<MapTile> _mapTiles;

    void Start()
    {
        _mapTiles = new List<MapTile>();
        ShowMap();
    }

    public void ShowMap()
    {
        Destroy(GameObject.FindGameObjectWithTag("flag"));

        LatLabel.text = "Latitude: " + Latitude.ToString();
        LonLabel.text = "Longitude: " + Longitude.ToString();
        _centerTile = new TileInfo(new WorldCoordinate { Lat = Latitude, Lon = Longitude }, 
            ZoomLevel, MapTileSize);
        LoadTiles();
        
    }

    private void LoadTiles(bool forceReload = false)
    {
        var size = (int)(MapSize / 2);

        var tileIndex = 0;
        for (var x = -size; x <= size; x++)
        {
            for (var y = -size; y <= size; y++)
            {
                var tile = GetOrCreateTile(x, y, tileIndex++);
                tile.SetTileData(new TileInfo(_centerTile.X - x, _centerTile.Y + y, ZoomLevel, MapTileSize),
                    forceReload);
                tile.gameObject.name = string.Format("({0},{1}) - {2},{3}", x, y, tile.TileData.X,
                    tile.TileData.Y);
                WorldCoordinate ne = tile.TileData.GetNorthEast();
                WorldCoordinate sw = tile.TileData.GetSouthWest();
                if (targetLatitude > sw.Lat && targetLatitude < ne.Lat && targetLongitude > sw.Lon && targetLongitude < ne.Lon)
                {
                    float relLat = (targetLatitude - sw.Lat) / (ne.Lat - sw.Lat);
                    float relLon = (targetLongitude - sw.Lon) / (ne.Lon - sw.Lon);
                    GameObject target = Instantiate(targetLocation);
                    target.transform.parent = tile.transform;
                    target.transform.localPosition = new Vector3((relLon * -10f) + 5f, 0, (relLat * -10f) + 5f);
                    target.transform.localEulerAngles = Vector3.zero;
                }
            }
        }
        
    }

    private MapTile GetOrCreateTile(int x, int y, int i)
    {
        if (_mapTiles.Any() && _mapTiles.Count > i)
        {
            return _mapTiles[i];
        }



        var mapTile = Instantiate(MapTilePrefab, transform);
        Renderer[] rends = mapTile.GetComponentsInChildren<Renderer>();

        for (int j = 1; j < 16; j++)
        {
            if (j == 1 || j == 1 + 8)
            {
                if (y == 2)
                {
                    rends[j].enabled = true;
                }
                else
                {
                    Destroy(rends[j].gameObject);
                }
            }

            if (j == 2 || j == 2 + 8)
            {
                if (x == 2)
                {
                    rends[j].enabled = true;
                }
                else
                {
                    Destroy(rends[j].gameObject);
                }
            }

            if (j == 3 || j == 3 + 8)
            {
                if (x == -2 && y == 2)
                {
                    rends[j].enabled = true;
                }
                else
                {
                    Destroy(rends[j].gameObject);
                }
            }

            if (j == 4 || j == 4 + 8)
            {
                if (x == -2)
                {
                    rends[j].enabled = true;
                }
                else
                {
                    Destroy(rends[j].gameObject);
                }
            }

            if (j == 5 || j == 5 + 8)
            {
                if (y == -2)
                {
                    rends[j].enabled = true;
                }
                else
                {
                    Destroy(rends[j].gameObject);
                }
            }

            if (j == 6 || j == 6 + 8)
            {
                if (x == 2 && y == -2)
                {
                    rends[j].enabled = true;
                }
                else
                {
                    Destroy(rends[j].gameObject);
                }
            }

            if (j == 7 || j == 7 + 8 || j == 8 || j == 8 + 8)
            {
                if (x == -2 && y == -2)
                {
                    rends[j].enabled = true;
                }
                else
                {
                    Destroy(rends[j].gameObject);
                }
            }
        }
        
            //rends[4].enabled = true;
            //rends[4 + 8].enabled = true;
        
       
            //rends[1].enabled = true;
            //rends[1 + 8].enabled = true;
        
       
            //rends[5].enabled = true;
            //rends[5 + 8].enabled = true;
        
       
            //rends[6].enabled = true;
            //rends[6 + 8].enabled = true;
        
        
            //rends[3].enabled = true;
            //rends[3 + 8].enabled = true;
        
        
            //rends[7].enabled = true;
            //rends[8].enabled = true;
            //rends[7 + 8].enabled = true;
            //rends[8 + 8].enabled = true;
        


        mapTile.transform.localPosition = new Vector3(MapTileSize * x - MapTileSize / 2, 0, MapTileSize * y + MapTileSize / 2);
        mapTile.transform.localRotation = Quaternion.identity;
        var tile = mapTile.GetComponent<MapTile>();
        _mapTiles.Add(tile);
        return tile;
    }
}