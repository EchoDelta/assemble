using System;
using System.Collections.Generic;

namespace Assemble.Desktop
{
    public class GridManager
    {
        IList<int>[,] entities;

        public GridManager(int gridWidth, int gridHeight)
        {
            entities = new IList<int>[gridWidth, gridHeight];
            for (var x = 0; x < entities.GetLength(0); x++)
            {
                for (var y = 0; y < entities.GetLength(1); y++)
                {
                    entities[x, y] = new List<int>();
                }
            }
        }

        public bool AddUnit(int entityId, (int x, int y) tile, (int x, int y) tileSpan)
        {
            if (tile.x < 0 || tile.y < 0 || tile.x + tileSpan.x > entities.GetLength(0) || tile.y + tileSpan.y > entities.GetLength(1))
            {
                return false;
            }

            for (var x = tile.x; x < tile.x + tileSpan.x; x++)
            {
                for (var y = tile.y; y < tile.y + tileSpan.y; y++)
                {
                    entities[x, y].Add(entityId);
                }
            }
            return true;
        }

        public int[] GetUnitsInArea((int x, int y) tile, (int x, int y) tileSpan)
        {
            var units = new List<int>();
            for (var x = Math.Max(0, tile.x); x < Math.Min(entities.GetLength(0), tile.x + tileSpan.x); x++)
            {
                for (var y = Math.Max(0, tile.y); y < Math.Min(entities.GetLength(1), tile.y + tileSpan.y); y++)
                {
                    units.AddRange(entities[x, y]);
                }
            }
            return units.ToArray();
        }
    }
}