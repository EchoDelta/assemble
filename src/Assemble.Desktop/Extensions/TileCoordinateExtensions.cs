using MonoGame.Extended;

namespace Assemble.Desktop.Extensions
{
    static class TileCoordinateExtensions
    {
        public static Point2 MapFromTileIndexToPoint(this (int, int) tileIndex)
        {
            var (tileX, tileY) = tileIndex;
            return new Point2(tileX, tileY);
        }
    }
}
