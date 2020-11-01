using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using Assemble.Desktop.Components;

namespace Assemble.Desktop.Systems
{
    public class SpacialTilePositionSystem : EntityUpdateSystem
    {
        private ComponentMapper<TilePosition> _tilePositionMapper;
        private ComponentMapper<Spacial> _spacialMapper;
        private readonly GridManager _gridManager;

        public SpacialTilePositionSystem(GridManager gridManager) : base(Aspect.All(typeof(TilePosition)))
        {
            _gridManager = gridManager;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _tilePositionMapper = mapperService.GetMapper<TilePosition>();
            _spacialMapper = mapperService.GetMapper<Spacial>();
        }

        public override void Update(GameTime gameTime)
        {

        }

        protected override void OnEntityAdded(int entityId)
        {
            var tilePosition = _tilePositionMapper.Get(entityId);
            if (tilePosition != null && _spacialMapper.Has(entityId))
            {
                var added = _gridManager.AddItem(entityId, ((int)tilePosition.Position.X, (int)tilePosition.Position.Y), tilePosition.TileSpan);
                if (!added)
                {
                    DestroyEntity(entityId);
                }
            }
        }
    }
}