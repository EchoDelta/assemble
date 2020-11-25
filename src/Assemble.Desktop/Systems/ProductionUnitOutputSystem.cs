using System.Linq;
using Assemble.Desktop.Components;
using Assemble.Desktop.Enums;
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
        private ComponentMapper<Alignable> _alignableMapper;
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
            _alignableMapper = mapperService.GetMapper<Alignable>();
        }

        public override void Process(GameTime gameTime, int entityId)
        {
            var productionUnit = _productionUnitMapper.Get(entityId);
            var tilePosition = _tilePositionMapper.Get(entityId);

            if (!productionUnit.OutputBuffer.Any())
            {
                return;
            }

            var outputDirection = _alignableMapper.Get(entityId)?.Direction ?? Direction.NorthEast;

            var outputPosition = GetOutputPosition(tilePosition, outputDirection);

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

        private static TilePosition GetOutputPosition(TilePosition unitPosition, Direction outputDirection)
        {
            var newPosition = outputDirection switch
            {
                Direction.NorthEast => unitPosition.Position + new Vector2(0, -1),
                Direction.SouthEast => unitPosition.Position + new Vector2(unitPosition.TileSpan.X, 0),
                Direction.SouthWest => unitPosition.Position + new Vector2(unitPosition.TileSpan.X - 1, unitPosition.TileSpan.Y),
                Direction.NorthWest => unitPosition.Position + new Vector2(-1, unitPosition.TileSpan.Y - 1),
                _ => unitPosition.Position + new Vector2(0, -1),
            };
            return new TilePosition(((int)newPosition.X, (int)newPosition.Y), (1, 1));
        }

        private bool OutputPositionIsOccupied(TilePosition outputPosition)
        {
            return _tileOccupationManager.GetItemsInArea(outputPosition.GetArea()).Any(ie => _blockableMapper.Has(ie) || _productMapper.Has(ie));
        }
    }
}