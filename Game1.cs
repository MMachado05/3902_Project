using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project.Blocks;
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
        private GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch; // Not best practice
        private KeyboardController _keyboardController;
        public Player player;
        private Rectangle playerPosition;
        private Vector2 playerPositionVector;
        MouseController mouseController;
        // NOTE: From Boggus: The Controllers all implement IController, so we should
        // use that abstraction

        public ISprite playerSprite; // Not best practice, but easiest fix. Could later create read-only property for playerSprite
        public Direction spriteType;
        KeyboardState input;

        public string lastDirection = "Down"; // Default direction set to "down" for now; also public not best practice but easy fix for now.
        private CollisionManager collisionManager;

        // NOTE: From Boggus: enemyManager, etc. should all be hidden behind a room manager.
        // Remove dependencies here once they've been moved into their appropriate classes

        private EnemyManager enemyManager;
        private float elapsedTime;


        // Not best practice; should be moved out of game1
        /// <summary>
        /// </summary>

        private SolidBlockManager blockManager;
        Renderer renderer;
        RoomsManager roomManager;
        public void restart()
        {
            this.LoadContent();
            this.Initialize();
        }

        // should be moved out of game1
        ItemManager itemManager;
        KeyboardState previousState = new KeyboardState();


        private ItemController _itemController;
        PlayerItemCollisionHandler playerItemCollisionHandler;

        public Game1()
        {
            // Since we're in constructor, no need to call GraphicsDeviceManager.ApplyChanges()
            // :)
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 960; // Set width
            _graphics.PreferredBackBufferHeight = 704; // Set height

            Content.RootDirectory = "Content";
            IsMouseVisible = true;


        }
        protected override void Initialize()
        {

            //playerPosition = new Rectangle(100, 100, 30, 30); // Initial character position
            //playerPositionVector = new Vector2(100, 100);

            player = new Player();

            input = Keyboard.GetState();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load all textures
            PlayerSpriteFactory.Instance.LoadAllTextures(Content);
            SolidBlockSpriteFactory.Instance.LoadAllTextures(Content);
            blockManager = new SolidBlockManager(_spriteBatch);

            // Set initial sprite to static down
            playerSprite = PlayerSpriteFactory.Instance.NewStoppedPlayerSprite(Direction.Down, false);

            // Load item sprites and create item manager
            ItemFactory.Instance.LoadContent(Content);
            itemManager = new ItemManager(this);
            // Initialize ItemController with commands for switching items
            _itemController = new ItemController(itemManager, this);

            // Initialize KeyboardController with movement and quit commands, pass in player and game

            EnemySpriteFactory.Instance.LoadAllTextures(Content);
            collisionManager = new CollisionManager();
            enemyManager = new EnemyManager();
            _keyboardController = new KeyboardController(player, this, blockManager, enemyManager);
            roomManager = new RoomsManager(blockManager, enemyManager, this);
            renderer = new Renderer(roomManager, enemyManager, collisionManager);
            mouseController = new MouseController(this, _graphics, roomManager);

            playerItemCollisionHandler = new PlayerItemCollisionHandler(itemManager, player);
        }

        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            _keyboardController.Update();
            mouseController.Update();
            _itemController.Update();

            // Check if player has stopped moving
            input = Keyboard.GetState();

            //collisionManager.UpdateCollisions(player, blockManager.GetAllBlocks());

            if (!(input.IsKeyDown(Keys.W) || input.IsKeyDown(Keys.A) || input.IsKeyDown(Keys.S) || input.IsKeyDown(Keys.D)) && player.Sprite.State != SpriteState.Attacking)
            {
                player.SetStaticSprite(); // Set idle sprite
            }


            // Update lastDirection based on movement input (def need to change this approach)
            if (input.IsKeyDown(Keys.W)) lastDirection = "Up";
            else if (input.IsKeyDown(Keys.S)) lastDirection = "Down";
            else if (input.IsKeyDown(Keys.A)) lastDirection = "Left";
            else if (input.IsKeyDown(Keys.D)) lastDirection = "Right";
            // NOTE: From Boggus: Move to KeyboardController class


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


            renderer.Update(gameTime);

            playerItemCollisionHandler.HandlePlayerItemCollision();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
           renderer.Draw(_spriteBatch);
            // Draw world items
            foreach (IItem item in itemManager.GetWorldItems())
            {
                item.Draw(_spriteBatch);
            }
            // Draw inventory items
            itemManager.GetCurrentItem().Draw(_spriteBatch);
           enemyManager.GetCurrentEnemy().Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
