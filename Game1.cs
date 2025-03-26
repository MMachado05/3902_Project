using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project.Blocks;
using Project.Commands;
using Project.Commands.CommandClasses;
using Project.Content;
using Project.Enemies;
using Project.Packages.Characters;
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
        private Rectangle playerPosition;
        private Vector2 playerPositionVector;

        public ISprite playerSprite; // Not best practice, but easiest fix. Could later create read-only property for playerSprite
        public Direction spriteType;

        public string lastDirection = "Down"; // Default direction set to "down" for now; also public not best practice but easy fix for now.
        private CollisionManager collisionManager;

        // NOTE: From Boggus: enemyManager, etc. should all be hidden behind a room manager.
        // Remove dependencies here once they've been moved into their appropriate classes

        private EnemyManager enemyManager;
        private float elapsedTime;


        // Not best practice; should be moved out of game1
        /// <summary>
        /// </summary>

        GameRenderer gameRenderer;
        RoomManager roomManager;
        public void restart()
        {
            this.LoadContent();
            this.Initialize();
        }

        // should be moved out of game1
        ItemManager itemManager;

        private ItemController _itemController;
        PlayerItemCollisionHandler playerItemCollisionHandler;

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

            // Set initial sprite to static down
            playerSprite = PlayerSpriteFactory.Instance.NewStoppedPlayerSprite(Direction.Down, false);

            // Load item sprites and create item manager
            ItemFactory.Instance.LoadContent(Content);
            itemManager = new ItemManager(this);
            // Initialize ItemController with commands for switching items
            _itemController = new ItemController(itemManager, this);

            // Initialize KeyboardController with movement and quit commands, pass in player and game
            GameRenderer gameRenderer = new GameRenderer(32, 32);
            EnemySpriteFactory.Instance.LoadAllTextures(Content);
            collisionManager = new CollisionManager();
            roomManager = new RoomManager(enemyManager);
            gameRenderer.RoomManager = roomManager;
            roomManager.LoadRoomsFromContent(Content, gameRenderer);
            gameRenderer.PlayerCharacter = this.player;

            _mouseController = new MouseController(this, _graphics, roomManager);

            // Set up controllers
            this._controllers = new List<IController>();
            this._controllers.Add(_mouseController);

            this.SetUpKeyboardController();

            this._controllers.Add(this._keyboardController);
            /*this._controllers.Add(this._mouseController);*/



            playerItemCollisionHandler = new PlayerItemCollisionHandler(itemManager, player);
        }

        protected override void Update(GameTime gameTime)
        {
            foreach (IController controller in this._controllers)
                controller.Update();
            _itemController.Update();

            //should be replaced with level loader
            _itemController.Update();

            // Update world items
            foreach (IItem item in itemManager.GetWorldItems())
            {
                item.Update();
            }

            //Placing and updating inventory items
            itemManager.GetCurrentItem().Update();
            itemManager.PlaceInventoryItem();

            playerItemCollisionHandler.HandlePlayerItemCollision();

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
            itemManager.GetCurrentItem().Draw(_spriteBatch);
            enemyManager.GetCurrentEnemy().Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
