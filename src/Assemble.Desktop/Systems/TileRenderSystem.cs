using System.Collections.Generic;
using System.Linq;
using Assemble.Desktop.Components;
using Assemble.Desktop.Enums;
using Assemble.Desktop.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.Shapes;
using MonoGame.Extended.Sprites;

namespace Assemble.Desktop.Systems
{
    class TileRenderSystem : EntityDrawSystem
    {
        private readonly SpriteBatch _spriteBatch;
        private readonly OrthographicCamera _camera;
        private readonly DepthHelper _depthHelper;
        private ComponentMapper<TilePosition> _tilePositionMapper;
        private ComponentMapper<Sprite> _spriteMapper;
        private ComponentMapper<TileBorder> _tileBorderMapper;
        private ComponentMapper<TileRenderLayer> _tileRenderLayerMapper;

        public TileRenderSystem(SpriteBatch spriteBatch, OrthographicCamera camera, DepthHelper depthHelper) : base(Aspect.All(typeof(TilePosition)).One(typeof(Sprite), typeof(TileBorder)))
        {
            _depthHelper = depthHelper;
            _spriteBatch = spriteBatch;
            _camera = camera;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _tilePositionMapper = mapperService.GetMapper<TilePosition>();
            _spriteMapper = mapperService.GetMapper<Sprite>();
            _tileBorderMapper = mapperService.GetMapper<TileBorder>();
            _tileRenderLayerMapper = mapperService.GetMapper<TileRenderLayer>();
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(sortMode: SpriteSortMode.FrontToBack, transformMatrix: _camera.GetViewMatrix());
            foreach (var entity in ActiveEntities)
            {
                var tilePosition = _tilePositionMapper.Get(entity);
                var layer = _tileRenderLayerMapper.Get(entity);

                var sprite = _spriteMapper.Get(entity);
                if (sprite != null)
                {
                    sprite.Depth = _depthHelper.GetDepth(tilePosition, layer);
                    _spriteBatch.Draw(sprite, (tilePosition.Position + new Vector2(tilePosition.TileSpan.X / 2.0f, tilePosition.TileSpan.Y / 2.0f)).ToIsometric());
                }

                var tileBorder = _tileBorderMapper.Get(entity);
                if (tileBorder != null)
                {
                    _spriteBatch.DrawPolygon(
                        tilePosition.Position.ToIsometric(),
                        new Polygon(new[]{
                            (Vector2.UnitX * tilePosition.TileSpan.X),
                            (Vector2.UnitX * tilePosition.TileSpan.X + Vector2.UnitY * tilePosition.TileSpan.Y),
                            (Vector2.UnitY * tilePosition.TileSpan.Y),
                            Vector2.Zero}.ToIsometric()),
                        tileBorder.Color,
                        5, _depthHelper.GetDepth(tilePosition, layer));
                }
            }
            _spriteBatch.End();
        }
    }
}
