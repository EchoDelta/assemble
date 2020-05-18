using Assemble.Desktop.Components;
using Assemble.Desktop.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;

namespace Assemble.Desktop.Systems
{
    class MapRenderSystem : EntityDrawSystem
    {
        private readonly SpriteBatch _spriteBatch;
        private readonly OrthographicCamera _camera;
        private ComponentMapper<Transform2> _transformMapper;
        private ComponentMapper<MapTile> _mapTileMapper;

        private const float mapTileSize = 1.5f;

        public MapRenderSystem(SpriteBatch spriteBatch, OrthographicCamera camera) : base(Aspect.All(typeof(MapTile), typeof(Transform2)))
        {
            _spriteBatch = spriteBatch;
            _camera = camera;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _transformMapper = mapperService.GetMapper<Transform2>();
            _mapTileMapper = mapperService.GetMapper<MapTile>();
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(transformMatrix: Matrix.CreateScale(mapTileSize));
            foreach (var entity in ActiveEntities)
            {
                var transform = _transformMapper.Get(entity);
                var mapTile = _mapTileMapper.Get(entity);
                _spriteBatch.FillRectangle(transform.Position, new Size2(1, 1), mapTile.Color);
            }

            var cameraTopLeft = new Vector2(_camera.BoundingRectangle.Left, _camera.BoundingRectangle.Top).FromIsometric();
            var cameraTopRight = new Vector2(_camera.BoundingRectangle.Right, _camera.BoundingRectangle.Top).FromIsometric();
            var cameraBottomLeft = new Vector2(_camera.BoundingRectangle.Left, _camera.BoundingRectangle.Bottom).FromIsometric();
            var cameraBottomRight = new Vector2(_camera.BoundingRectangle.Right, _camera.BoundingRectangle.Bottom).FromIsometric();
            _spriteBatch.DrawPolygon(Vector2.Zero, new[] { cameraTopLeft, cameraTopRight, cameraBottomRight, cameraBottomLeft, cameraTopLeft }, Color.Red, 1f/mapTileSize);

            _spriteBatch.End();
        }
    }
}
