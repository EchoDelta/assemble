using System.Linq;
using Assemble.Desktop.Components;
using Assemble.Desktop.Positioning;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;

namespace Assemble.Desktop.Systems
{
    public class TransporterTransportSystem : EntityProcessingSystem
    {
        private readonly TileOccupationManager _tileOccupationManager;
        private ComponentMapper<Transporter> _transporterMapper;
        private ComponentMapper<TilePosition> _tilePositionMapper;
        private ComponentMapper<Transportable> _transportableMapper;

        public TransporterTransportSystem(TileOccupationManager tileOccupationManager) : base(Aspect.All(typeof(Transporter), typeof(TilePosition)))
        {
            _tileOccupationManager = tileOccupationManager;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _transporterMapper = mapperService.GetMapper<Transporter>();
            _tilePositionMapper = mapperService.GetMapper<TilePosition>();
            _transportableMapper = mapperService.GetMapper<Transportable>();
        }

        public override void Process(GameTime gameTime, int entityId)
        {
            var transporter = _transporterMapper.Get(entityId);
            var tilePosition = _tilePositionMapper.Get(entityId);

            var transportables = _tileOccupationManager.GetItemsInArea(tilePosition.GetArea())
                .Where(entity => _transportableMapper.Has(entity))
                .Select(entity => _transportableMapper.Get(entity));

            foreach (var transportable in transportables)
            {
                var motion = new Vector2(0, (float)(-transporter.TransportSpeed * gameTime.ElapsedGameTime.TotalSeconds));
                if (IsTransporterAtNextTile(tilePosition, motion))
                {
                    transportable.Velocity = motion;
                }
            }
        }

        private bool IsTransporterAtNextTile(TilePosition transporterTilePosition, Vector2 motion)
        {
            var nextTilePostition = transporterTilePosition.Position + motion.NormalizedCopy();
            var (x, y) = ((int)nextTilePostition.X, (int)nextTilePostition.Y);
            return _tileOccupationManager.GetItemsInTile(x, y).Any(e => _transporterMapper.Has(e));
        }
    }
}