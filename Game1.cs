using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project.Blocks;
using Project.Content;
using Project.Enemies;
using Project.Packages.Items;

namespace Project
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch; // Not best practice
        private KeyboardController _keyboardController;
        private Player player;

        public ISprite playerSprite; // Not best practice, but easiest fix. Could later create read-only property for playerSprite
        private Rectangle playerPosition;
        private Vector2 playerPositionVector;
        public Direction spriteType;

        public string lastDirection = "Down"; // Default direction set to "down" for now; also public not best practice but easy fix for now.

        private EnemyManager enemyManager;

        private float elapsedTime;

        // Not best practice; should be moved out of game1
        /// <summary>
        /// </summary>
        KeyboardState input;

        private SolidBlockManager blockManager;
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
            // Since we're in constructor, no need to call GraphicsDeviceManager.ApplyChanges()
            // :)
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1600; // Set width
            _graphics.PreferredBackBufferHeight = 900; // Set height

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            playerPosition = new Rectangle(100, 100, 30, 30); // Initial character position
            playerPositionVector = new Vector2(100, 100);

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
            playerSprite = PlayerSpriteFactory.Instance.NewDownStoppedPlayer();

            // Load item sprites and create item manager
            ItemFactory.Instance.LoadContent(Content);
            itemManager = new ItemManager();
            // Initialize ItemController with commands for switching items
            _itemController = new ItemController(itemManager, this);

            // Initialize KeyboardController with movement and quit commands, pass in player and game

            EnemySpriteFactory.Instance.LoadAllTextures(Content);

            enemyManager = new EnemyManager();
            _keyboardController = new KeyboardController(player, this, blockManager, enemyManager);

            playerItemCollisionHandler = new PlayerItemCollisionHandler(itemManager, player);
        }


        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            _keyboardController.Update();
            _itemController.Update();

            // Check if player has stopped moving
            input = Keyboard.GetState();
            if (!(input.IsKeyDown(Keys.W) || input.IsKeyDown(Keys.A) || input.IsKeyDown(Keys.S) || input.IsKeyDown(Keys.D)) && player.Sprite.State != SpriteState.Attacking)
            {
                player.SetStaticSprite(); // Set idle sprite
            }

            player.Update(gameTime);

            //should be replaced with level loader
            foreach (Item item in itemManager.itemList)
            {
                item.Update();
            }
            //inventory
            itemManager.getCurrentItem().Update();
            Vector2 heldItemPosition = new Vector2(player.PositionVector.X + 25, player.PositionVector.Y);
            itemManager.getCurrentItem().Position = heldItemPosition;


            elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (elapsedTime > 0.25)
            {
                enemyManager.GetCurrentEnemy().UpdateAnimation(gameTime);
                elapsedTime = 0f;
            }

            enemyManager.GetCurrentEnemy().UpdateState(gameTime);

            playerItemCollisionHandler.HandlePlayerItemCollision();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            blockManager.GetCurrentBlock().Draw();

            player.Draw(_spriteBatch);

            enemyManager.GetCurrentEnemy().Draw(_spriteBatch);
            itemManager.getCurrentItem().Draw(_spriteBatch);
            //should be replaced with level loader
            foreach (Item item in itemManager.itemList)
            {
                item.Draw(_spriteBatch);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
