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
        private readonly Random _random;
        IDictionary<Texture, TextureRegion2D> _textureMap;

        public TexturesManager(ContentManager contentManager)
        {
            _contentManager = contentManager;
            _random = new Random();
        }

        public void LoadTextures()
        {
            _textureMap = new Dictionary<Texture, TextureRegion2D>();

            LoadTiles();
            LoadOre();
            LoadMiner();
        }

        private void LoadTiles()
        {
            var texture = _contentManager.Load<Texture2D>("Tile");
            var textureMap = _contentManager.Load<Dictionary<string, Rectangle>>("TileMap");
            var atlas = new TextureAtlas("TileAtlas", texture, textureMap);
            _textureMap.Add(Texture.Tile1, atlas.GetRegion(0));
            _textureMap.Add(Texture.Tile2, atlas.GetRegion(1));
            _textureMap.Add(Texture.Tile3, atlas.GetRegion(2));
            _textureMap.Add(Texture.Tile4, atlas.GetRegion(3));
        }

        private void LoadOre()
        {
            var texture = _contentManager.Load<Texture2D>("Ore");
            var textureMap = _contentManager.Load<Dictionary<string, Rectangle>>("OreMap");
            var atlas = new TextureAtlas("OreAtlas", texture, textureMap);
            _textureMap.Add(Texture.IronOre1, atlas.GetRegion(0));
            _textureMap.Add(Texture.IronOre2, atlas.GetRegion(1));
            _textureMap.Add(Texture.IronOre3, atlas.GetRegion(2));
        }

        private void LoadMiner()
        {
            var texture = _contentManager.Load<Texture2D>("Miner");
            var textureMap = _contentManager.Load<Dictionary<string, Rectangle>>("MinerMap");
            var atlas = new TextureAtlas("MinerAtlas", texture, textureMap);
            _textureMap.Add(Texture.Miner, atlas.GetRegion(0));
        }

        public TextureRegion2D GetRandomTexture(params Texture[] textures)
        {
            if (textures.Length == 0)
            {
                throw new ArgumentException("Must specify at least one texture");
            }
            var texture = textures[_random.Next(textures.Length)];
            return _textureMap[texture];
        }
    }
}