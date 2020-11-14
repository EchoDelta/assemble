using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using Assemble.Desktop.Components;
using Assemble.Desktop.Positioning;

namespace Assemble.Desktop.Systems
{
    public class SpatialTilePositionSystem : EntityUpdateSystem
    {
        private ComponentMapper<TilePosition> _tilePositionMapper;
        private ComponentMapper<Spatial> _spatialMapper;
        private readonly TileOccupationManager _tileOccupationManager;

        public SpatialTilePositionSystem(TileOccupationManager tileOccupationManager) : base(Aspect.All(typeof(TilePosition), typeof(Spatial)))
        {
            _tileOccupationManager = tileOccupationManager;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _tilePositionMapper = mapperService.GetMapper<TilePosition>();
            _spatialMapper = mapperService.GetMapper<Spatial>();
        }

        public override void Update(GameTime gameTime)
        {
            _tileOccupationManager.RemoveAllItems();
            foreach (var entity in ActiveEntities)
            {
                _tileOccupationManager.AddItem(entity, _tilePositionMapper.Get(entity));
            }
        }

        private void TrackEntity(int entityId, TilePosition tilePosition)
        {
            var added = _tileOccupationManager.AddItem(entityId, tilePosition);
            if (!added)
            {
                DestroyEntity(entityId);
            }
        }
    }
}