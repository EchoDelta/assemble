using Assemble.Desktop.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Entities;

namespace Assemble.Desktop
{
    public class GameMain : Game
    {
        readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private World _world;

        private OrthographicCamera _camera;

        public GameMain()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            var asdf = new OrthographicCamera(GraphicsDevice);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _camera = new OrthographicCamera(GraphicsDevice);
            _world = new WorldBuilder()
                .AddSystem(new ControlSystem())
                .AddSystem(new CameraSystem())
                .AddSystem(new RenderSystem(_spriteBatch, _camera))
                .Build();

            var entityBuilder = new EntityBuilder(Content);

            var mapBuilder = new MapBuilder(_world, entityBuilder, 100, 100);
            mapBuilder.BuildMap();

            var gameCamera = entityBuilder.BuildGameCamera(_world.CreateEntity(), _camera, new Vector2(50, 50));

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _world.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _world.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}
