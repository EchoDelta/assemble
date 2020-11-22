using System.Linq;
using Assemble.Desktop.Components;
using Assemble.Desktop.Positioning;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;

namespace Assemble.Desktop.Systems
{
    public class TransportableMovementSystem : EntityUpdateSystem
    {
        private readonly TileOccupationManager _tileOccupationManager;
        private ComponentMapper<TilePosition> _tilePositionMapper;
        private ComponentMapper<Transportable> _transportableMapper;
        private ComponentMapper<Product> _productMapper;

        public TransportableMovementSystem(TileOccupationManager tileOccupationManager) : base(Aspect.All(typeof(Transportable), typeof(TilePosition)))
        {
            _tileOccupationManager = tileOccupationManager;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _tilePositionMapper = mapperService.GetMapper<TilePosition>();
            _transportableMapper = mapperService.GetMapper<Transportable>();
            _productMapper = mapperService.GetMapper<Product>();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var entityId in ActiveEntities)
            {
                var transportable = _transportableMapper.Get(entityId);
                var tilePosition = _tilePositionMapper.Get(entityId);

                var newPosition = tilePosition.Position + transportable.Velocity;
                if (!_tileOccupationManager
                    .GetItemsInArea(new RectangleF(newPosition.X, newPosition.Y, tilePosition.TileSpan.X, tilePosition.TileSpan.Y))
                    .Where(e => e != entityId)
                    .Any(e => _productMapper.Has(e)))
                {

                    tilePosition.Move(transportable.Velocity);
                }
            }

            foreach (var entityId in ActiveEntities)
            {
                var transportable = _transportableMapper.Get(entityId);
                transportable.Velocity = Vector2.Zero;
            }
        }
    }
}