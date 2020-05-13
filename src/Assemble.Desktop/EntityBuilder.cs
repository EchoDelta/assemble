using Assemble.Desktop.Extensions;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Sprites;

namespace Assemble.Desktop
{
    class EntityBuilder
    {
        private readonly ContentManager _contentManager;

        public EntityBuilder(ContentManager contentManager)
        {
            _contentManager = contentManager;
        }

        public Entity BuildTile(Entity entity, Point2 tileCoordinate)
        {
            entity.Attach(new Transform2(tileCoordinate.MapFromTileCoordinateToCenterPoint()));
            entity.Attach(new Sprite(_contentManager.Load<Texture2D>("Tile")));
            return entity;
        }
    }
}
