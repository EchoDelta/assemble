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
    }
}