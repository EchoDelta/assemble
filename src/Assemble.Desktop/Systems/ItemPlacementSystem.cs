using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using Assemble.Desktop.Components;
using MonoGame.Extended.Input;
using Assemble.Desktop.Extensions;
using MonoGame.Extended;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using Assemble.Desktop.UnitConfiguration;
using Assemble.Desktop.Positioning;

namespace Assemble.Desktop.Systems
{
    public class UnitPlacementSystem : EntityUpdateSystem
    {
        private Entity _currentPlaceableEntity;
        private IUnitConfig _currentPlaceableUnitConfig;
        private ComponentMapper<Placeable> _placeableMapper;
        private ComponentMapper<TilePosition> _tilePositionMapper;
        private ComponentMapper<TileBorder> _tileBorderMapper;
        private ComponentMapper<Unit> _unitMapper;
        private ComponentMapper<Alignable> _alignableMapper;
        private readonly EntityBuilder _entityBuilder;
        private readonly OrthographicCamera _camera;
        private readonly TileOccupationManager _tileOccupationManager;
        private MouseStateExtended _previousMouseState;

        private bool isRotating = false;


        public UnitPlacementSystem(EntityBuilder entityBuilder, OrthographicCamera camera, TileOccupationManager tileOccupationManager) : base(Aspect.All(typeof(Placeable)))
        {
            _camera = camera;
            _tileOccupationManager = tileOccupationManager;
            _entityBuilder = entityBuilder;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _placeableMapper = mapperService.GetMapper<Placeable>();
            _tilePositionMapper = mapperService.GetMapper<TilePosition>();
            _tileBorderMapper = mapperService.GetMapper<TileBorder>();
            _unitMapper = mapperService.GetMapper<Unit>();
            _alignableMapper = mapperService.GetMapper<Alignable>();
        }

        public override void Update(GameTime gameTime)
        {
            var mouseState = MouseExtended.GetState();
            var keyboardState = KeyboardExtended.GetState();

            var mousePosition = _camera.ScreenToWorld(mouseState.Position.ToVector2()).FromIsometric();

            (int, int) currentTile = mousePosition.MapFromCenterTilePointToTopRightTileIndex(_currentPlaceableUnitConfig?.TileSpan ?? (1, 1));

            // TODO: Introduce state machine :D
            if (keyboardState.IsKeyDown(Keys.D1))
            {
                MakeNewPlacementGuide(new MinerUnitConfig(), currentTile);
            }

            if (keyboardState.IsKeyDown(Keys.D2))
            {
                MakeNewPlacementGuide(new ConveyorBeltUnitConfig(), currentTile);
            }

            if (keyboardState.IsKeyDown(Keys.Escape) && _currentPlaceableEntity != null)
            {
                ClearPlacementGuide();
            }

            if (keyboardState.IsKeyDown(Keys.R) && _currentPlaceableEntity != null && !isRotating)
            {
                isRotating = true;
                var alignable = _currentPlaceableEntity.Get<Alignable>();
                if (alignable != null)
                {
                    alignable.Rotate();
                }
            }
            else if (keyboardState.IsKeyUp(Keys.R))
            {
                isRotating = false;
            }

            var currentTileOccupied = IsCurrentTileOccupied(currentTile);

            if (mouseState.LeftButton == ButtonState.Released && _previousMouseState.LeftButton == ButtonState.Pressed
                && _currentPlaceableEntity != null && !currentTileOccupied)
            {
                var alignable = _currentPlaceableEntity.Get<Alignable>();
                _entityBuilder.BuildUnit(CreateEntity(), currentTile, _currentPlaceableUnitConfig, alignable.Direction);
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

        private bool IsCurrentTileOccupied((int x, int y) currentTile)
        {
            var (tileSpanX, tileSpanY) = _currentPlaceableUnitConfig?.TileSpan ?? (1, 1);
            return _tileOccupationManager.GetItemsInArea(new RectangleF(currentTile.x, currentTile.y, tileSpanX, tileSpanY)).Any(item => _unitMapper.Has(item));
        }

        private void MakeNewPlacementGuide(IUnitConfig unitConfig, (int, int) currentTile)
        {
            _currentPlaceableUnitConfig = unitConfig;
            if (_currentPlaceableEntity != null)
            {
                DestroyEntity(_currentPlaceableEntity.Id);
            }
            _currentPlaceableEntity = _entityBuilder.BuildPlacementGuide(CreateEntity(), _currentPlaceableUnitConfig, currentTile);
        }

        private void ClearPlacementGuide()
        {
            _currentPlaceableUnitConfig = null;
            DestroyEntity(_currentPlaceableEntity.Id);
            _currentPlaceableEntity = null;
        }
    }
}