using Assemble.Desktop;
using Assemble.Desktop.Components;
using Assemble.Desktop.Enums;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.Sprites;

namespace Assemble.Desktop.Systems
{
    public class AlignableSpritePickingSystem : EntityProcessingSystem
    {
        private readonly TexturesManager textureManager;
        private ComponentMapper<Sprite> _spriteMapper;
        private ComponentMapper<Alignable> _alignableMapper;
        private ComponentMapper<DirectionalTexture> _directionalTextureMapper;

        public AlignableSpritePickingSystem(TexturesManager textureManager) : base(Aspect.All(typeof(Alignable), typeof(DirectionalTexture)))
        {
            this.textureManager = textureManager;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _spriteMapper = mapperService.GetMapper<Sprite>();
            _alignableMapper = mapperService.GetMapper<Alignable>();
            _directionalTextureMapper = mapperService.GetMapper<DirectionalTexture>();
        }

        public override void Process(GameTime gameTime, int entityId)
        {
            var direction = _alignableMapper.Get(entityId).Direction;
            var directionalTexture = _directionalTextureMapper.Get(entityId);
            var texture = direction switch
            {
                Direction.NorthEast => directionalTexture.NorthEastTexture,
                Direction.SouthEast => directionalTexture.SouthEastTexture,
                Direction.SouthWest => directionalTexture.SouthWestTexture,
                Direction.NorthWest => directionalTexture.NorthWestTexture,
                _ => directionalTexture.NorthEastTexture,
            };

            var sprite = _spriteMapper.Get(entityId);
            if (sprite != null)
            {
                sprite.TextureRegion = textureManager.GetRandomTexture(texture);
            }
            else
            {
                sprite = new Sprite(textureManager.GetRandomTexture(texture));
                GetEntity(entityId).Attach(sprite);
            }

            sprite.Alpha = directionalTexture.Alpha;
        }
    }
}