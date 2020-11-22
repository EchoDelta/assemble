using Assemble.Desktop.UnitConfiguration;

namespace Assemble.Desktop.Components
{
    public class Unit
    {
        public Unit(UnitType type)
        {
            Type = type;
        }
        public UnitType Type { get; }
    }
}