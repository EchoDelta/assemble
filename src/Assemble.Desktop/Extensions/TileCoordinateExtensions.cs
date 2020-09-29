using Microsoft.Xna.Framework;
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

        public static (int x, int y) MapToTileIndexFromVector(this Vector2 tilePoint)
        {
            return ((int)tilePoint.X, (int)tilePoint.Y);
        }
    }
}
