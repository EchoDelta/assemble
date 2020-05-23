using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.TextureAtlases;

namespace Assemble.Desktop
{
    public class TexturesManager
    {
        private readonly ContentManager _contentManager;
        IDictionary<Texture, Texture2D> _textureMap;

        public TexturesManager(ContentManager contentManager)
        {
            _contentManager = contentManager;
        }

        public void LoadTextures()
        {
            _textureMap = new Dictionary<Texture, Texture2D>();

            LoadTiles();
        }

        private void LoadTiles()
        {
            _textureMap.Add(Texture.Tile1, _contentManager.Load<Texture2D>("Tile"));
        }

        public Texture2D GetTexture(params Texture[] textures)
        {
            if (textures.Length == 0)
            {
                throw new ArgumentException("Must specify at least one texture");
            }
            var random = new Random();
            return _textureMap[textures[random.Next(textures.Length)]];
        }
    }
}