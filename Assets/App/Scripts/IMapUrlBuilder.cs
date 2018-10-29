public interface IMapUrlBuilder
{
    string GetTileUrl(TileInfo tileInfo);
    string GetGeoUrl(TileInfo tileInfo);
}
