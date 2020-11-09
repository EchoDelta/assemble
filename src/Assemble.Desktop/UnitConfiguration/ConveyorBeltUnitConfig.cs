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
    }
}