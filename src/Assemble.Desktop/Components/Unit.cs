using Assemble.Desktop.UnitConfiguration;

namespace Assemble.Desktop.Components
{
    public class Unit
    {
        public Unit(UnitType type)
        {
            UnitType = type;
        }
        public UnitType UnitType { get; }
    }
}