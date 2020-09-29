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
        private ComponentMapper<Transform2> _transformMapper;
        private ComponentMapper<Sprite> _spriteMapper;
        private ComponentMapper<GridBorder> _gridBorderMapper;

        public RenderSystem(SpriteBatch spriteBatch, OrthographicCamera camera) : base(Aspect.All(typeof(Transform2)).One(typeof(Sprite), typeof(GridBorder)))
        {
            _spriteBatch = spriteBatch;
            _camera = camera;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _transformMapper = mapperService.GetMapper<Transform2>();
            _spriteMapper = mapperService.GetMapper<Sprite>();
            _gridBorderMapper = mapperService.GetMapper<GridBorder>();
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(transformMatrix: _camera.GetViewMatrix());
            foreach (var entity in ActiveEntities)
            {
                var transform = _transformMapper.Get(entity).ToIsometric();

                var sprite = _spriteMapper.Get(entity);
                if (sprite != null)
                {
                    _spriteBatch.Draw(sprite, transform);
                }

                var gridBorder = _gridBorderMapper.Get(entity);
                if (gridBorder != null)
                {
                    _spriteBatch.DrawPolygon(
                        transform.Position,
                        new Polygon(new[]{
                            (Vector2.UnitX * gridBorder.GridSizeX),
                            (Vector2.UnitX * gridBorder.GridSizeX + Vector2.UnitY * gridBorder.GridSizeY),
                            (Vector2.UnitY * gridBorder.GridSizeY),
                            Vector2.Zero}.ToIsometric(true)),
                        Color.HotPink,
                        5);
                }

            }
            _spriteBatch.End();
        }
    }
}
