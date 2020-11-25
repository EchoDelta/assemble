using System;
using Microsoft.Xna.Framework;

namespace Assemble.Desktop.UnitConfiguration
{
    public class ConveyorBeltUnitConfig : IUnitConfig
    {
        public UnitType UnitType => UnitType.ConveyorBelt;
        public Texture Texture => Texture.ConveyorBeltNE;
        public (int x, int y) TileSpan => (1, 1);
        public Color MiniMapColor => Color.Gold;
        public TimeSpan? ProductionSpeed => null;
        public int? OutputBufferSize => null;
        public bool Blockable => false;
        public float? TransportationSpeed => 0.25f;
        public Texture TextureNorthEast => Texture.ConveyorBeltNE;
        public Texture TextureSouthEast => Texture.ConveyorBeltSE;
        public Texture TextureSouthWest => Texture.ConveyorBeltSW;
        public Texture TextureNorthWest => Texture.ConveyorBeltNW;
    }
}