using System;
using System.Collections.Generic;

namespace Assemble.Desktop.Components
{
    public class ProductionUnit
    {
        public ProductionUnit(TimeSpan productionSpeed, int outputBufferSize)
        {
            OutputBufferSize = outputBufferSize;
            ProductionSpeed = productionSpeed;
        }

        public TimeSpan ProductionSpeed { get; }
        public TimeSpan CurrentProductionTime { get; set; }
        public bool ProductionActive { get; set; }
        public int OutputBufferSize { get; }
        public List<int> OutputBuffer { get; } = new List<int>();
    }
}