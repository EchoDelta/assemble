using Assemble.Desktop.Components;
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
    class RenderSystem : EntityDrawSystem
    {
        private readonly SpriteBatch _spriteBatch;
        private readonly OrthographicCamera _camera;
        private ComponentMapper<TilePosition> _tilePositionMapper;
        private ComponentMapper<Sprite> _spriteMapper;
        private ComponentMapper<TileBorder> _tileBorderMapper;

        public RenderSystem(SpriteBatch spriteBatch, OrthographicCamera camera) : base(Aspect.All(typeof(TilePosition)).One(typeof(Sprite), typeof(TileBorder)))
        {
            _spriteBatch = spriteBatch;
            _camera = camera;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _tilePositionMapper = mapperService.GetMapper<TilePosition>();
            _spriteMapper = mapperService.GetMapper<Sprite>();
            _tileBorderMapper = mapperService.GetMapper<TileBorder>();
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(transformMatrix: _camera.GetViewMatrix());
            foreach (var entity in ActiveEntities)
            {
                var tilePosition = _tilePositionMapper.Get(entity);

                var sprite = _spriteMapper.Get(entity);
                if (sprite != null)
                {
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
                        Color.HotPink,
                        5);
                }

            }
            _spriteBatch.End();
        }
    }
}
