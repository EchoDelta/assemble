using System;
using System.Collections.Generic;
using System.Linq;
using Assemble.Desktop.Components;
using MonoGame.Extended;

namespace Assemble.Desktop.Positioning
{
    public class TileOccupationManager
    {
        List<(int entityId, TilePosition tilePosition)>[,] entities;

        public TileOccupationManager(int gridWidth, int gridHeight)
        {
            entities = new List<(int, TilePosition)>[gridWidth, gridHeight];
            for (var x = 0; x < entities.GetLength(0); x++)
            {
                for (var y = 0; y < entities.GetLength(1); y++)
                {
                    entities[x, y] = new List<(int, TilePosition)>();
                }
            }
        }

        public bool AddItem(int entityId, TilePosition tilePosition)
        {
            var (posX, posY) = ((int)tilePosition.Position.X, (int)tilePosition.Position.Y);
            var (endPosX, endPosY) = ((int)Math.Ceiling(tilePosition.Position.X + tilePosition.TileSpan.X), (int)Math.Ceiling(tilePosition.Position.Y + tilePosition.TileSpan.Y));

            if (posX < 0 || posY < 0 || endPosX > entities.GetLength(0) || endPosY > entities.GetLength(1))
            {
                return false;
            }

            var item = (entityId, tilePosition);

            for (var x = posX; x < endPosX; x++)
            {
                for (var y = posY; y < endPosY; y++)
                {
                    entities[x, y].Add(item);
                }
            }
            return true;
        }

        public void RemoveAllItems()
        {
            foreach (var items in entities)
            {
                items.Clear();
            }
        }

        public IEnumerable<int> GetItemsInArea(RectangleF area)
        {
            var possibleItems = GetItemsInTiles((int)area.TopLeft.X, (int)area.TopLeft.Y, (int)area.BottomRight.X, (int)area.BottomRight.Y).Distinct();
            foreach (var possibleItem in possibleItems)
            {
                if (area.Intersects(possibleItem.tilePosition.GetArea()))
                {
                    yield return possibleItem.entityId;
                }
            }
        }

        public IEnumerable<int> GetItemsInTile(int x, int y)
        {
            return GetItemsInTiles(x, y, x, y).Select(i => i.entityId);
        }

        private IEnumerable<(int entityId, TilePosition tilePosition)> GetItemsInTiles(int startTileX, int startTileY, int endTileX, int endTileY)
        {
            for (var x = Math.Max(0, startTileX); x < Math.Min(entities.GetLength(0), endTileX + 1); x++)
            {
                for (var y = Math.Max(0, startTileY); y < Math.Min(entities.GetLength(1), endTileY + 1); y++)
                {
                    foreach (var item in entities[x, y])
                    {
                        yield return item;
                    }
                }
            }
        }
    }
}