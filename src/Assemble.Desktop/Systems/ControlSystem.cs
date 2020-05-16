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

        public ControlSystem() : base(Aspect.All(typeof(Moveable), typeof(Controlable)))
        {
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _moveableMapper = mapperService.GetMapper<Moveable>();
        }

        public override void Process(GameTime gameTime, int entityId)
        {
            var keyboardState = KeyboardExtended.GetState();
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

            var moveable = _moveableMapper.Get(entityId);

            moveable.Velocity = normalizedMotion * moveable.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}
