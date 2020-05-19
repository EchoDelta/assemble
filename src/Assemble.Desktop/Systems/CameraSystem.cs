using Assemble.Desktop.Components;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;

namespace Assemble.Desktop.Systems
{
    class CameraSystem : EntityProcessingSystem
    {
        private ComponentMapper<GameCamera> _cameraMapper;
        private ComponentMapper<Moveable> _moveableMapper;
        private ComponentMapper<Zoomable> _zoomableMapper;

        public CameraSystem() : base(Aspect.All(typeof(GameCamera)))
        {
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _cameraMapper = mapperService.GetMapper<GameCamera>();
            _moveableMapper = mapperService.GetMapper<Moveable>();
            _zoomableMapper = mapperService.GetMapper<Zoomable>();
        }

        public override void Process(GameTime gameTime, int entityId)
        {
            var camera = _cameraMapper.Get(entityId);
            var moveable = _moveableMapper.Get(entityId);
            var zoomable = _zoomableMapper.Get(entityId);

            if (zoomable != null)
            {
                camera.Camera.Zoom = zoomable.Zoom;
            }

            if (moveable != null)
            {
                camera.Camera.Position += moveable.Velocity / camera.Camera.Zoom;
            }
        }
    }
}
