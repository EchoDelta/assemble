using System;
using System.Collections.Generic;
using System.Linq;
using Assemble.Desktop.Components;
using Assemble.Desktop.Enums;
using Assemble.Desktop.Positioning;
using Assemble.Desktop.UnitConfiguration;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;

namespace Assemble.Desktop.Systems
{
    public class MinerProcessingSystem : EntityProcessingSystem
    {
        private readonly TileOccupationManager _tileOccupationManager;
        private readonly EntityBuilder _entityBuilder;
        private ComponentMapper<Unit> _unitMapper;
        private ComponentMapper<ProductionUnit> _productionUnitMapper;
        private ComponentMapper<TilePosition> _tilePositionMapper;
        private ComponentMapper<MineableResource> _mineableResourceMapper;

        public MinerProcessingSystem(TileOccupationManager tileOccupationManager, EntityBuilder entityBuilder) : base(Aspect.All(typeof(Unit), typeof(ProductionUnit), typeof(TilePosition)))
        {
            _tileOccupationManager = tileOccupationManager;
            _entityBuilder = entityBuilder;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _unitMapper = mapperService.GetMapper<Unit>();
            _productionUnitMapper = mapperService.GetMapper<ProductionUnit>();
            _tilePositionMapper = mapperService.GetMapper<TilePosition>();
            _mineableResourceMapper = mapperService.GetMapper<MineableResource>();
        }

        public override void Process(GameTime gameTime, int entityId)
        {
            var unitType = _unitMapper.Get(entityId).Type;
            if (unitType != UnitType.Miner)
            {
                return;
            }

            var productionUnit = _productionUnitMapper.Get(entityId);
            var tilePosition = _tilePositionMapper.Get(entityId);

            var activeMineableResources = GetActiveMineableResources(tilePosition);

            SetProductionUnitActive(productionUnit, activeMineableResources);

            if (productionUnit.ProductionActive)
            {
                productionUnit.CurrentProductionTime += gameTime.ElapsedGameTime;
            }
            if (productionUnit.CurrentProductionTime >= productionUnit.ProductionSpeed)
            {
                productionUnit.CurrentProductionTime -= productionUnit.ProductionSpeed;
                Produce(productionUnit, activeMineableResources.ToList());
            }
        }

        private IEnumerable<MineableResource> GetActiveMineableResources(TilePosition tilePosition)
        {
            return _tileOccupationManager.GetItemsInArea(tilePosition.GetArea()).Select(i => _mineableResourceMapper.Get(i)).Where(r => r != null);
        }

        private void Produce(ProductionUnit productionUnit, IList<MineableResource> activeMineableResources)
        {
            var random = new Random();

            activeMineableResources[random.Next(0, activeMineableResources.Count())].Amount--;
            var product = _entityBuilder.BuildProduct(CreateEntity(), ProductType.IronOre);
            productionUnit.OutputBuffer.Add(product.Id);
        }

        private void SetProductionUnitActive(ProductionUnit productionUnit, IEnumerable<MineableResource> activeMineableResources)
        {
            productionUnit.ProductionActive = productionUnit.OutputBuffer.Count < productionUnit.OutputBufferSize && activeMineableResources.Any();
        }
    }
}