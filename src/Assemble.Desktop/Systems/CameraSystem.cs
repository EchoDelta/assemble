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

        public CameraSystem() : base(Aspect.All(typeof(GameCamera)))
        {
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _cameraMapper = mapperService.GetMapper<GameCamera>();
            _moveableMapper = mapperService.GetMapper<Moveable>();
        }

        public override void Process(GameTime gameTime, int entityId)
        {
            var camera = _cameraMapper.Get(entityId);
            var moveable = _moveableMapper.Get(entityId);

            if (moveable != null)
            {
                camera.Camera.Position += moveable.Velocity;
            }
        }
    }
}
