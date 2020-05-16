using Microsoft.Xna.Framework;

namespace Assemble.Desktop.Components
{
    class Moveable
    {
        public Moveable(float speed)
        {
            Speed = speed;
        }
        public float Speed { get; }
        public Vector2 Velocity { get; set; }
    }
}
