using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace Assemble.Desktop.Components
{
    public class TilePosition
    {
        public TilePosition((int X, int Y) tileIndex) : this(tileIndex, (1, 1))
        {
        }

        public TilePosition((int X, int Y) tileIndex, (int x, int y) tileSpan)
        {
            ChangeTile(tileIndex);
            TileSpan = tileSpan;
        }

        public Vector2 Position { get; private set; }
        public (int X, int Y) TileSpan { get; }

        public void ChangeTile((int X, int Y) tileIndex)
        {
            Position = new Vector2(tileIndex.X, tileIndex.Y);
        }

        public RectangleF GetArea()
        {
            return new RectangleF(Position.X, Position.Y, TileSpan.X, TileSpan.Y);
        }
    }
}