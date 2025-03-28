using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project.Blocks;
using Project.Commands;
using Project.Commands.CommandClasses;
using Project.Enemies;
using Project.Packages.Items;
using Project.renderer;
using Project.rooms;

namespace Project
{
    public class Game1 : Game
    {
        // Basic Game fields
        private GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch; // Not best practice

        // Controllers
        private List<IController> _controllers;
        private IController _keyboardController;
        private IController _mouseController;

        public Player player;

        // NOTE: From Boggus: enemyManager, etc. should all be hidden behind a room manager.
        // Remove dependencies here once they've been moved into their appropriate classes

        private EnemyManager enemyManager;
        private float elapsedTime;

        GameRenderer gameRenderer;
        RoomManager roomManager;

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
            this.enemyManager = new EnemyManager();

            base.Initialize();
        }

        private void SetUpMouseController()
        {
            /*MouseController mc = new MouseController();*/
            /**/
            /*this._mouseController = mc;*/
        }

        private void SetUpKeyboardController()
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

            kbc.RegisterKey(Keys.O, new CommandPreviousEnemy(this, this.enemyManager));
            kbc.RegisterKey(Keys.P, new CommandNextEnemy(this, this.enemyManager));

            this._keyboardController = kbc;
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
            roomManager = new RoomManager(enemyManager);
            gameRenderer.RoomManager = roomManager;
            roomManager.LoadRoomsFromContent(Content, gameRenderer);
            roomManager.AssignPlayer(this.player);
            gameRenderer.PlayerCharacter = this.player;

            _mouseController = new MouseController(this, _graphics, roomManager);

            // Set up controllers
            this._controllers = new List<IController>();
            this._controllers.Add(_mouseController);

            this.SetUpKeyboardController();

            this._controllers.Add(this._keyboardController);
        }

        protected override void Update(GameTime gameTime)
        {
            foreach (IController controller in this._controllers)
                controller.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            gameRenderer.Draw(_spriteBatch);

            // Draw world items
            // TODO: This will be removed once the GameRenderer class has implementations
            // for drawing items to the screen properly
            _spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
