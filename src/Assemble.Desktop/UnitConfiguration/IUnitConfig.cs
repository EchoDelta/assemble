using System;
using Microsoft.Xna.Framework;

namespace Assemble.Desktop.UnitConfiguration
{
    public interface IUnitConfig
    {
        Texture Texture { get; }
        UnitType UnitType { get; }
        (int x, int y) TileSpan { get; }
        Color MiniMapColor { get; }
        TimeSpan? ProductionSpeed { get; }
        int? OutputBufferSize { get; }
    }
}