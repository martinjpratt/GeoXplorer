using UnityEngine;

public class OpenStreetMapTileBuilder : IMapUrlBuilder
{
    private static readonly string[] TilePathPrefixes = { "a", "b", "c" };
	private string mapBoxToken = "pk.eyJ1IjoibWFydGluanByYXR0IiwiYSI6ImNqaml3aHlyYzJuMzQzcG9semhueG40ZHgifQ.a2J_c8QL1XvFQC8WX9dFtA";


    public string GetTileUrl(TileInfo tileInfo)
    {
        //return string.Format("http://{0}.tile.openstreetmap.org/{1}/{2}/{3}.png",
          //         TilePathPrefixes[Mathf.Abs(tileInfo.X) % 3],
            //       tileInfo.ZoomLevel, tileInfo.X, tileInfo.Y);

		return string.Format("https://api.mapbox.com/v4/mapbox.satellite/{1}/{2}/{3}.jpg70?access_token=" + mapBoxToken,
			TilePathPrefixes[Mathf.Abs(tileInfo.X) % 3],
			tileInfo.ZoomLevel, tileInfo.X, tileInfo.Y);

		
    }

    public string GetGeoUrl(TileInfo tileInfo)
    {
        return string.Format("https://macrostrat.org/api/v2/maps/burwell/emphasized/{1}/{2}/{3}/tile.png",
        		TilePathPrefixes[Mathf.Abs(tileInfo.X) % 3],tileInfo.ZoomLevel, tileInfo.X, tileInfo.Y);
    }
}