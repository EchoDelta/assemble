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
        private ComponentMapper<TilePosition> _tilePositionMapper;
        private readonly EntityBuilder entityBuilder;
        private readonly OrthographicCamera camera;
        private MouseStateExtended _previousMouseState;

        public ItemPlacementSystem(EntityBuilder entityBuilder, OrthographicCamera camera) : base(Aspect.All(typeof(Placeable)))
        {
            this.camera = camera;
            this.entityBuilder = entityBuilder;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _placeableMapper = mapperService.GetMapper<Placeable>();
            _tilePositionMapper = mapperService.GetMapper<TilePosition>();
        }

        public override void Update(GameTime gameTime)
        {
            var mouseState = MouseExtended.GetState();
            var keyboardState = KeyboardExtended.GetState();

            var mousePosition = camera.ScreenToWorld(mouseState.Position.ToVector2()).FromIsometric();

            var currentSize = (2, 2);
            var currentTile = mousePosition.MapFromCenterTilePointToTopRightTileIndex(currentSize);

            if (keyboardState.IsKeyDown(Keys.D1))
            {
                entityBuilder.BuildPlacementGuide(CreateEntity(), currentTile, currentSize);
            }
            else if (keyboardState.IsKeyDown(Keys.Escape) && _currentPlaceableEntityId.HasValue)
            {
                DestroyEntity(_currentPlaceableEntityId.Value);
            }

            if (mouseState.LeftButton == ButtonState.Released && _previousMouseState.LeftButton == ButtonState.Pressed && _currentPlaceableEntityId.HasValue)
            {
                entityBuilder.BuildMiner(CreateEntity(), currentTile);
            }

            foreach (var entityId in ActiveEntities)
            {
                var placeable = _placeableMapper.Get(entityId);
                var tilePosition = _tilePositionMapper.Get(entityId);
                if (tilePosition != null)
                {
                    tilePosition.ChangeTile(currentTile);
                }
            }

            _previousMouseState = mouseState;
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