using Assemble.Desktop.Components;
using Assemble.Desktop.Extensions;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Sprites;

namespace Assemble.Desktop
{
    public class EntityBuilder
    {
        private readonly TexturesManager _texturesManager;

        public EntityBuilder(TexturesManager texturesManager)
        {
            _texturesManager = texturesManager;
        }

        public Entity BuildTile(Entity entity, int tileIndexX, int tileIndexY)
        {
            entity.Attach(new Transform2((tileIndexX, tileIndexY).MapFromTileIndexToPoint()));
            entity.Attach(new Sprite(_texturesManager.GetTexture(Texture.Tile1, Texture.Tile2, Texture.Tile3, Texture.Tile4)));
            entity.Attach(new MapTile(Color.DarkGreen));
            return entity;
        }

        public Entity BuildGameCamera(Entity entity, OrthographicCamera camera, Vector2 initialPosition)
        {
            camera.Position = initialPosition.ToIsometric();
            entity.Attach(new GameCamera(camera));
            entity.Attach(new Moveable(200.0f));
            entity.Attach(new Zoomable(1.0f, 0.25f));
            entity.Attach(new Controlable());
            return entity;
        }

        public Entity BuildIronOrePatch(Entity entity, int tileIndexX, int tileIndexY)
        {
            entity.Attach(new Transform2((tileIndexX, tileIndexY).MapFromTileIndexToPoint()));
            entity.Attach(new Sprite(_texturesManager.GetTexture(Texture.IronOre1, Texture.IronOre2, Texture.IronOre3)));
            entity.Attach(new MapTile(Color.LightBlue));
            return entity;
        }

        public Entity BuildPlaceableItem(Entity entity, int initialTileIndexX, int initialTileIndexY, int size)
        {
            entity.Attach(new Transform2((initialTileIndexX, initialTileIndexY).MapFromTileIndexToPoint()));
            entity.Attach(new GridBorder { GridSizeX = size, GridSizeY = size });
            entity.Attach(new Placeable { GridSizeX = size, GridSizeY = size });
            return entity;
        }
    }
}
