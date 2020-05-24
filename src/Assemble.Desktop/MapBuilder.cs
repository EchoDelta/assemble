using System;
using MonoGame.Extended;
using MonoGame.Extended.Entities;

namespace Assemble.Desktop
{
    class MapBuilder
    {
        private readonly World _world;
        private readonly EntityBuilder _entityBuilder;
        private readonly int _mapSize;
        private readonly Random _randomizer;

        public MapBuilder(World world, EntityBuilder entityBuilder, int mapSize)
        {
            _world = world;
            _entityBuilder = entityBuilder;
            _mapSize = mapSize;
            _randomizer = new Random();
        }

        public void BuildMap()
        {
            AddTiles();
            AddResources();
        }

        private void AddTiles()
        {
            for (var x = 0; x < _mapSize; x++)
            {
                for (var y = 0; y < _mapSize; y++)
                {
                    _entityBuilder.BuildTile(_world.CreateEntity(), x, y);
                }
            }
        }

        private void AddResources()
        {
            var numIronOreFields = 5;
            var sizeIronOreFields = 10;

            for (var _ = 0; _ < numIronOreFields; _++)
            {
                var oreFieldStartX = _randomizer.Next(0, _mapSize - sizeIronOreFields);
                var oreFieldStartY = _randomizer.Next(0, _mapSize - sizeIronOreFields);
                for (var x = oreFieldStartX; x < oreFieldStartX + sizeIronOreFields; x++)
                {
                    for (var y = oreFieldStartY; y < oreFieldStartY + sizeIronOreFields; y++)
                    {
                        _entityBuilder.BuildIronOrePatch(_world.CreateEntity(), x, y);
                    }
                }
            }
        }
    }
}
