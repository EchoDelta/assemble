using Assemble.Desktop.Components;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;

namespace Assemble.Desktop.Systems
{
    public class TransportableMovementSystem : EntityUpdateSystem
    {
        private ComponentMapper<TilePosition> _tilePositionMapper;
        private ComponentMapper<Transportable> _transportableMapper;

        public TransportableMovementSystem() : base(Aspect.All(typeof(Transportable), typeof(TilePosition)))
        {
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _tilePositionMapper = mapperService.GetMapper<TilePosition>();
            _transportableMapper = mapperService.GetMapper<Transportable>();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var entityId in ActiveEntities)
            {
                var transportable = _transportableMapper.Get(entityId);
                var tilePosition = _tilePositionMapper.Get(entityId);
                tilePosition.Move(transportable.Velocity);
            }

            foreach (var entityId in ActiveEntities)
            {
                var transportable = _transportableMapper.Get(entityId);
                transportable.Velocity = Vector2.Zero;
            }
        }
    }
}