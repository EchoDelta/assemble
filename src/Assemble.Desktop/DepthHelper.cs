using System;
using Assemble.Desktop.Components;
using Assemble.Desktop.Enums;

namespace Assemble.Desktop
{
    public class DepthHelper
    {
        private float _heightModifier;
        private float _layerModifier;
        public DepthHelper((int x, int y) mapSize)
        {
            _heightModifier = 1.0f / (mapSize.x + mapSize.y);
            _layerModifier = _heightModifier / (Enum.GetNames(typeof(TileRenderLayerType)).Length + 1);
        }

        public float GetDepth(TilePosition tilePosition, TileRenderLayer layer)
        {
            return GetPositionDepth(tilePosition) + GetLayerDepth(layer);
        }

        private float GetPositionDepth(TilePosition tilePosition)
        {
            return (tilePosition.Position.X + tilePosition.TileSpan.X + tilePosition.Position.Y + tilePosition.TileSpan.Y) * _heightModifier;
        }

        private float GetLayerDepth(TileRenderLayer layer)
        {

            return layer?.Layer switch
            {
                TileRenderLayerType.GroundTile => 0 * _layerModifier,
                TileRenderLayerType.Resources => 1 * _layerModifier,
                TileRenderLayerType.Units => 2 * _layerModifier,
                TileRenderLayerType.Products => 3 * _layerModifier,
                TileRenderLayerType.Overlay => 4 * _layerModifier,
                _ => 5 * _layerModifier,
            };
        }
    }
}