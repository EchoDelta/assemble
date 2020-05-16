using MonoGame.Extended;

namespace Assemble.Desktop.Components
{
    class GameCamera
    {
        public GameCamera(OrthographicCamera camera)
        {
            Camera = camera;
        }

        public OrthographicCamera Camera { get; }
    }
}
