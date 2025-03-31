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
        }

        protected override void Update(GameTime gameTime)
        {
            this.updater.Update(gameTime);
            base.Update(gameTime);
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
            kbc.RegisterOnPress(Keys.W, new UpdateVelocityCommand(player, Direction.Up,
                  false, 0, -2, false, true));
            kbc.RegisterOnPress(Keys.A, new UpdateVelocityCommand(player, Direction.Left,
                  false, -2, 0, true, false));
            kbc.RegisterOnPress(Keys.S, new UpdateVelocityCommand(player, Direction.Down,
                  false, 0, 2, false, true));
            kbc.RegisterOnPress(Keys.D, new UpdateVelocityCommand(player, Direction.Right,
                  false, 2, 0, true, false));
            kbc.RegisterOnRelease(Keys.W, new UpdateVelocityCommand(player, Direction.Up,
                  true, 0, 0, false, true));
            kbc.RegisterOnRelease(Keys.A, new UpdateVelocityCommand(player, Direction.Left,
                  true, 0, 0, true, false));
            kbc.RegisterOnRelease(Keys.S, new UpdateVelocityCommand(player, Direction.Down,
                  true, 0, 0, false, true));
            kbc.RegisterOnRelease(Keys.D, new UpdateVelocityCommand(player, Direction.Right,
                  true, 0, 0, true, false));

            kbc.DefaultCommand = new StopPlayerCommand(player);

            // Attacking
            kbc.RegisterOnPress(Keys.Z, new AttackCommand(player));
            kbc.RegisterOnPress(Keys.N, new AttackCommand(player));

            // Arrow keys to change current room
            kbc.RegisterOnPress(Keys.Up, new RoomUpCommand(roomManager));
            kbc.RegisterOnPress(Keys.Down, new RoomDownCommand(roomManager));
            kbc.RegisterOnPress(Keys.Left, new RoomLeftCommand(roomManager));
            kbc.RegisterOnPress(Keys.Right, new RoomRightCommand(roomManager));

            // Game logic
            kbc.RegisterOnPress(Keys.Q, new QuitCommand(this));
            kbc.RegisterOnPress(Keys.Escape, new QuitCommand(this));
            kbc.RegisterOnPress(Keys.R, new RestartGameCommand(this));

            // Debugging commands
            kbc.RegisterOnPress(Keys.E, new DamageCommand(player));

            return kbc;
        }

    }
}
