using Assemble.Desktop.Components;
using Assemble.Desktop.Extensions;
using Microsoft.Xna.Framework;
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

        public Entity BuildTile(Entity entity, int tileIndexX, int tileIndexY)
        {
            entity.Attach(new Transform2((tileIndexX, tileIndexY).MapFromTileIndexToPoint()));
            entity.Attach(new Sprite(_contentManager.Load<Texture2D>("Tile")));
            entity.Attach(new MapTile(Color.DarkGreen));
            return entity;
        }

        public Entity BuildGameCamera(Entity entity, OrthographicCamera camera, Vector2 initialPosition)
        {
            camera.Position = initialPosition.ToIsometric();
            entity.Attach(new GameCamera(camera));
            entity.Attach(new Moveable(200.0f));
            entity.Attach(new Zoomable(1.0f, 0.5f));
            entity.Attach(new Controlable());
            return entity;
        }
    }
}
