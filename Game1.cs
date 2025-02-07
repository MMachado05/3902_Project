using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Project
{

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        internal CharacterManager CharacterManager = new();
        public int Printscale = 3;

        internal IController _keyboardController;
        internal IController _mouseController;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();

            CharacterManager = new();

            LoadContent();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _keyboardController = new KeyboardController(this);
            _mouseController = new MouseController(this, _graphics);

            //add load content from CharacterManager
            CharacterManager.LoadContent(_graphics, _spriteBatch, Content);
        }

        protected override void Update(GameTime gameTime)
        {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                //Exit();

            CharacterManager.Update(gameTime);

            _keyboardController.Update();
            //_mouseController.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            CharacterManager.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

    }

}
