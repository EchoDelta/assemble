using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace Assemble.Desktop.Extensions
{
    static class IsometricExtensions
    {
        private const float IsometricTileWidth = 64f;
        private const float IsometricTileHeight = 32f;

        public static Transform2 ToIsometric(this Transform2 tilePos)
        {
            return new Transform2(tilePos.Position.ToIsometric());
        }

        public static Vector2 ToIsometric(this Vector2 tilePos, bool adjustFromCenter = false)
        {
            var adjustment = (adjustFromCenter ? -1 : 0) * new Vector2(0, IsometricTileHeight);
            return new Vector2(
                (tilePos.X - tilePos.Y) * IsometricTileWidth,
                (tilePos.X + tilePos.Y) * IsometricTileHeight) + adjustment;
        }

        public static IEnumerable<Vector2> ToIsometric(this IEnumerable<Vector2> tilePoss, bool adjustFromCenter = false)
        {
            foreach (var tilePos in tilePoss)
            {
                yield return tilePos.ToIsometric(adjustFromCenter);
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
