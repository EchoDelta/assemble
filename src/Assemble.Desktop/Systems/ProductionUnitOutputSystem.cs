using System.Linq;
using Assemble.Desktop.Components;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;

namespace Assemble.Desktop.Systems
{
    public class ProductionUnitOutputSystem : EntityProcessingSystem
    {
        private ComponentMapper<ProductionUnit> _productionUnitMapper;
        private ComponentMapper<TilePosition> _tilePositionMapper;
        private readonly GridManager _gridManager;

        public ProductionUnitOutputSystem(GridManager gridManager) : base(Aspect.All(typeof(ProductionUnit), typeof(TilePosition)))
        {
            _gridManager = gridManager;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _productionUnitMapper = mapperService.GetMapper<ProductionUnit>();
            _tilePositionMapper = mapperService.GetMapper<TilePosition>();
        }

        public override void Process(GameTime gameTime, int entityId)
        {
            var productionUnit = _productionUnitMapper.Get(entityId);
            var tilePosition = _tilePositionMapper.Get(entityId);

            if (!productionUnit.OutputBuffer.Any())
            {
                return;
            }

            var outputPosition = new TilePosition(((int)tilePosition.Position.X, (int)tilePosition.Position.Y - 1), (1, 1));
            int outputProductEntityId = productionUnit.OutputBuffer[0];
            productionUnit.OutputBuffer.RemoveAt(0);
            var outputProductEntity = GetEntity(outputProductEntityId);
            outputProductEntity.Attach(outputPosition);
        }
    }
}