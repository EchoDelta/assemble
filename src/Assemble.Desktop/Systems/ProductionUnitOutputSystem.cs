using System.Linq;
using Assemble.Desktop.Components;
using Assemble.Desktop.Positioning;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;

namespace Assemble.Desktop.Systems
{
    public class ProductionUnitOutputSystem : EntityProcessingSystem
    {
        private ComponentMapper<ProductionUnit> _productionUnitMapper;
        private ComponentMapper<TilePosition> _tilePositionMapper;
        private ComponentMapper<Blockable> _blockableMapper;
        private ComponentMapper<Product> _productMapper;
        private readonly TileOccupationManager _tileOccupationManager;

        public ProductionUnitOutputSystem(TileOccupationManager tileOccupationManager) : base(Aspect.All(typeof(ProductionUnit), typeof(TilePosition)))
        {
            _tileOccupationManager = tileOccupationManager;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _productionUnitMapper = mapperService.GetMapper<ProductionUnit>();
            _tilePositionMapper = mapperService.GetMapper<TilePosition>();
            _blockableMapper = mapperService.GetMapper<Blockable>();
            _productMapper = mapperService.GetMapper<Product>();
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

            if (OutputPositionIsOccupied(outputPosition))
            {
                return;
            }

            int outputProductEntityId = productionUnit.OutputBuffer[0];
            productionUnit.OutputBuffer.RemoveAt(0);
            var outputProductEntity = GetEntity(outputProductEntityId);
            outputProductEntity.Attach(outputPosition);
            outputProductEntity.Attach(new Transportable());
        }

        private bool OutputPositionIsOccupied(TilePosition outputPosition)
        {
            return _tileOccupationManager.GetItemsInArea(outputPosition.GetArea()).Any(ie => _blockableMapper.Has(ie) || _productMapper.Has(ie));
        }
    }
}