using Assemble.Desktop.Enums;

namespace Assemble.Desktop.Components
{
    public class TileRenderLayer
    {
        public TileRenderLayerType Layer { get; }
        public TileRenderLayer(TileRenderLayerType layer)
        {
            Layer = layer;
        }
    }
}