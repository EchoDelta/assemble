using MonoGame.Extended;

namespace Assemble.Desktop.Extensions
{
    static class IsometricExtensions
    {
        public static Transform2 ToIsometric(this Transform2 worldPos)
        {
            //90 45
            return new Transform2(
                (worldPos.Position.X - worldPos.Position.Y) * 0.707109375f,
                (worldPos.Position.X + worldPos.Position.Y) * 0.3535546875f);
        }
    }
}
