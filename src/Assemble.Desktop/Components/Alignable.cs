using System;
using Assemble.Desktop.Enums;

namespace Assemble.Desktop.Components
{
    public class Alignable
    {
        public Direction Direction { get; set; }

        public Alignable(Direction initalDirection)
        {
            Direction = initalDirection;
        }

        public void Rotate()
        {
            Direction = (Direction)((int)(Direction + 1) % Enum.GetNames(typeof(Direction)).Length);
        }
    }
}