using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using Assemble.Desktop.Components;
using MonoGame.Extended.Input;
using Assemble.Desktop.Extensions;
using MonoGame.Extended;
using Microsoft.Xna.Framework.Input;

namespace Assemble.Desktop.Systems
{
    public class ItemPlacementSystem : EntityUpdateSystem
    {
        private int? _currentPlaceableEntityId;
        private ComponentMapper<Placeable> _placeableMapper;
        private ComponentMapper<Transform2> _transformMapper;
        private readonly EntityBuilder entityBuilder;
        private readonly OrthographicCamera camera;

        public ItemPlacementSystem(EntityBuilder entityBuilder, OrthographicCamera camera) : base(Aspect.All(typeof(Placeable)))
        {
            this.camera = camera;
            this.entityBuilder = entityBuilder;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _placeableMapper = mapperService.GetMapper<Placeable>();
            _transformMapper = mapperService.GetMapper<Transform2>();
        }

        public override void Update(GameTime gameTime)
        {
            var mouseState = MouseExtended.GetState();
            var keyboardState = KeyboardExtended.GetState();

            var currentTile = camera.ScreenToWorld(mouseState.Position.ToVector2()).FromIsometric().MapToTileIndexFromVector();

            if (keyboardState.IsKeyDown(Keys.D1))
            {
                entityBuilder.BuildPlaceableItem(CreateEntity(), currentTile.x, currentTile.y, 5);
            }
            else if (keyboardState.IsKeyDown(Keys.Escape) && _currentPlaceableEntityId.HasValue)
            {
                DestroyEntity(_currentPlaceableEntityId.Value);
            }

            foreach (var entityId in ActiveEntities)
            {
                var transform = _transformMapper.Get(entityId);
                if (transform != null)
                {
                    transform.Position = currentTile.MapFromTileIndexToPoint();
                }
            }
        }

        protected override void OnEntityAdded(int entityId)
        {
            var placeable = _placeableMapper.Get(entityId);
            if (placeable != null)
            {
                if (_currentPlaceableEntityId.HasValue)
                {
                    DestroyEntity(_currentPlaceableEntityId.Value);
                }
                _currentPlaceableEntityId = entityId;
            }

            base.OnEntityAdded(entityId);
        }

        protected override void OnEntityRemoved(int entityId)
        {
            var placeable = _placeableMapper.Get(entityId);
            if (placeable != null)
            {
                if (_currentPlaceableEntityId == entityId)
                {
                    _currentPlaceableEntityId = null;
                }
            }

            base.OnEntityRemoved(entityId);
        }
    }
}