using System;
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
            entity.Attach(new Spatial());
            return entity;
        }

        public Entity BuildPlacementGuide(Entity entity, IUnitConfig unitConfig, (int X, int Y) tileIndex)
        {
            entity.Attach(new TilePosition(tileIndex, unitConfig.TileSpan));
            var directionalTexture = unitConfig.GetDirectionalTexture();
            directionalTexture.Alpha = 0.7f;
            entity.Attach(directionalTexture);
            entity.Attach(new Alignable(Direction.NorthEast));
            entity.Attach(new TileBorder() { Color = Color.LimeGreen });
            entity.Attach(new TileRenderLayer(TileRenderLayerType.Overlay));
            entity.Attach(new Placeable());
            return entity;
        }

        public Entity BuildUnit(Entity entity, (int x, int y) tileIndex, IUnitConfig unitConfig, Direction direction)
        {
            entity.Attach(new TilePosition(tileIndex, unitConfig.TileSpan));
            entity.Attach(unitConfig.GetDirectionalTexture());
            entity.Attach(new Alignable(direction));
            entity.Attach(new MapTile(unitConfig.MiniMapColor));
            entity.Attach(new TileRenderLayer(TileRenderLayerType.Units));
            entity.Attach(new Unit(unitConfig.UnitType));
            entity.Attach(new Spatial());
            if (unitConfig.ProductionSpeed.HasValue || unitConfig.OutputBufferSize.HasValue)
            {
                entity.Attach(new ProductionUnit(unitConfig.ProductionSpeed ?? TimeSpan.FromSeconds(60), unitConfig.OutputBufferSize ?? 1));
            }
            if (unitConfig.Blockable)
            {
                entity.Attach(new Blockable());
            }
            if (unitConfig.TransportationSpeed.HasValue)
            {
                entity.Attach(new Transporter(unitConfig.TransportationSpeed.Value));
            }

            return entity;
        }

        public Entity BuildProduct(Entity entity, ProductType productType)
        {
            entity.Attach(new Product(productType));
            entity.Attach(new Sprite(_texturesManager.GetRandomTexture(Texture.IronOre1, Texture.IronOre2, Texture.IronOre3)));
            entity.Attach(new TileRenderLayer(TileRenderLayerType.Products));
            entity.Attach(new Spatial());
            return entity;
        }
    }
}
