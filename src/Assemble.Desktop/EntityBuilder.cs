using Assemble.Desktop.Components;
using Assemble.Desktop.Enums;
using Assemble.Desktop.Extensions;
using Assemble.Desktop.UnitConfiguration;
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

        public Entity BuildTile(Entity entity, (int X, int Y) tileIndex)
        {
            entity.Attach(new TilePosition(tileIndex));
            entity.Attach(new Sprite(_texturesManager.GetRandomTexture(Texture.Tile1, Texture.Tile2, Texture.Tile3, Texture.Tile4)));
            entity.Attach(new TileRenderLayer(TileRenderLayerType.GroundTile));
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

        public Entity BuildIronOrePatch(Entity entity, (int X, int Y) tileIndex)
        {
            entity.Attach(new MineableResource() { Amount = 100 });
            entity.Attach(new TilePosition(tileIndex));
            entity.Attach(new Sprite(_texturesManager.GetRandomTexture(Texture.IronOre1, Texture.IronOre2, Texture.IronOre3)));
            entity.Attach(new TileRenderLayer(TileRenderLayerType.Resources));
            entity.Attach(new MapTile(Color.LightBlue));
            entity.Attach(new Spacial());
            return entity;
        }

        public Entity BuildPlacementGuide(Entity entity, Texture spriteTexture, (int X, int Y) tileIndex, (int X, int Y) tileSpan)
        {
            entity.Attach(new TilePosition(tileIndex, tileSpan));
            entity.Attach(new Sprite(_texturesManager.GetRandomTexture(spriteTexture)) { Alpha = 0.7f });
            entity.Attach(new TileBorder() { Color = Color.LimeGreen });
            entity.Attach(new TileRenderLayer(TileRenderLayerType.Overlay));
            entity.Attach(new Placeable());
            return entity;
        }

        public Entity BuildUnit(Entity entity, (int x, int y) tileIndex, IUnitConfig unitConfig)
        {
            entity.Attach(new TilePosition(tileIndex, unitConfig.TileSpan));
            entity.Attach(new Sprite(_texturesManager.GetRandomTexture(unitConfig.Texture)));
            entity.Attach(new MapTile(unitConfig.MiniMapColor));
            entity.Attach(new TileRenderLayer(TileRenderLayerType.Units));
            entity.Attach(new Unit(unitConfig.UnitType));
            entity.Attach(new Spacial());
            return entity;
        }
    }
}
