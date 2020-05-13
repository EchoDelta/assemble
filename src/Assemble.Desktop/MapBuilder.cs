using MonoGame.Extended;
using MonoGame.Extended.Entities;

namespace Assemble.Desktop
{
    class MapBuilder
    {
        private readonly World _world;
        private readonly EntityBuilder _entityBuilder;
        private readonly int _sizeX;
        private readonly int _sizeY;

        public MapBuilder(World world, EntityBuilder entityBuilder, int sizeX, int sizeY)
        {
            _world = world;
            _entityBuilder = entityBuilder;
            _sizeX = sizeX;
            _sizeY = sizeY;
        }

        public void BuildMap()
        {
            for(var x = 0; x < _sizeX; x++)
            {
                for(var y = 0; y < _sizeY; y++)
                {
                    _entityBuilder.BuildTile(_world.CreateEntity(), new Point2(x, y));
                }
            }
        }
    }
}
