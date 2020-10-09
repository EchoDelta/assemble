using System;
using Microsoft.Xna.Framework;

namespace Assemble.Desktop.Extensions
{
    static class TilePointExtensions
    {
        public static (int X, int Y) MapFromCenterTilePointToTopRightTileIndex(this Vector2 tilePoint, (int X, int Y) tileSpan)
        {
            // TODO: This is not completely correct...
            return ((int)Math.Round(tilePoint.X - tileSpan.X / 2), (int)Math.Round(tilePoint.Y - tileSpan.Y / 2));
        }
    }
}
