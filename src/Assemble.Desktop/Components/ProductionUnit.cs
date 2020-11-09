using System;

namespace Assemble.Desktop.Components
{
    public class ProductionUnit
    {
        public ProductionUnit(TimeSpan productionSpeed)
        {
            ProductionSpeed = productionSpeed;
        }

        public TimeSpan ProductionSpeed { get; }
        public TimeSpan CurrentProductionTime { get; set; }
        public bool ProductionActive { get; set; }
    }
}