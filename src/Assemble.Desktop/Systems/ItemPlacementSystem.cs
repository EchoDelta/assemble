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
        private Entity _currentPlaceableEntity;
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
                if (_currentPlaceableEntity != null)
                {
                    DestroyEntity(_currentPlaceableEntity.Id);
                }
                _currentPlaceableEntity = entityBuilder.BuildMiner(CreateEntity(), currentTile);
                entityBuilder.BuildPlacementGuide(_currentPlaceableEntity);
            }
            else if (keyboardState.IsKeyDown(Keys.Escape) && _currentPlaceableEntity != null)
            {
                DestroyEntity(_currentPlaceableEntity.Id);
                _currentPlaceableEntity = null;
            }

            if (mouseState.LeftButton == ButtonState.Released && _previousMouseState.LeftButton == ButtonState.Pressed && _currentPlaceableEntity != null)
            {
                entityBuilder.RemovePlacementGuide(_currentPlaceableEntity);
                _currentPlaceableEntity = null;
            }

            foreach (var entityId in ActiveEntities)
            {
                var placeable = _placeableMapper.Get(entityId);
                var tilePosition = _tilePositionMapper.Get(entityId);
                if (placeable != null && tilePosition != null)
                {
                    tilePosition.ChangeTile(currentTile);
                }
            }

            _previousMouseState = mouseState;
        }
    }
}