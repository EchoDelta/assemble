using Assemble.Desktop.Components;

namespace Assemble.Desktop
{
    public class DepthHelper
    {
        private float _heightModifier;
        public DepthHelper((int x, int y) mapSize)
        {
            _heightModifier = 1.0f / (mapSize.x + mapSize.y);
        }

        public float GetDepth(TilePosition tilePosition)
        {
            return (tilePosition.Position.X + tilePosition.TileSpan.X + tilePosition.Position.Y + tilePosition.TileSpan.Y) * _heightModifier;
        }
    }
}