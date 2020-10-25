using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using Assemble.Desktop.Components;

namespace Assemble.Desktop.Systems
{
    public class UnitGridOccupationSystem : EntityUpdateSystem
    {
        private ComponentMapper<Unit> _unitMapper;
        private ComponentMapper<TilePosition> _tilePositionMapper;
        private readonly GridManager _gridManager;

        public UnitGridOccupationSystem(GridManager gridManager) : base(Aspect.All(typeof(Unit), typeof(TilePosition)))
        {
            _gridManager = gridManager;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _unitMapper = mapperService.GetMapper<Unit>();
            _tilePositionMapper = mapperService.GetMapper<TilePosition>();
        }

        public override void Update(GameTime gameTime)
        {

        }

        protected override void OnEntityAdded(int entityId)
        {
            var tilePosition = _tilePositionMapper.Get(entityId);
            var unit = _unitMapper.Get(entityId);
            if (tilePosition != null && unit != null)
            {
                var added = _gridManager.AddUnit(entityId, ((int)tilePosition.Position.X, (int)tilePosition.Position.Y), tilePosition.TileSpan);
                if (!added)
                {
                    DestroyEntity(entityId);
                }
            }
        }
    }
}