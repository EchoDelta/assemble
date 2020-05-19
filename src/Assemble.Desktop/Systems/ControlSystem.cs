using Assemble.Desktop.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.Input;

namespace Assemble.Desktop.Systems
{
    class ControlSystem : EntityProcessingSystem
    {
        private ComponentMapper<Moveable> _moveableMapper;
        private ComponentMapper<Zoomable> _zoomableMapper;

        public ControlSystem() : base(Aspect.All(typeof(Controlable)))
        {
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _moveableMapper = mapperService.GetMapper<Moveable>();
            _zoomableMapper = mapperService.GetMapper<Zoomable>();
        }

        public override void Process(GameTime gameTime, int entityId)
        {
            var keyboardState = KeyboardExtended.GetState();
            var mouseState = MouseExtended.GetState();

            var moveable = _moveableMapper.Get(entityId);
            if (moveable != null)
            {
                ProcessMovement(gameTime, moveable, keyboardState);
            }

            var zoomable = _zoomableMapper.Get(entityId);
            if (zoomable != null)
            {
                ProcessZoom(gameTime, zoomable, mouseState);
            }

        }

        private void ProcessMovement(GameTime gameTime, Moveable moveable, KeyboardStateExtended keyboardState)
        {
            var normalizedMotion = Vector2.Zero;
            if (keyboardState.IsKeyDown(Keys.W))
            {
                normalizedMotion += new Vector2(0, -1);
            }

            if (keyboardState.IsKeyDown(Keys.S))
            {
                normalizedMotion += new Vector2(0, 1);
            }

            if (keyboardState.IsKeyDown(Keys.A))
            {
                normalizedMotion += new Vector2(-1, 0);
            }

            if (keyboardState.IsKeyDown(Keys.D))
            {
                normalizedMotion += new Vector2(1, 0);
            }

            moveable.Velocity = normalizedMotion * moveable.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        private void ProcessZoom(GameTime gameTime, Zoomable zoomable, MouseStateExtended mouseState)
        {
            zoomable.Zoom -= (float)gameTime.ElapsedGameTime.TotalSeconds * 0.015f * mouseState.DeltaScrollWheelValue;
        }
    }
}
