using System;
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
        private readonly (int x, int y) _worldMapSize;
        private ComponentMapper<TilePosition> _tilePositionMapper;
        private ComponentMapper<MapTile> _mapTileMapper;

        private readonly float _mapTileSize;
        private const float MiniMapSizeWidth = 300;

        public MapRenderSystem(SpriteBatch spriteBatch, OrthographicCamera camera, int worldMapSize) : base(Aspect.All(typeof(MapTile), typeof(TilePosition)))
        {
            _spriteBatch = spriteBatch;
            _camera = camera;
            _worldMapSize = (worldMapSize, worldMapSize);
            _mapTileSize = MiniMapSizeWidth * _worldMapSize.x;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _tilePositionMapper = mapperService.GetMapper<TilePosition>();
            _mapTileMapper = mapperService.GetMapper<MapTile>();
        }

        public override void Draw(GameTime gameTime)
        {
            DrawBackdrop();
            DrawIsometricMapAndViewport();
        }

        private void DrawIsometricMapAndViewport()
        {
            var worldHyptothenus = Math.Sqrt(_worldMapSize.x * _worldMapSize.x + _worldMapSize.y * _worldMapSize.y);
            _spriteBatch.Begin(transformMatrix:
                Matrix.CreateRotationZ((float)Math.PI / 4) *
                Matrix.CreateTranslation((float)worldHyptothenus / 2, 0, 0) *
                Matrix.CreateScale((float)(MiniMapSizeWidth / worldHyptothenus), 0.5f * (float)(MiniMapSizeWidth / worldHyptothenus), 1f));

            foreach (var entity in ActiveEntities)
            {
                var tilePosition = _tilePositionMapper.Get(entity);
                var mapTile = _mapTileMapper.Get(entity);
                _spriteBatch.FillRectangle(tilePosition.Position, new Size2(tilePosition.TileSpan.X, tilePosition.TileSpan.Y), mapTile.Color);
            }

            var cameraTopLeft = new Vector2(_camera.BoundingRectangle.Left, _camera.BoundingRectangle.Top).FromIsometric();
            var cameraTopRight = new Vector2(_camera.BoundingRectangle.Right, _camera.BoundingRectangle.Top).FromIsometric();
            var cameraBottomLeft = new Vector2(_camera.BoundingRectangle.Left, _camera.BoundingRectangle.Bottom).FromIsometric();
            var cameraBottomRight = new Vector2(_camera.BoundingRectangle.Right, _camera.BoundingRectangle.Bottom).FromIsometric();
            _spriteBatch.DrawPolygon(Vector2.Zero, new[] { cameraTopLeft, cameraTopRight, cameraBottomRight, cameraBottomLeft, cameraTopLeft }, Color.White, 1.1f);

            _spriteBatch.End();
        }

        private void DrawBackdrop()
        {
            _spriteBatch.Begin();
            _spriteBatch.FillRectangle(0, 0, MiniMapSizeWidth, 0.5f * MiniMapSizeWidth, Color.CornflowerBlue);
            _spriteBatch.End();
        }
    }
}
