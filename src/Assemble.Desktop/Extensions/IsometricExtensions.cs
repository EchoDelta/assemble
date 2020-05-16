using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace Assemble.Desktop.Extensions
{
    static class IsometricExtensions
    {
        private const float IsometricTileWidth = 45.2550f;
        private const float IsometricTileHeight = 22.6275f;

        public static Transform2 ToIsometric(this Transform2 worldPos)
        {
            return new Transform2(worldPos.Position.ToIsometric());
        }

        public static Vector2 ToIsometric(this Vector2 worldPos)
        {
            return new Vector2(
                (worldPos.X - worldPos.Y) * IsometricTileWidth,
                (worldPos.X + worldPos.Y) * IsometricTileHeight);
        }
    }
}
