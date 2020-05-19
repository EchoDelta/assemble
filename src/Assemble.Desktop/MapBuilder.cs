using MonoGame.Extended;
using MonoGame.Extended.Entities;

namespace Assemble.Desktop
{
    class MapBuilder
    {
        private readonly World _world;
        private readonly EntityBuilder _entityBuilder;
        private readonly int _mapSize;

        public MapBuilder(World world, EntityBuilder entityBuilder, int mapSize)
        {
            _world = world;
            _entityBuilder = entityBuilder;
            _mapSize = mapSize;
        }

        public void BuildMap()
        {
            for (var x = 0; x < _mapSize; x++)
            {
                for (var y = 0; y < _mapSize; y++)
                {
                    _entityBuilder.BuildTile(_world.CreateEntity(), x, y);
                }
            }
        }
    }
}
