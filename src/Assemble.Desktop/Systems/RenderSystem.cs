using Assemble.Desktop.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.Sprites;

namespace Assemble.Desktop.Systems
{
    class RenderSystem : EntityDrawSystem
    {
        private readonly SpriteBatch _spriteBatch;
        private readonly OrthographicCamera _camera;
        private ComponentMapper<Transform2> _transformMapper;
        private ComponentMapper<Sprite> _spriteMapper;

        public RenderSystem(SpriteBatch spriteBatch, OrthographicCamera camera) : base(Aspect.All(typeof(Transform2), typeof(Sprite)))
        {
            _spriteBatch = spriteBatch;
            _camera = camera;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _transformMapper = mapperService.GetMapper<Transform2>();
            _spriteMapper = mapperService.GetMapper<Sprite>();
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(transformMatrix: _camera.GetViewMatrix());
            foreach (var entity in ActiveEntities)
            {
                var transform = _transformMapper.Get(entity).ToIsometric();
                var sprite = _spriteMapper.Get(entity);
                _spriteBatch.Draw(sprite, transform);
            }
            _spriteBatch.End();
        }
    }
}
