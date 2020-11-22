using System;
using Microsoft.Xna.Framework;

namespace Assemble.Desktop.UnitConfiguration
{
    public class MinerUnitConfig : IUnitConfig
    {
        public UnitType UnitType => UnitType.Miner;
        public Texture Texture => Texture.MinerNE;
        public (int x, int y) TileSpan => (2, 2);
        public Color MiniMapColor => Color.DarkBlue;
        public TimeSpan? ProductionSpeed => TimeSpan.FromSeconds(5);
        public int? OutputBufferSize => 3;
        public bool Blockable => true;
        public float? TransportationSpeed => null;
    }
}