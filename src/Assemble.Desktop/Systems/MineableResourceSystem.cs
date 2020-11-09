using Assemble.Desktop.Components;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;

namespace Assemble.Desktop.Systems
{
    public class MineableResourceSystem : EntityProcessingSystem
    {
        private ComponentMapper<MineableResource> _mineableResourceMapper;

        public MineableResourceSystem() : base(Aspect.All(typeof(MineableResource)))
        {
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _mineableResourceMapper = mapperService.GetMapper<MineableResource>();
        }

        public override void Process(GameTime gameTime, int entityId)
        {
            var mineableResource = _mineableResourceMapper.Get(entityId);
            if (mineableResource.Amount <= 0)
            {
                DestroyEntity(entityId);
            }
        }
    }
}