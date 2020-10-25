using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using Assemble.Desktop.Components;
using MonoGame.Extended.Input;
using Assemble.Desktop.Extensions;
using MonoGame.Extended;
using Microsoft.Xna.Framework.Input;
using System.Linq;

namespace Assemble.Desktop.Systems
{
    public class ItemPlacementSystem : EntityUpdateSystem
    {
        private Entity _currentPlaceableEntity;
        private ComponentMapper<Placeable> _placeableMapper;
        private ComponentMapper<TilePosition> _tilePositionMapper;
        private ComponentMapper<TileBorder> _tileBorderMapper;
        private readonly EntityBuilder _entityBuilder;
        private readonly OrthographicCamera _camera;
        private readonly GridManager _gridManager;
        private MouseStateExtended _previousMouseState;


        public ItemPlacementSystem(EntityBuilder entityBuilder, OrthographicCamera camera, GridManager gridManager) : base(Aspect.All(typeof(Placeable)))
        {
            _camera = camera;
            _gridManager = gridManager;
            _entityBuilder = entityBuilder;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _placeableMapper = mapperService.GetMapper<Placeable>();
            _tilePositionMapper = mapperService.GetMapper<TilePosition>();
            _tileBorderMapper = mapperService.GetMapper<TileBorder>();
        }

        public override void Update(GameTime gameTime)
        {
            var mouseState = MouseExtended.GetState();
            var keyboardState = KeyboardExtended.GetState();

            var mousePosition = _camera.ScreenToWorld(mouseState.Position.ToVector2()).FromIsometric();

            var currentSize = (2, 2);
            var currentTile = mousePosition.MapFromCenterTilePointToTopRightTileIndex(currentSize);

            var currentTileOccupied = _gridManager.GetUnitsInArea(currentTile, currentSize).Any();

            if (keyboardState.IsKeyDown(Keys.D1))
            {
                if (_currentPlaceableEntity != null)
                {
                    DestroyEntity(_currentPlaceableEntity.Id);
                }
                _currentPlaceableEntity = _entityBuilder.BuildPlacementGuide(CreateEntity(), Texture.Miner, currentTile, currentSize);
            }
            else if (keyboardState.IsKeyDown(Keys.Escape) && _currentPlaceableEntity != null)
            {
                DestroyEntity(_currentPlaceableEntity.Id);
                _currentPlaceableEntity = null;
            }

            if (mouseState.LeftButton == ButtonState.Released && _previousMouseState.LeftButton == ButtonState.Pressed
                && _currentPlaceableEntity != null && !currentTileOccupied)
            {
                _entityBuilder.BuildMiner(CreateEntity(), currentTile);
            }

            foreach (var entityId in ActiveEntities)
            {
                var placeable = _placeableMapper.Get(entityId);
                var tilePosition = _tilePositionMapper.Get(entityId);
                var tileBorder = _tileBorderMapper.Get(entityId);

                if (placeable == null)
                {
                    continue;
                }

                if (tilePosition != null)
                {
                    tilePosition.ChangeTile(currentTile);
                }

                tileBorder.Color = currentTileOccupied ? Color.Red : Color.LimeGreen;
            }

            _previousMouseState = mouseState;
        }
    }
}