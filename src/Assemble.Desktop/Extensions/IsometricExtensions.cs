using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Assemble.Desktop.Extensions
{
    static class IsometricExtensions
    {
        private const float IsometricTileWidth = 64f;
        private const float IsometricTileHeight = 32f;

        public static Vector2 ToIsometric(this Vector2 tilePos)
        {
            return new Vector2(
                (tilePos.X - tilePos.Y) * IsometricTileWidth,
                (tilePos.X + tilePos.Y) * IsometricTileHeight);
        }

        public static IEnumerable<Vector2> ToIsometric(this IEnumerable<Vector2> tilePositions)
        {
            foreach (var tilePos in tilePositions)
            {
                yield return tilePos.ToIsometric();
            }
        }

        public static Vector2 FromIsometric(this Vector2 isometricPos)
        {
            return new Vector2(
                (isometricPos.X / IsometricTileWidth + isometricPos.Y / IsometricTileHeight) / 2,
                (isometricPos.Y / IsometricTileHeight - isometricPos.X / IsometricTileWidth) / 2);
        }
    }
}
