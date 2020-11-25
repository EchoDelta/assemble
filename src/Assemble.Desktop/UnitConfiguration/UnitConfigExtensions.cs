using Assemble.Desktop.Components;

namespace Assemble.Desktop.UnitConfiguration
{
    public static class UnitConfigExtensions
    {
        public static DirectionalTexture GetDirectionalTexture(this IUnitConfig unitConfig)
        {
            return new DirectionalTexture(unitConfig.TextureNorthEast, unitConfig.TextureSouthEast, unitConfig.TextureSouthWest, unitConfig.TextureNorthWest);
        }
    }
}