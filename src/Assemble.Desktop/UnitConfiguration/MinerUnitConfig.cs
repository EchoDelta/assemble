using System;
using Microsoft.Xna.Framework;

namespace Assemble.Desktop.UnitConfiguration
{
    public class MinerUnitConfig : IUnitConfig
    {
        public UnitType UnitType => UnitType.Miner;
        public Texture Texture => Texture.Miner;
        public (int x, int y) TileSpan => (2, 2);
        public Color MiniMapColor => Color.DarkBlue;
        public TimeSpan? ProductionSpeed => TimeSpan.FromSeconds(5);
    }
}