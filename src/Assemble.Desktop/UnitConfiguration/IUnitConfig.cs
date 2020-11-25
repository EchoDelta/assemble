using System;
using Microsoft.Xna.Framework;

namespace Assemble.Desktop.UnitConfiguration
{
    public interface IUnitConfig
    {
        Texture TextureNorthEast { get; }
        Texture TextureSouthEast { get; }
        Texture TextureSouthWest { get; }
        Texture TextureNorthWest { get; }
        UnitType UnitType { get; }
        (int x, int y) TileSpan { get; }
        Color MiniMapColor { get; }
        TimeSpan? ProductionSpeed { get; }
        int? OutputBufferSize { get; }
        bool Blockable { get; }
        float? TransportationSpeed { get; }
    }
}