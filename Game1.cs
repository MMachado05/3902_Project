using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Project.Commands.PlayerCommands;
using Project.Commands.RoomCommands;
using Project.Commands.GameLogicCommands;

using Project.Controllers;
using Project.Factories;
using Project.Renderer;
using Project.Rooms;
using Project.Characters;
using Project.Packages.Sounds;

namespace Project
{
    public class Game1 : Game
    {
        // Basic Game fields
        private GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;

        public Player player;

        private float elapsedTime;

        GameRenderer gameRenderer;
        Updater updater;
        RoomManager roomManager;
        SoundEffectManager soundEffectManager;

        public void restart()
        {
            this.LoadContent();
            this.Initialize();
        }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 960; // Set width
            _graphics.PreferredBackBufferHeight = 704; // Set height

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            this.player = new Player();

            base.Initialize();
        }

        private void CreateMouseController()
        {
            // TODO: Implement
        }


        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load all textures
            PlayerSpriteFactory.Instance.LoadAllTextures(Content);
            SolidBlockFactory.Instance.LoadAllTextures(Content);
            EnemySpriteFactory.Instance.LoadAllTextures(Content);
            ItemFactory.Instance.LoadAllTextures(Content);

            this.gameRenderer = new GameRenderer(64, 64);
            this.roomManager = new RoomManager();
            this.gameRenderer.RoomManager = roomManager;
            this.roomManager.LoadRoomsFromContent(Content, gameRenderer);
            this.roomManager.AssignPlayer(this.player);
            this.gameRenderer.PlayerCharacter = this.player;
            this.updater = new Updater(this.roomManager, this.player);
            this.updater.RegisterController(this.CreateKeyboardController());

            this.soundEffectManager = new SoundEffectManager(this.gameRenderer, this.roomManager);
            this.soundEffectManager.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            this.updater.Update(gameTime);
            base.Update(gameTime);
            this.soundEffectManager.Update();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            gameRenderer.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        // -------------------------- UTILITY METHODS -------------------------------

        private IController CreateKeyboardController()
        {
            KeyboardController kbc = new KeyboardController();

            // Player movement
            kbc.RegisterKey(Keys.W, new MoveCommand(player, Direction.Up));
            kbc.RegisterKey(Keys.A, new MoveCommand(player, Direction.Left));
            kbc.RegisterKey(Keys.S, new MoveCommand(player, Direction.Down));
            kbc.RegisterKey(Keys.D, new MoveCommand(player, Direction.Right));
            kbc.DefaultCommand = new StopPlayerCommand(player);

            // Attacking
            kbc.RegisterKey(Keys.Z, new AttackCommand(player));
            kbc.RegisterKey(Keys.N, new AttackCommand(player));

            // Arrow keys to change current room
            kbc.RegisterKey(Keys.Up, new RoomUpCommand(roomManager));
            kbc.RegisterKey(Keys.Down, new RoomDownCommand(roomManager));
            kbc.RegisterKey(Keys.Left, new RoomLeftCommand(roomManager));
            kbc.RegisterKey(Keys.Right, new RoomRightCommand(roomManager));

            // Game logic
            kbc.RegisterKey(Keys.Q, new QuitCommand(this));
            kbc.RegisterKey(Keys.Escape, new QuitCommand(this));
            kbc.RegisterKey(Keys.R, new RestartGameCommand(this));

            // Debugging commands
            kbc.RegisterKey(Keys.E, new DamageCommand(player));

            return kbc;
        }

    }
}
