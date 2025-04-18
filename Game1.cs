﻿using Microsoft.Xna.Framework;
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
using Project.Packages.Commands.GameLogicCommands;
using Myra;
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

        private GameStateMachine gameState;

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
            MyraEnvironment.Game = this; // UI library

            this.player = new Player();
            this.gameState = new GameStateMachine();
            this.gameState.State = GameState.Playing;

            base.Initialize();
        }

        private void CreateMouseController()
        {
            // TODO: Implement
        }


        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            this.roomManager = new RoomManager();
            this.gameRenderer = new GameRenderer(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight, 64, 64, this.gameState);

            // Load all textures
            // TODO: All of these should likely take the tile width and height, especially
            // if they have any say in how these are drawn to the screen.
            PlayerSpriteFactory.Instance.LoadAllTextures(Content);
            SolidBlockFactory.Instance.LoadAllTextures(Content, this.roomManager);
            EnemySpriteFactory.Instance.LoadAllTextures(Content);
            ItemFactory.Instance.LoadAllTextures(Content,
                gameRenderer.TileWidth, gameRenderer.TileHeight);
            HealthBarSpriteFactory.Instance.LoadAllTextures(Content);

            this.gameRenderer.RoomManager = roomManager;
            this.roomManager.LoadRoomsFromContent(Content, gameRenderer);
            this.roomManager.AssignPlayer(this.player);
            this.gameRenderer.PlayerCharacter = this.player;

            SoundEffectManager.Instance.LoadContent(Content);

            // Osama: Also, these need to be loaded after roomManager, so moving these down here.
            this.updater = new Updater(this.roomManager, this.player, new RestartGameCommand(this), this.gameState); //TODO: update updater.cs to accept this.
            this.updater.RegisterController(this.CreateKeyboardController());
        }

        protected override void Update(GameTime gameTime)
        {
            this.updater.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

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

            // Music toggle
            kbc.RegisterOnPress(Keys.M, new ToggleMusicCommand(soundEffectManager));
            kbc.RegisterOnPress(Keys.P, new PauseGameCommand(this.gameState));

            // Debugging commands
            kbc.RegisterOnPress(Keys.E, new DamageCommand(player));

            return kbc;
        }
    }

    public class GameStateMachine
    {
        public GameState State { get; set; }
    }
}
