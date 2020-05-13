using MonoGame.Extended;

namespace Assemble.Desktop.Extensions
{
    static class TileCoordinateExtensions
    {
        private const float TileSize = 64f;
        public static Point2 MapFromTileCoordinateToCenterPoint(this Point2 tileCoordinate)
        {
            return new Point2(
                tileCoordinate.X * TileSize + TileSize * 0.5f,
                tileCoordinate.Y * TileSize + TileSize * 0.5f);
        }
    }
}
